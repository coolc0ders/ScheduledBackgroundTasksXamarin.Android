using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ScheduledBackgroundTask
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private bool _scheduleRunning = false;
        private PendingIntent _pendingIntent;
        private AlarmManager _alarm;
        private int _interval = 5000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            Intent intent = new Intent(this, typeof(ScheduleReceiver));
            _pendingIntent = PendingIntent.GetBroadcast(this, 0, intent, 0);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            if (!_scheduleRunning)
            {
                //Starting the scheduled task
                _alarm = (AlarmManager)GetSystemService(AlarmService);
                _alarm.SetRepeating(AlarmType.RtcWakeup, DateTime.Now.TimeOfDay.Milliseconds, _interval, _pendingIntent);
                Toast.MakeText(this, "Schedule started", ToastLength.Short).Show();
            }
            else
            {
                if (_alarm != null)
                {
                    _alarm.Cancel(_pendingIntent);
                    Toast.MakeText(this, "Schedule stopped", ToastLength.Short).Show();
                }
            }
        }
	}
}

