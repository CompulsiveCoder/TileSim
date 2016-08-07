using System;
using tilesim.Engine;
using System.Web;
using System.Threading;

namespace tilesim.Web
{
    public class EngineWebHolder
    {
        public Thread EngineThread;
        
        public EngineContext Context
        {
            get {
                return (EngineContext)HttpContext.Current.Application["EngineContext"];
            }
            set { // TODO: Should this property be read-only?
                HttpContext.Current.Application["EngineContext"] = value;
            }
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
            StartThread(Guid.NewGuid().ToString(), gameSpeed);
        }

        public void StartThread(string engineId, int gameSpeed)
        {
            if (engineId == String.Empty)
                engineId = Guid.NewGuid ().ToString ();

            Console.WriteLine ("Launching engine thread " + engineId);

            var context = EngineContext.New();
            context.Settings.GameSpeed = gameSpeed;
            context.PopulateFromSettings();
            context.AddCompleteLogic();

            context.Initialize();

            Attach(context);

            ThreadStart threadStart = delegate {
                context.Run();                
            };

            var engineThread = new Thread(threadStart);

            engineThread.IsBackground = true;
            engineThread.Start();
        }

        public void Attach(EngineContext context)
        {
            Context = context;
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

