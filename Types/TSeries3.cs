//  *****************************************************************************
//  File:       TSeries3.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using System;
using OOP.Interfaces;

namespace OOP.Types {
  /// <summary>
  ///   TSeries5
  /// </summary>
  // ReSharper disable once InconsistentNaming
  public class TSeries3 : IControllerData {
    public TSeries3() {
      Name = GetType().Name.Substring(1);
      Value = Convert.ToDouble(Name.Substring(Name.IndexOfAny("0123456789".ToCharArray())));
    }

    public string Name { get; set; }
    public double Value { get; set; }

    public override string ToString() {
      return Name;
    }
  }
}