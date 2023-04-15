using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HW_290323
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TV tv = new TV();
			ICommand command = new TVOnCommand(tv); // specific command
			Invoker(command, true);
			Microwave microwave = new Microwave();
			command = new MicrowaveCommand(microwave, 5000);
			Invoker(command, false);

			Console.ReadLine();
		}

		static void Invoker(ICommand command, bool undo)
		{
			Controller controller = new Controller();
			controller.SetCommand(command);
			controller.PressButton();
			if (undo) {
				controller.PressUndo();
			}
		}
	}

	class TV
	{
		public void On()
		{
			Console.WriteLine("TV is on!");
		}

		public void Off()
		{
			Console.WriteLine("TV is off!");
		}
	}

	class Microwave
	{
		public void StartCooking(int time)
		{
			Console.WriteLine("Warming up food...");
			System.Threading.Thread.Sleep(time);
		}

		public void StopCooking()
		{
			Console.WriteLine("The food is warmed up!");
		}
	}

	interface ICommand
	{
		void Execute();
		void Undo();
	}

	class TVOnCommand : ICommand
	{
		private TV tv;

		public TVOnCommand(TV tvSet)
		{
			tv = tvSet;
		}

		public void Execute()
		{
			tv.On();
		}

		public void Undo()
		{
			tv.Off();
		}
	}

	class MicrowaveCommand : ICommand
	{
		private Microwave microwave;
		private int time;

		public MicrowaveCommand(Microwave m, int t)
		{
			microwave = m;
			time = t;
		}

		public void Execute()
		{
			microwave.StartCooking(time);
			microwave.StopCooking();
		}

		public void Undo()
		{
			microwave.StopCooking();
		}
	}

	class Controller
	{
		private ICommand command;

		public void SetCommand(ICommand com)
		{
			command = com;
		}

		public void PressButton()
		{
			if (command != null) {
				command.Execute();
			}
		}

		public void PressUndo()
		{
			if (command != null) {
				command.Undo();
			}
		}
	}
}