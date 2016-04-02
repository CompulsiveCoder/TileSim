using System;
using System.Collections.Generic;

namespace townsim.Engine.Entities
{
    public partial class Person
    {
        public List<BaseActivity> ActivityQueue = new List<BaseActivity>();

        public string ActivityName {
            get {
                if (Activity == null)
                    return String.Empty;
                else
                    return Activity.GetType ().Name;
            }
        }

        public BaseActivity Activity {
            get {
                if (ActivityQueue.Count == 0)
                    return null;
                else
                    return ActivityQueue [0];   
            }
        }

        public void RushActivity(BaseActivity activity)
        {
            ActivityQueue.Insert(0, activity);
        }

        public void AddActivity(BaseActivity activity)
        {
            ActivityQueue.Add (activity);
        }

        public void FinishedActivity(BaseActivity activity)
        {
            ActivityQueue.Remove (activity);
        }
    }
}

