using System;

namespace tilesim.Engine.ClientConsole
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine("tilesim");
            
            var arguments = new Arguments (args);

            if (arguments.KeylessArguments.Length >= 1)
                HandleCommand (arguments);
            else
                Help ();
        }

        public static void HandleCommand(Arguments arguments)
        {
            var cmd = arguments.KeylessArguments[0].ToLower();
            
            Console.WriteLine("Command: " + cmd);

            var client = new EngineClient();
            
            client.IsVerbose = arguments.Contains("v");

            switch (cmd) {
            case "new":
                client.New ();
                break;
            case "list":
                List (client);
                break;
            }
        }

        public static void Help()
        {
            throw new NotImplementedException ();            
        }

        public static void List(EngineClient client)
        {
            var engines = client.ListEngines();
            Console.WriteLine("Game Engines:");
            foreach (var engineInfo in engines) {
                Console.WriteLine (engineInfo.ToString ());
            }
            if (engines.Length == 0)
                Console.WriteLine("  No engines found. Use the \"new\" command.");
        }
    }
}
