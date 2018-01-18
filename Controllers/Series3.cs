//  *****************************************************************************
//  File:       Series3.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using System;
using System.Threading;
using OOP.Interfaces;
using OOP.Types;

namespace OOP.Controllers {
  /// <inheritdoc />
  public sealed class Series3 : Controller {
    /// <inheritdoc />
    /// <summary>
    ///  Constructor
    /// </summary>
    public Series3() : base(0) { }


    /// <inheritdoc />
    /// <summary>
    ///  InitializeData
    /// </summary>
    /// <returns></returns>
    protected override IControllerData InitializeData() {
      return new TSeries3();
    }


    /// <inheritdoc />
    /// <summary>
    ///  ReadDataDelegate
    /// </summary>
    /// <returns></returns>
    public override IControllerData ReadDataDelegate() {
      // --------------------------------------
      // Provide implementation here.
      // --------------------------------------
      Console.WriteLine($"Reading data from {ControllerType.Name}");

      Thread.Sleep(2000);

      return ControllerType;
    }
  }
}