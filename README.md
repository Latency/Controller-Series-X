# Async Driver for Controller Series-X

---

## Console Driver PoC

* CREATED BY:   [Latency McLaughlin]
* FRAMEWORK:    [.NET] v[4.7.1] & Core v[[2.0]](https://www.microsoft.com/net/download/windows)
* LANGUAGE:     [C#] (v7.0)
* GFX SUBSYS:   [Console]
* SUPPORTS:     [Visual Studio] 2017, 2015, 2013, 2012, 2010, 2008
* UPDATED:      1/5/2018
* VERSION:      1.0.0
* TAGS:         [.NET], [C#]


## Navigation
* <a href="#overview">Overview</a>
* <a href="#example">Example</a>
* <a href="#references">References</a>
* <a href="#license">License</a>

<hr>

<h2><a name="overview">Overview</a></h2>

Make modifications to the existing program doing whatever is necessary.

<h2><a name="example">Example</a></h2>

```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP {
	/// <summary>
	/// Improve the code below. 
	/// </summary>
	class Program {

		class Series3Controller {
			public void Connect(string address) {
				// connect to series 3 controller
			}
			public bool ReadData() {
				// read data using controller specific protocol
				// ......
				// add some data to a queue (for test only)
				_Data.Enqueue(new Tuple<string, double>("Item1", 1));
				return _Data.Count < 3;
			}
			public bool GetData(out string Name, out double Value) {
				Name = string.Empty;
				Value = 0;
				if (_Data.Count == 0)
					return false;
				Tuple<string, double> data = _Data.Dequeue();
				Name = data.Item1;
				Value = data.Item2;
				return true;
			}
			public Queue<Tuple<string, double>> _Data = new Queue<Tuple<string, double>>();
		}

		class Series5Controller {
			public void Connect(string address) {
				// connect to series 5 controller
			}
			public bool ReadData() {
				// read data using controller specific protocol
				// ......
				// add some data to a queue (for test only)
				_Data.Enqueue(new Tuple<string, double>("Item2", 2));
				return _Data.Count < 3;
			}
			public bool GetData(out string Name, out double Value) {
				Name = string.Empty;
				Value = 0;
				if (_Data.Count == 0)
					return false;
				Tuple<string, double> data = _Data.Dequeue();
				Name = data.Item1;
				Value = data.Item2;
				return true;
			}
			public Queue<Tuple<string, double>> _Data = new Queue<Tuple<string, double>>();
		}

		class Series6Controller {
			public void Connect(string address) {
				// connect to series 6 controller
			}
			public bool ReadData() {
				// read data using controller specific protocol
				// ......
				// add some data to a queue (for test only)
				_Data.Enqueue(new Tuple<string, double>("Item3", 3));
				return _Data.Count < 3;
			}
			public bool GetData(out string Name, out double Value) {
				Name = string.Empty;
				Value = 0;
				if (_Data.Count == 0)
					return false;
				Tuple<string, double> data = _Data.Dequeue();
				Name = data.Item1;
				Value = data.Item2;
				return true;
			}
			public Queue<Tuple<string, double>> _Data = new Queue<Tuple<string, double>>();
		}
		/// <summary>
		/// Communication server
		/// </summary>
		class CommServer {
			public Series3Controller S3controller = new Series3Controller();
			public Series5Controller S5controller = new Series5Controller();
			public Series6Controller S6controller = new Series6Controller();

			public void Connect(string address) {
				if (address.StartsWith("s3."))
					S3controller.Connect(address);
				else if (address.StartsWith("s5."))
					S5controller.Connect(address);
				else if (address.StartsWith("s6."))
					S6controller.Connect(address);
			}
			public void ReadData() {
				while (S3controller.ReadData());
				while (S5controller.ReadData());
				while (S6controller.ReadData());
			}
			public void ProcessData() {
				string Name;
				double Value;
				while (S3controller.GetData(out Name, out Value))
					SendToClient(Name, Value);
				while (S5controller.GetData(out Name, out Value))
					SendToClient(Name, Value);
				while (S6controller.GetData(out Name, out Value))
					SendToClient(Name, Value);
			}

			public void SendToClient(string Name, double Value) {
				Console.WriteLine(string.Format("Name={0}", Value));
				// send data to client
			}
		}

		static void Main(string[] args) {
			CommServer server = new CommServer();

			server.ReadData();

			server.ProcessData();
		}

	};
}
```

<h2><a name="references">References</a></h2>

 [.NET], [IoC], [DI], [Generics], [Delegates], [Parametric Polymorphism]

<h2><a name="license">License</a></h2>

[GNU LESSER GENERAL PUBLIC LICENSE] - Version 3, 29 June 2007


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job.)

   [GNU LESSER GENERAL PUBLIC LICENSE]: <http://www.gnu.org/licenses/lgpl-3.0.en.html>
   [Comparison]: <https://en.wikipedia.org/wiki/Comparison_of_C_Sharp_and_Java>
   [Console]: <https://www.techopedia.com/definition/25593/console-application-c>
   [TaskEvent.cs]: <https://github.com/Latency/ORM-Monitor/blob/master/DLL/TaskEvent.cs>
   [NuGet]: <https://www.nuget.org/packages/ORM-Monitor/>
   [.NET]: <https://en.wikipedia.org/wiki/.NET_Framework/>
   [WPF]: <https://en.wikipedia.org/wiki/Windows_Presentation_Foundation/>
   [Visual Studio]: <https://en.wikipedia.org/wiki/Microsoft_Visual_Studio/>
   [Latency McLaughlin]: <https://www.linkedin.com/in/Latency/>
   [API]: <https://en.wikipedia.org/wiki/Application_programming_interface>
   [AOP]: <https://en.wikipedia.org/wiki/Aspect-oriented_programming>
   [Parametric Polymorphism]: <https://en.wikipedia.org/wiki/Parametric_polymorphism>
   [ORM-Monitor]: <https://github.com/Latency/ORM-Monitor/>
   [TAP]: <https://msdn.microsoft.com/en-us/library/hh873175(v=vs.110).aspx>
   [AMI]: <https://en.wikipedia.org/wiki/Asynchronous_method_invocation>
   [TPL]: <https://msdn.microsoft.com/en-us/library/dd460717(v=vs.110).aspx>
   [ORM]: <https://en.wikipedia.org/wiki/Object-relational_mapping>
   [C#]: <https://en.wikipedia.org/wiki/C_Sharp_(programming_language)>
   [DLL]: <https://en.wikipedia.org/wiki/Dynamic-link_library>
   [MVC]: <https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller>
   [MVA]: <https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93adapter>
   [CMS]: <https://en.wikipedia.org/wiki/Content_management_system>
   [IoC]: <https://msdn.microsoft.com/en-us/library/ff921087.aspx>
   [DI]: <https://en.wikipedia.org/wiki/Dependency_injection>
   [Generics]: <https://en.wikipedia.org/wiki/Generic_programming>
   [Delegates]: <https://msdn.microsoft.com/en-us/library/ms173171.aspx>
   [EventHandlers]: <https://msdn.microsoft.com/en-us/library/2z7x8ys3(v=vs.90).aspx>
   [NUnit]: <https://en.wikipedia.org/wiki/NUnit>
   [lambda expression]: <https://msdn.microsoft.com/en-us/library/bb397687.aspx>