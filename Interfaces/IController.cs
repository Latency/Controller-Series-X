//  *****************************************************************************
//  File:       IController.cs
//  Solution:   Test
//  Project:    OOP
//  Date:       01/10/2018
//  Author:     Latency McLaughlin
//  Copywrite:  Bio-Hazard Industries - 1998-2018
//  *****************************************************************************

using System;

namespace OOP.Interfaces {
  internal interface IController : IDisposable {
    /// <summary>
    ///   Connect
    /// </summary>
    /// <param name="address"></param>
    void Connect(string address);

    /// <summary>
    ///  ReadData
    /// </summary>
    /// <returns></returns>
    bool ReadData();

    /// <summary>
    ///  ReadData
    /// </summary>
    /// <returns></returns>
    bool ReadData(Func<IControllerData> data);

    /// <summary>
    ///  GetData
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    bool GetData(out IControllerData data);
  }
}