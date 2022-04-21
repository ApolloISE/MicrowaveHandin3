using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output, 800);

            Light light = new Light(output);
            Buzzer buzzer = new Buzzer(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, buzzer, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            for (int i = 0; i < 800/50; i++)
            {
                powerButton.Press();
            }
            

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            while (true)
            {
                var ch = Console.ReadLine()[0];
                 if (ch == 'c')
                 {
                     timeButton.Press();
                 }
                
            }

            System.Console.ReadLine();
        }
    }
}
