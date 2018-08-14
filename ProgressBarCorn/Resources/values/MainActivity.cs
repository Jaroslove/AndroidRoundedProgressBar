using Android.App;
using Android.Widget;
using Android.OS;

namespace ProgressBarCorn
{
    [Activity(Label = "ProgressBar", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        
        private RoundProgressBar mRoundCornerProgress;
        //private ProgressBar mSysProgress;
        private Handler mDelayHandler = new Handler();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            mRoundCornerProgress = (RoundProgressBar)FindViewById(Resource.Id.pb_view);
            mRoundCornerProgress.SetProgress(50);
        }
    }
}

