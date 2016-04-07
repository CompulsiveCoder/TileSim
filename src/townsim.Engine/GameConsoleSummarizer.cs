using System;
using townsim.Engine.Entities;

namespace townsim.Engine
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
            var console = Context.Console;

            // Player.ValidateProperties ();
            console.ClearGame ();
            console.WriteGameLine ("TownSim Engine");
            console.WriteGameLine ("  Engine Id: " + Context.Settings.EngineId + "     Speed: " + Context.Settings.GameSpeed);
            console.WriteGameLine ("  Real clock: " + Context.Clock.GetRealDurationString() + "   Game clock: " + Context.Clock.GetGameDurationString());

            console.WriteGameLine ("  Player:");
            console.WriteGameLine ("    Age: " + Convert.ToInt32(Context.Player.Age) + "    Gender:" + Context.Player.Gender + "    Health:" + Context.Player.Vitals[PersonVital.Health]);
            console.WriteGameLine ("    Thirst: " + Convert.ToInt32(Context.Player.Vitals[PersonVital.Thirst]) + "   Hunger:" + Convert.ToInt32(Context.Player.Vitals[PersonVital.Hunger]));
            console.WriteGameLine ("    Activity: " + Context.Player.ActivityName);
            console.WriteGameLine ("    Home: " + (Context.Player.Home != null ? Context.Player.Home.PercentComplete : 0) + "%");
            console.WriteGameLine ("    Inventory");
            console.WriteGameLine ("      Water: " + (int)Context.Player.Inventory[ItemType.Water] + "  Food: " + (int)Context.Player.Inventory[ItemType.Food] + "   Timber: " + (int)Context.Player.Inventory[ItemType.Timber] + "    Wood: " + (int)Context.Player.Inventory[ItemType.Wood] + " ");
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

            /*Console.WriteGameLine ("  Towns:");

            foreach (var town in Towns) {
                town.ValidateProperties ();
                Console.WriteDebugLine ("    " + town.Name);
                Console.WriteDebugLine ("     People:");
                Console.WriteDebugLine ("       Population: " + town.Population + "   Males: " + town.TotalMales + "   Females: " + town.TotalFemales + "   Couples: " + town.TotalCouples + "  Births: " + town.TotalBirths + "   Deaths: " + town.TotalDeaths);
                Console.WriteDebugLine ("       Immigrants: " + town.TotalImmigrants + "  Emigrants: " + town.TotalEmigrants);
                Console.WriteDebugLine ("       Average age: " + String.Format("{0:0.##}", town.AverageAge));
                Console.WriteDebugLine ("       Homeless: " + town.TotalHomelessPeople);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Activities:");
                Console.WriteDebugLine ("       Active: " + town.TotalActive + "   Inactive: " + town.TotalInactive);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Forestry:");
                Console.WriteDebugLine ("       Trees: " + town.Trees.Length + "        Forestry workers: " + town.TotalForestryWorkers);
                Console.WriteDebugLine ("       Trees planted today: " + town.CountTreesPlantedToday(Clock.GameDuration) + "   Trees planted: " + town.TotalTreesPlanted + "    Trees being planted: " + town.TotalTreesBeingPlanted);
                Console.WriteDebugLine ("       Average tree size: " + (int)town.AverageTreeSize + "   Average tree age: " + (int)town.AverageTreeAge);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Garden:");
                Console.WriteDebugLine ("       Vegetables: " + town.Vegetables.Length);
                Console.WriteDebugLine ("       Gardeners: " + town.TotalGardeners);
                Console.WriteDebugLine ("       Average vegetable size: " + (int)town.AverageVegetableSize);
                Console.WriteDebugLine ("       Average vegetable age: " + (int)town.AverageVegetableAge);
                Console.WriteDebugLine ("       Vegetables planted today: " + town.CountVegetablesPlantedToday(Clock.GameDuration));
                Console.WriteDebugLine ("       Vegetables planted: " + town.TotalVegetablesPlanted);
                Console.WriteDebugLine ("       Vegetables being planted: " + town.TotalVegetablesBeingPlanted);
                Console.WriteDebugLine ("       Vegetables harvested today: " + town.CountVegetablesHarvestedToday(Clock.GameDuration));
                Console.WriteDebugLine ("       Vegetables harvested: " + town.TotalVegetablesHarvested);
                Console.WriteDebugLine ("       Vegetables being harvested: " + town.TotalVegetablesBeingHarvested);
                Console.WriteDebugLine ();
                Console.WriteDebugLine ("     Buildings:");
                Console.WriteDebugLine ("       Builders: " + town.TotalBuilders);
                Console.WriteDebugLine ("       Houses (complete): " + town.Buildings.TotalCompletedHouses);
                Console.WriteDebugLine ("       Houses (under const.): " + town.Buildings.TotalIncompleteHouses);
                Console.WriteDebugLine ("       Average percent complete: " + (int)town.Buildings.AveragePercentComplete);
                if (town.Alerts.Length > 0) {
                    Console.WriteDebugLine ("     Alerts:");
                    foreach (var alert in town.Alerts) {
                        Console.WriteDebugLine ("       " + alert.Message);
                    }
                }
                
            }*/
        }
    }
}

