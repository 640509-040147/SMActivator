using System;


namespace SMActivatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Activator act = new Activator();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "SmartBreak 2.4 Activator";
            const string title = @"
------------------------------------------------
 SmartBreak 2.4 Activator
 RCE by vikram
------------------------------------------------
 ";
            Console.Write(title);
            act.Activation();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Block the app in firewall.\n");
            System.Threading.Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" Enter any key to exit.");
            Console.ReadKey();
        }
    }
}
