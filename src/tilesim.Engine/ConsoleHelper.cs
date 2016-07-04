using System;
using tilesim.Engine.Entities;

namespace tilesim.Engine
{
    public class ConsoleHelper
    {
        public EngineSettings Settings { get; set; }

        public ConsoleHelper (EngineSettings settings)
        {
            Settings = settings;
        }

        public void WriteGameLine()
        {
            WriteGameLine ("");
        }

        public void WriteGameLine(string text)
        {
            if (Settings.OutputType == ConsoleOutputType.Game)
            {
                Console.WriteLine(text);
            }
        }

        public void WriteDebugLine(string text)
        {
            if (Settings.OutputType == ConsoleOutputType.Debug)
            {
                Console.WriteLine(text);
            }
        }

        public void ClearGame()
        {
            if (Settings.OutputType == ConsoleOutputType.Game) {
                Console.Clear ();
            }
        }
    }
}

