using System;
using UnityEngine;
using SgLib;

namespace Assets.SimpleAndroidNotifications
{
    public class NotificationExample : MonoBehaviour
    {
        public static NotificationExample NH;
        public static GameObject NHGO;

        public string AndroidURL;
        public string IOSURL;

        public static int Notifications;

        void Awake(){
            
        }

        void Start()
        {
            NHGO = this.gameObject;
            NH = NHGO.GetComponent<NotificationExample>();
            Notifications = PlayerPrefs.GetInt("Notifications");
        }

        public void Rate()
        {
            #if UNITY_ANDROID
            Application.OpenURL(AndroidURL);
            #elif UNITY_IOS
            Application.OpenURL(IOSURL);
            #endif
        }

        public void OnApplicationQuit()
        {
            ScheduleSimple();
        }

        public void OnApplicationPause(bool paused)
        {
            if (paused)
            {
                ScheduleSimple();
            }else{
//                ScheduleNormal();
            }
        }

        public void ScheduleSimple()
        {
            NotificationManager.SendWithAppIcon(TimeSpan.FromHours(6), "Prison Payback", "The guards need some payback! Ready to give some more?", new Color(1, 0.3f, 0.15f));
        }

        public void ScheduleNormal()
        {
            NotificationManager.SendWithAppIcon(DailyRewardController.Instance.TimeUntilReward, "Your Reward is Ready!", "Come back to see what you get!", new Color(0, 0.6f, 1), NotificationIcon.Message);
        }

        public void ScheduleCustom()
        {
            var notificationParams = new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = TimeSpan.FromSeconds(5),
                Title = "Custom notification",
                Message = "Message",
                Ticker = "Ticker",
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = NotificationIcon.Heart,
                SmallIconColor = new Color(0, 0.5f, 0),
                LargeIcon = "app_icon"
            };

            NotificationManager.SendCustom(notificationParams);
        }

        public void CancelAll()
        {
            NotificationManager.CancelAll();
        }
    }
}