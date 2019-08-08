using UnityEngine;
using System.Collections;
using System;
using ChartboostSDK;

namespace SgLib
{

    public class DailyRewardController : MonoBehaviour
    {
        public static DailyRewardController Instance { get; private set; }

        public DateTime NextRewardTime
        {
            get
            {
                return GetNextRewardTime();
            }
        }

        public TimeSpan TimeUntilReward
        {
            get
            {
                return NextRewardTime.Subtract(DateTime.Now);
            }
        }

        [Header("Check to disable Daily Reward Feature")]
        public bool disable;

        [Header("Daily Reward Config")]
        [Tooltip("Number of hours between 2 rewards")]
        public int rewardIntervalHours = 6;
        [Tooltip("Number of minues between 2 rewards")]
        public int rewardIntervalMinutes = 0;
        [Tooltip("Number of seconds between 2 rewards")]
        public int rewardIntervalSeconds = 0;
        public int rareChance;
        public int minRewardvalueCurrent = 20;
        public int maxRewardvalueCurrent = 50;

        private const string NextRewardTimePPK = "SGLIB_NEXT_DAILY_REWARD_TIME";

        void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            Chartboost.cacheInterstitial(CBLocation.Default);

        }

        /// <summary>
        /// Determines whether the waiting time has passed and can reward now.
        /// </summary>
        /// <returns><c>true</c> if this instance can reward now; otherwise, <c>false</c>.</returns>
        public bool CanRewardNow()
        {
            return TimeUntilReward <= TimeSpan.Zero;
        }

        /// <summary>
        /// Gets the random reward.
        /// </summary>
        /// <returns>The random reward.</returns>
        public int GetRandomReward()
        {
            if (UnityEngine.Random.Range(0, 20) == 5)
            {
                reward = 300;
            }
            else
            {
                reward = UnityEngine.Random.Range(minRewardvalueCurrent, maxRewardvalueCurrent + 1);
            }
            StartCoroutine(ShowAdThenReward());
            return reward;

        }
        public void ShowAd(){
            StartCoroutine(ShowAdThenReward());
        }
        int reward;
        public IEnumerator ShowAdThenReward()
        {
            yield return new WaitForEndOfFrame();
            if (Chartboost.hasInterstitial(CBLocation.Default))
            {
                Chartboost.showInterstitial(CBLocation.Default);
                Debug.Log("watching ad");
            }
            else
            {
                // We don't have a cached video right now, but try to get one for next time
                Chartboost.cacheInterstitial(CBLocation.Default);
                Debug.Log("no ad to be found");
            }
        }

        /// <summary>
        /// Set the next reward time to some time in future determined by the predefined number of hours, minutes and seconds.
        /// </summary>
        public void ResetNextRewardTime()
        {
            DateTime next = DateTime.Now.Add(new TimeSpan(rewardIntervalHours, rewardIntervalMinutes, rewardIntervalSeconds));
            StoreNextRewardTime(next);
        }

        void StoreNextRewardTime(DateTime time)
        {
            PlayerPrefs.SetString(NextRewardTimePPK, time.ToBinary().ToString());
            PlayerPrefs.Save();
        }

        DateTime GetNextRewardTime()
        {
            string storedTime = PlayerPrefs.GetString(NextRewardTimePPK, string.Empty);

            if (!string.IsNullOrEmpty(storedTime))
                return DateTime.FromBinary(Convert.ToInt64(storedTime));
            else
                return DateTime.Now;
        }
    }
}
