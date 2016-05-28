using System;
using System.Collections.Generic;
using System.Linq;
using tilesim.Engine.Activities;

namespace tilesim.Engine.Entities
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

        public string ActivityText {
            get {
                if (Activity == null)
                    return String.Empty;
                else
                    return Activity.ToString ();
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

        public bool HasActivity(Type activityType)
        {
            var hasActivity = (from a in ActivityQueue
                                        where a.GetType () == activityType
                                        select a).Count () > 0;

            return hasActivity;
        }

        public BaseActivity GetActivity(Type activityType)
        {
            var activity = (from a in ActivityQueue
                where a.GetType () == activityType
                select a).SingleOrDefault();

            return activity;
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

