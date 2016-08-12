using System;
using System.Collections.Generic;
using tilesim.Engine.Activities;

namespace tilesim.Engine
{
    public class PersonSettings
    {
        public Dictionary<string, bool> AllowAutomatic = new Dictionary<string, bool>();

        public PersonSettings ()
        {
            AllowAutomatic.Add (typeof(BuildShelterActivity).Name, true);
            AllowAutomatic.Add (typeof(MillTimberActivity).Name, true);
            AllowAutomatic.Add (typeof(FellWoodActivity).Name, true);
            AllowAutomatic.Add (typeof(DrinkWaterActivity).Name, true);
            AllowAutomatic.Add (typeof(GatherWaterActivity).Name, true);
            AllowAutomatic.Add (typeof(EatFoodActivity).Name, true);
            AllowAutomatic.Add (typeof(GatherFoodActivity).Name, true);
            AllowAutomatic.Add (typeof(SleepActivity).Name, true);
        }

        static public PersonSettings Autonomous {
            get
            {
                var settings = new PersonSettings ();

                settings.AllowAutomatic[typeof(BuildShelterActivity).Name] = true;
                settings.AllowAutomatic[typeof(MillTimberActivity).Name] = true;
                settings.AllowAutomatic[typeof(FellWoodActivity).Name] = true;
                settings.AllowAutomatic[typeof(DrinkWaterActivity).Name] = true;
                settings.AllowAutomatic[typeof(GatherWaterActivity).Name] = true;
                settings.AllowAutomatic[typeof(EatFoodActivity).Name] = true;
                settings.AllowAutomatic[typeof(GatherFoodActivity).Name] = true;
                settings.AllowAutomatic[typeof(SleepActivity).Name] = true;

                return settings;
            }
        }

        static public PersonSettings SemiAutonomous
        {
            get
            {   
                var settings = new PersonSettings ();

                settings.AllowAutomatic[typeof(BuildShelterActivity).Name] = false;
                settings.AllowAutomatic[typeof(MillTimberActivity).Name] = false;
                settings.AllowAutomatic[typeof(FellWoodActivity).Name] = false;
                settings.AllowAutomatic[typeof(DrinkWaterActivity).Name] = true;
                settings.AllowAutomatic[typeof(GatherWaterActivity).Name] = true;
                settings.AllowAutomatic[typeof(EatFoodActivity).Name] = true;
                settings.AllowAutomatic[typeof(GatherFoodActivity).Name] = true;
                settings.AllowAutomatic[typeof(SleepActivity).Name] = true;

                return settings;
            }
        }

        static public PersonSettings Manual
        {
            get {
                var settings = new PersonSettings ();

                settings.AllowAutomatic[typeof(BuildShelterActivity).Name] = false;
                settings.AllowAutomatic[typeof(MillTimberActivity).Name] = false;
                settings.AllowAutomatic[typeof(FellWoodActivity).Name] = false;
                settings.AllowAutomatic[typeof(DrinkWaterActivity).Name] = false;
                settings.AllowAutomatic[typeof(GatherWaterActivity).Name] = false;
                settings.AllowAutomatic[typeof(EatFoodActivity).Name] = false;
                settings.AllowAutomatic[typeof(GatherFoodActivity).Name] = false;
                settings.AllowAutomatic[typeof(SleepActivity).Name] = false;

                return settings;
            }
        }
    }
}

