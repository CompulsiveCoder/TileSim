using System;
using tilesim.Engine.Entities;
using datamanager.Data;

namespace tilesim.Engine
{
    public class EngineClient
    {
        public bool IsVerbose = false;

        public DataManager Data { get;set; }

        public EngineClient ()
        {
            Data = new DataManager();
            Data.Settings.Prefix = "tilesim";
            Data.Settings.IsVerbose = IsVerbose;
        }

        public EngineClient (DataManager data)
        {
            Data = data;
        }

        public EngineContext New()
        {
            if (IsVerbose)
                Console.WriteLine ("Creating new game engine");

            var engineId = CreateNewEngineId ();

            Data.Settings.Prefix = "tilesim-" + engineId;

            var settings = CreateDefaultSettings (engineId);

            var context = CreateEngineContext (settings);

            context.SaveInfo ();

            return context;
        }

        public EngineInfo[] ListEngines()
        {
            return Data.Get<EngineInfo> ();
        }

        public EngineSettings CreateDefaultSettings(string engineId)
        {
            if (IsVerbose) {
                Console.WriteLine ("  Creating default settings");
            }
            
            var settings = EngineSettings.Default;

            if (IsVerbose)
                settings = EngineSettings.DefaultVerbose;

            settings.EngineId = engineId;

            if (IsVerbose) {
                Console.WriteLine ("    Engine ID: " + settings.EngineId);
                Console.WriteLine ("    Game speed: " + settings.GameSpeed);
            }

            return settings;
        }

        public EngineContext CreateEngineContext(EngineSettings settings)
        {
            if (IsVerbose) {
                Console.WriteLine ("  Creating engine context");
            }

            var context = EngineContext.New(settings, Data);

            context.PopulateFromSettings();
            context.AddCompleteLogic();

            context.Initialize();

            return context;
        }

        public string CreateNewEngineId()
        {
            var fullId = Guid.NewGuid ().ToString ();

            var shortId = fullId.Substring (0, fullId.IndexOf ("-"));

            return shortId;
        }

    }
}

