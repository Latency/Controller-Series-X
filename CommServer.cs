//  *****************************************************************************
//  File:       CommServer.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using OOP.Controllers;
using OOP.Interfaces;

namespace OOP {
  /// <inheritdoc />
  /// <summary>
  ///   Communication server
  /// </summary>
  public sealed class CommServer : IDisposable {
    #region Fields
    // -----------------------------------------------------------------------

    private readonly IController[] _controllers;
    private readonly ParallelOptions _options;

    // -----------------------------------------------------------------------
    #endregion Fields


    #region Properties
    // -----------------------------------------------------------------------

    public List<Task> RunningTasks { get; }
    public CancellationTokenSource TokenSource { get; }

    // -----------------------------------------------------------------------
    #endregion Properties


    #region Constructors
    // -----------------------------------------------------------------------

    /// <summary>
    ///  Constructor
    /// </summary>
    public CommServer() {
      RunningTasks = new List<Task>();

      _controllers = new IController[] {
        new Series3(),
        new Series4(),
        new Series5(),
        new Series6()
      };

      TokenSource = new CancellationTokenSource();
      TokenSource.Token.Register(() => {
        Console.WriteLine("Cancelled...");
      });

      _options = new ParallelOptions {
        MaxDegreeOfParallelism = Environment.ProcessorCount,
        CancellationToken = TokenSource.Token
      };
    }


    /// <summary>
    ///   Finalizer
    /// </summary>
    ~CommServer() {
      Dispose();
    }

    // -----------------------------------------------------------------------

    #endregion Constructors


    #region Methods

    // -----------------------------------------------------------------------

    /// <summary>
    ///   Connect
    /// </summary>
    /// <param name="address"></param>
    public void Connect(string address) {
      var server = address.Substring(0, address.IndexOf('.'));

      Parallel.ForEach(_controllers, _options, (ctrlr, idx) => {
        if (idx.ShouldExitCurrentIteration || TokenSource.IsCancellationRequested || !nameof(ctrlr).Substring(1).StartsWith(server))
          return;

        ctrlr.Connect(address);
      });
    }


    /// <summary>
    ///   ReadData
    /// </summary>
    public void ReadData() {
      Parallel.ForEach(_controllers, _options, (ctrlr, idx) => {
        if (idx.ShouldExitCurrentIteration || TokenSource.IsCancellationRequested)
          return;

        Task.Run(() => {
          while (!TokenSource.IsCancellationRequested) {
            ctrlr.ReadData();
            Thread.Sleep(TimeSpan.FromSeconds(2));
          }
        }, TokenSource.Token);
      });
    }


    /// <summary>
    ///   ProcessData
    /// </summary>
    public void ProcessData() {
      Parallel.ForEach(_controllers, _options, async (ctrlr, idx) => {
        if (idx.ShouldExitCurrentIteration || TokenSource.IsCancellationRequested)
          return;

        Task runningTask = null;

        try {
          runningTask = Task.Run(() => {
            while (!TokenSource.IsCancellationRequested) {
              ctrlr.GetData(out var data);
              if (data != null)
                Console.WriteLine(data.ToString());
              else {
                throw new Exception();
              }
            }
          }, TokenSource.Token);
          RunningTasks.Add(runningTask);
          Console.WriteLine($"Created task {runningTask.Id}...");
          await runningTask;
        } catch (TaskCanceledException) {
        } catch (Exception ex) {
          Console.WriteLine($"{MethodBase.GetCurrentMethod().Name}:  {ex.Message}");
        } finally {
          RunningTasks.Remove(runningTask);
        }
      });
    }


    /// <summary>
    ///  Listen
    /// </summary>
    public void Listen() {
      try {
        Task.WaitAll(RunningTasks.ToArray(), TokenSource.Token);
      } catch (OperationCanceledException) { }
    }


    /// <inheritdoc />
    /// <summary>
    ///   Dispose
    /// </summary>
    public void Dispose() {
      foreach (var ctrlr in _controllers)
        ctrlr.Dispose();

      if (!TokenSource.IsCancellationRequested)
        TokenSource.Cancel();

      try {
        Task.WaitAll(RunningTasks.ToArray());
      } catch (TaskCanceledException) {
      } finally {
        RunningTasks.Clear();

        GC.SuppressFinalize(this);
      }
    }

    // -----------------------------------------------------------------------

    #endregion Methods
  }
}