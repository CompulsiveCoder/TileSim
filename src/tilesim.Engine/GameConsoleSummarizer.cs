using System;
using tilesim.Engine.Entities;

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

                // Player.ValidateProperties ();
                console.ClearGame ();
                console.WriteGameLine ("TileSim Engine");
                console.WriteGameLine ("  Engine Id: " + Context.Settings.EngineId + "     Speed: " + Context.Settings.GameSpeed);
                console.WriteGameLine ("  Real clock: " + Context.Clock.GetRealDurationString () + "   Game clock: " + Context.Clock.GetGameDurationString ());

                console.WriteGameLine ("  Player:");
                console.WriteGameLine ("    Age: " + Convert.ToInt32 (Context.Player.Age) + "    Gender:" + Context.Player.Gender + "    Health:" + Context.Player.Vitals [PersonVital.Health]);
                console.WriteGameLine ("    Thirst: " + Convert.ToInt32 (Context.Player.Vitals [PersonVital.Thirst]) + "   Hunger:" + Convert.ToInt32 (Context.Player.Vitals [PersonVital.Hunger]));
                console.WriteGameLine ("    Home: " + (Context.Player.Home != null ? (int)Context.Player.Home.PercentComplete : 0) + "%");
                console.WriteGameLine ("    Activity: " + (Context.Player.Activity != null ? Context.Player.Activity.ToString() : "[idle]"));
                console.WriteGameLine ("    Inventory");
                console.WriteGameLine ("      Water: " + (int)Context.Player.Inventory [ItemType.Water] + "  Food: " + (int)Context.Player.Inventory [ItemType.Food] + "   Timber: " + (int)Context.Player.Inventory [ItemType.Timber] + "    Wood: " + (int)Context.Player.Inventory [ItemType.Wood] + " ");
                console.WriteGameLine ("    Needs");
                foreach (var need in Context.Player.Needs) {
                    console.WriteGameLine ("      " + need.ActionType + " " + need.ItemType + ": " + need.Quantity + " (" + need.Priority + ")");
                }

                /*console.WriteGameLine ("   Priorities:");
            console.WriteGameLine ("     Water: " + (int)Context.Player.Priorities[PriorityTypes.Water] + "%      Food: " + (int)Context.Player.Priorities[PriorityTypes.Food] + "%     Shelter: " + (int)Player.Priorities[PriorityTypes.Shelter] + "%");

            console.WriteGameLine ();
            console.WriteGameLine ("   Supplies:");
            console.WriteGameLine ("     Water: " + (int)Context.Player.Supplies[needTypes.Water] + "ml      Food: " + Context.Player.Supplies[needTypes.Food] + " kgs     Wood: " + (int)Player.Supplies[NeedType.Wood] + "    Timber: " + (int)Player.Supplies[NeedType.Timber]);

            console.WriteGameLine ();
            console.WriteGameLine ("   Demands:");
            console.WriteGameLine ("     Water: " + (int)Context.Player.GetDemandAmount(needTypes.Water) + "ml      Food: " + Context.Player.GetDemandAmount(needTypes.Food) + " kgs     Wood: " + (int)Player.GetDemandAmount(NeedType.Wood) + "    Timber: " + (int)Player.GetDemandAmount(NeedType.Timber));
*/
                console.WriteGameLine ();

                var tile = Context.Player.Tile;

                console.WriteGameLine ("  Current tile:");
                console.WriteGameLine ("     People:");
                console.WriteGameLine ("       Population: " + tile.People.Length);
                console.WriteGameLine ("       Items:");

                var inventoryString = "";

                foreach (var itemType in tile.Inventory.Items.Keys) {
                    inventoryString += itemType + ": " + tile.Inventory[itemType] + "  ";
                }

                console.WriteGameLine ("         " + inventoryString);
                /*Console.WriteGameLine ("  Tiles:");

            foreach (var tile in Tiles) {
                tile.ValidateProperties ();
                Console.WriteDebugLine ("    " + tile.Name);
                Console.WriteDebugLine ("     People:");
                Console.WriteDebugLine ("       Population: " + tile.Population + "   Males: " + tile.TotalMales + "   Females: " + tile.TotalFemales + "   Couples: " + tile.TotalCouples + "  Births: " + tile.TotalBirths + "   Deaths: " + tile.TotalDeaths);
                Console.WriteDebugLine ("       Immigrants: " + tile.TotalImmigrants + "  Emigrants: " + tile.TotalEmigrants);
                Console.WriteDebugLine ("       Average age: " + String.Format("{0:0.##}", tile.AverageAge));
                Console.WriteDebugLine ("       Homeless: " + tile.TotalHomelessPeople);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Activities:");
                Console.WriteDebugLine ("       Active: " + tile.TotalActive + "   Inactive: " + tile.TotalInactive);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Forestry:");
                Console.WriteDebugLine ("       Trees: " + tile.Trees.Length + "        Forestry workers: " + tile.TotalForestryWorkers);
                Console.WriteDebugLine ("       Trees planted today: " + tile.CountTreesPlantedToday(Clock.GameDuration) + "   Trees planted: " + tile.TotalTreesPlanted + "    Trees being planted: " + tile.TotalTreesBeingPlanted);
                Console.WriteDebugLine ("       Average tree size: " + (int)tile.AverageTreeSize + "   Average tree age: " + (int)tile.AverageTreeAge);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Garden:");
                Console.WriteDebugLine ("       Vegetables: " + tile.Vegetables.Length);
                Console.WriteDebugLine ("       Gardeners: " + tile.TotalGardeners);
                Console.WriteDebugLine ("       Average vegetable size: " + (int)tile.AverageVegetableSize);
                Console.WriteDebugLine ("       Average vegetable age: " + (int)tile.AverageVegetableAge);
                Console.WriteDebugLine ("       Vegetables planted today: " + tile.CountVegetablesPlantedToday(Clock.GameDuration));
                Console.WriteDebugLine ("       Vegetables planted: " + tile.TotalVegetablesPlanted);
                Console.WriteDebugLine ("       Vegetables being planted: " + tile.TotalVegetablesBeingPlanted);
                Console.WriteDebugLine ("       Vegetables harvested today: " + tile.CountVegetablesHarvestedToday(Clock.GameDuration));
                Console.WriteDebugLine ("       Vegetables harvested: " + tile.TotalVegetablesHarvested);
                Console.WriteDebugLine ("       Vegetables being harvested: " + tile.TotalVegetablesBeingHarvested);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Buildings:");
                Console.WriteDebugLine ("       Builders: " + tile.TotalBuilders);
                Console.WriteDebugLine ("       Houses (complete): " + tile.Buildings.TotalCompletedHouses);
                Console.WriteDebugLine ("       Houses (under const.): " + tile.Buildings.TotalIncompleteHouses);
                Console.WriteDebugLine ("       Average percent complete: " + (int)tile.Buildings.AveragePercentComplete);
                if (tile.Alerts.Length > 0) {
                    Console.WriteDebugLine ("     Alerts:");
                    foreach (var alert in tile.Alerts) {
                        Console.WriteDebugLine ("       " + alert.Message);
                    }
                }
                
            }*/
            }
        }
    }
}

