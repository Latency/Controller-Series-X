//  *****************************************************************************
//  File:       Program.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;

namespace OOP {
  /// <summary>
  ///   Improve the code below.
  /// </summary>
  internal class Program {
    /// <summary>
    ///  Main entry point.
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args) {
      var server = new CommServer();

      Task.Run(() => {
        // ReSharper disable once ConvertToLocalFunction
        ConsoleCancelEventHandler onCancel = (o, e) => {
          e.Cancel = true;
          server.TokenSource.Cancel();
        };

        Console.WriteLine(@"Press `CTRL+C' to abort the process...");

        // Establish an event handler to process key press events.
        Console.CancelKeyPress += onCancel;

        while (!server.TokenSource.IsCancellationRequested) {
          if (Console.KeyAvailable)
            Console.ReadKey(true);
          else
            Thread.Sleep(250);
        }

        Console.CancelKeyPress -= onCancel;
      });

      server.ReadData();

      server.ProcessData();

      server.Listen();
    }
  }
}