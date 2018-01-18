//  *****************************************************************************
//  File:       Controller.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using OOP.Interfaces;

namespace OOP {
  public abstract class Controller : IController {
    private readonly Queue<IControllerData> _data = new Queue<IControllerData>();


    #region Constructors
    // ------------------------------------------------------------------------

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="id"></param>
    protected Controller(double id) {
      ID = id;
      EventHandle = new AutoResetEvent(false);
      // ReSharper disable once VirtualMemberCallInConstructor
      ControllerType = InitializeData();
    }

    // ------------------------------------------------------------------------
    #endregion Constructors

    
    #region Properties
    // ------------------------------------------------------------------------
    
    // ReSharper disable once InconsistentNaming
    public double ID { get; }

    public EventWaitHandle EventHandle { get; }

    public IPAddress Address {get; private set; }
    
    public IControllerData ControllerType { get; set; }

    // ------------------------------------------------------------------------
    #endregion Properties

    
    #region Methods
    // ------------------------------------------------------------------------

    /// <summary>
    ///  InitializeData
    /// </summary>
    protected abstract IControllerData InitializeData();


    /// <inheritdoc />
    /// <summary>
    ///  Connect
    /// </summary>
    /// <param name="ipAddress"></param>
    public virtual void Connect(string ipAddress) {
      try {
        // Create an instance of IPAddress for the specified address string (in dotted-quad, or colon-hexadecimal notation).
        Address = IPAddress.Parse(ipAddress);

        // Display the address in standard notation.
        //Console.WriteLine($"Parsing your input string:  \"{ipAddress}\" produces this address (shown in its standard notation):  {address.ToString()}");
      } catch (ArgumentNullException) {
        // ...
      } catch (FormatException) {
        // ...
      }
    }


    /// <summary>
    ///  ReadDataDelegate
    /// </summary>
    /// <returns></returns>
    public abstract IControllerData ReadDataDelegate();


    /// <inheritdoc />
    /// <summary>
    ///  ReadData
    /// </summary>
    /// <returns>bool</returns>
    public bool ReadData() {
      return ReadData(ReadDataDelegate);
    }


    /// <inheritdoc />
    /// <summary>
    ///  ReadData
    /// </summary>
    /// <returns>bool</returns>
    public bool ReadData(Func<IControllerData> action) {
      // read data using controller specific protocol
      // ......
      // add some data to a queue (for test only)
      Monitor.Enter(_data);
      _data.Enqueue(action());
      Monitor.Exit(_data);

      EventHandle.Set();

      return _data.Count < 3;
    }


    /// <inheritdoc />
    /// <summary>
    ///  GetData
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool GetData(out IControllerData data) {
      // Wait on the EventWaitHandle.
      EventHandle.WaitOne();

      Monitor.Enter(_data);
      data = _data.Dequeue();
      Monitor.Exit(_data);

      EventHandle.Reset();

      return true;
    }


    /// <inheritdoc />
    /// <summary>
    ///  Dispose
    /// </summary>
    public virtual void Dispose() { }

    // ------------------------------------------------------------------------
    #endregion Methods
  }
}