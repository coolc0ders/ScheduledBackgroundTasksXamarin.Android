﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ScheduledBackgroundTask
{
    [BroadcastReceiver]
    public class ScheduleReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Received intent! You can run your background task here.", ToastLength.Short).Show();
        }
    }
}