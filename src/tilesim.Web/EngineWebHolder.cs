using System;
using tilesim.Engine;
using System.Web;
using System.Threading;
using tilesim.Engine.Entities;

namespace tilesim.Web
{
    public class EngineWebHolder
    {
        public Thread EngineThread;

        private EngineContext context;
        public EngineContext Context
        {
            get {
                // TODO: Clean up
                return context;//(EngineContext)HttpContext.Current.Application["EngineContext"];
            }
            //set { // TODO: Should this property be read-only?
            //    HttpContext.Current.Application["EngineContext"] = value;
            //}
        }

        public bool IsStarted
        {
            get
            {
                return Context != null;
            }
        }

        public void StartGame(int gameSpeed)
        {
            var settings = EngineSettings.Default;

            settings.EngineId = Guid.NewGuid ().ToString ();
            settings.GameSpeed = gameSpeed;

            StartThread(settings);
        }

        public void StartGame(EngineSettings settings)
        {
            if (String.IsNullOrEmpty (settings.EngineId))
                settings.EngineId = Guid.NewGuid ().ToString ();

            if (settings.IsVerbose) {
                Console.WriteLine ("Starting game");
                Console.WriteLine ("  Engine ID: " + settings.EngineId);
                Console.WriteLine ("  Speed: " + settings.GameSpeed);
            }

            StartThread(settings);
        }

        public void ContinueGame(string gameId)
        {
            throw new NotImplementedException ();
            /*if (settings.IsVerbose) {
                Console.WriteLine ("Starting game");
                Console.WriteLine ("  Engine ID: " + settings.EngineId);
                Console.WriteLine ("  Speed: " + settings.GameSpeed);
            }

            ContinueThread(settings);*/
        }

        public void StartThread(EngineSettings settings)
        {
            if (settings.EngineId == String.Empty)
                settings.EngineId = Guid.NewGuid ().ToString ();

            Console.WriteLine ("Launching engine thread " + settings.EngineId);

            var context = CreateEngineContext (settings);

            AttachEngineContext(context);

            ThreadStart threadStart = delegate {
                context.Run();                
            };

            var engineThread = new Thread(threadStart);

            engineThread.IsBackground = true;
            engineThread.Start();
        }

        public EngineContext CreateEngineContext(EngineSettings settings)
        {
            if (settings.IsVerbose) {
                Console.WriteLine ("Creating engine context");
            }

            throw new NotImplementedException ();
           /* var context = EngineContext.New(settings);

            // TODO: Remove if not needed
            //context.Settings.EngineId = engineId;
            //context.Settings.EnableDataSaving = true;
            //context.Settings.GameSpeed = gameSpeed;
            //context.Settings.DefaultPeoplePerTile = 1;

            context.PopulateFromSettings();
            context.AddCompleteLogic();

            context.Initialize();

            return context;*/
        }

        public void AttachEngineContext(EngineContext context)
        {
            // TODO: Clean up
            this.context = context;
            //HttpContext.Current.Application["EngineContext"] = context;
        }

        static private EngineWebHolder current;
        static public EngineWebHolder Current
        {
            get {
                if (current == null)
                    current = new EngineWebHolder ();
                return current; }
            set { current = value; }
        }
    }
}

