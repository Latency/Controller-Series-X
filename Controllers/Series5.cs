//  *****************************************************************************
//  File:       Series5.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using OOP.Interfaces;
using OOP.Types;

namespace OOP.Controllers {
  /// <inheritdoc />
  public sealed class Series5 : Controller {
    /// <inheritdoc />
    /// <summary>
    ///  Constructor
    /// </summary>
    public Series5() : base(2) { }


    /// <inheritdoc />
    /// <summary>
    ///  InitializeData
    /// </summary>
    /// <returns></returns>
    protected override IControllerData InitializeData() {
      return ControllerType = new TSeries5();
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
      return ControllerType;
    }
  }
}