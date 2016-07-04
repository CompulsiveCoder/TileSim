using System;
using tilesim.Engine.Entities;
using tilesim.Engine;
using System.Linq;

namespace tilesim.Engine
{
    // TODO: Come up with a better class name
    public class GameConsoleSummarizer
    {
        public EngineContext Context { get; set; }

        public GameConsoleSummarizer (EngineContext context)
        {
            Context = context;
        }

        public void WriteSummary()
        {
            if (Context.Settings.OutputType == ConsoleOutputType.Game) {
                var console = Context.Console;

                // TODO: Remove if not needed
                // Player.ValidateProperties ();
                console.ClearGame ();
                console.WriteGameLine ("TileSim Engine");
                console.WriteGameLine ("  Engine Id: " + Context.Settings.EngineId + "     Speed: " + Context.Settings.GameSpeed);
                console.WriteGameLine ("  Real clock: " + Context.Clock.GetRealDurationString () + "   Game clock: " + Context.Clock.GetGameDurationString ());

                console.WriteGameLine ("  Player:");
                console.WriteGameLine ("    Is Alive? " + (Context.Player.IsAlive ? "Yes" : "No"));
                console.WriteGameLine ("    Age: " + Convert.ToInt32 (Context.Player.Age) + "    Gender:" + Context.Player.Gender + "    Health:" + Context.Player.Vitals [PersonVitalType.Health]);
                console.WriteGameLine ("    Energy: " + Convert.ToInt32 (Context.Player.Vitals [PersonVitalType.Energy]) + "   Thirst: " + Convert.ToInt32 (Context.Player.Vitals [PersonVitalType.Thirst]) + "   Hunger:" + Convert.ToInt32 (Context.Player.Vitals [PersonVitalType.Hunger]));
                console.WriteGameLine ("    Home: " + (Context.Player.Home != null ? (int)Context.Player.Home.PercentComplete : 0) + "%");
                console.WriteGameLine ("    Activity: " + (Context.Player.Activity != null ? Context.Player.Activity.ToString() : "[idle]"));
                console.WriteGameLine ("    Inventory");
                console.WriteGameLine ("      Water: " + (int)Context.Player.Inventory [ItemType.Water] + "  Food: " + (int)Context.Player.Inventory [ItemType.Food] + "   Timber: " + (int)Context.Player.Inventory [ItemType.Timber] + "    Wood: " + (int)Context.Player.Inventory [ItemType.Wood] + " ");
                console.WriteGameLine ("    Needs:\t\t\tamount\t(priority)");
                foreach (var need in Context.Player.Needs.OrderByDescending(o=>o.Priority)) {
                    console.WriteGameLine ("      " + need.ActionType + " " + need.ItemType + ":\t\t" + (int)need.Quantity + "\t(" + need.Priority + ")");
                }

                console.WriteGameLine ();

                var tile = Context.Player.Tile;

                console.WriteGameLine ("  Current tile:");
                console.WriteGameLine ("     People:");
                console.WriteGameLine ("       Population: " + tile.People.Length);
                console.WriteGameLine ("       Trees: " + tile.Trees.Length);
                console.WriteGameLine ("       Items:");

                var inventoryString = "";

                foreach (var itemType in tile.Inventory.Items.Keys) {
                    inventoryString += itemType + ": " + tile.Inventory[itemType] + "  ";
                }

                console.WriteGameLine ("         " + inventoryString);
            }
        }
    }
}

