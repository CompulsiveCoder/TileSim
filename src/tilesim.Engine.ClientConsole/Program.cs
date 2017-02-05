using System;

namespace tilesim.Engine.ClientConsole
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            var arguments = new Arguments (args);

            if (arguments.KeylessArguments.Length >= 1)
                HandleCommand (arguments);
            else
                Help ();
        }

        public static void HandleCommand(Arguments arguments)
        {
            var cmd = arguments.KeylessArguments[0].ToLower();

            var client = new EngineClient ();

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
            foreach (var engineInfo in client.ListEngines()) {
                Console.WriteLine (engineInfo.ToString ());
            }
        }
    }
}
