using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    public static WaveManager WM;
    public static bool WaveComplete;
    public int MaxEnemiesOnScreen;
    public int maxEnemyDifficultyWeight = 20;
    public int WeightUsed;

    public List<Transform> EnemiesSpawnedForCount;

    public List<Transform> EnemiesSpawned;
    public bool bossWave;
    public bool SkipCountdown;
    public bool ZombieMode;
    public bool WeaponStore;
    public GameObject WeaponStoreGO;
    public GameObject StageCompleteGO;
    public GameObject StageCompleteGO2;
    public GameObject HideShopButton;
    public Image[] ThumbAreaHelpers;
    public Transform[] ItemsToSpawn;
    public int CurrentWave;

    List<Transform> EnemiesToSpawn;
    public List<Transform> ZombieEnemies;
    public List<Transform> set1Enemies;
    public List<Transform> set2Enemies;
    public List<Transform> set3Enemies;
    public List<Transform> set4Enemies;
    public List<Transform> set5Enemies;
    public List<Transform> set6Enemies;
    public List<Transform> set7Enemies;
    public List<Transform> set8Enemies;
    public List<Transform> set9Enemies;
    public List<Transform> set10Enemies;
    public AudioSource CountDownTimer;

    Transform BruteEnemiesToSpawn;
    public List<Transform> BruteEnemies;

    public List<Transform> BossEnemies;

    public List<Transform> EnemySpawnLocations;
    public Transform GeneralSpawnPosition;
    public Transform[] SectionLocationSpawns;

    public static int SpawnItemAmount;
    public int spawnLimit;
    int EnemiesLeft;
    public int spawnCount;

    public Animator[] WaveHUDAnims;
    public Text WaveCounter;
    public Text EnemiesToKill;

    public bool spawn;
    public int PlayerCount = 1;
    Transform Player;
    public static bool SpawnBrute;
    public bool SpawnBoss;

    public Animator CashAnim;

    Dictionary<float, GameObject> distDic = new Dictionary<float, GameObject>();

    public static float DamageMultiplier = 1;
    public static float HealthMultiplier = 1;
    public static float ScoreMultiplier = 1;

    public Text CountdownText;
    bool Countdown;
    float countdownTimer = 10;

    public Animator WaveStats;
    public Text EnemyHealth;
    public Text EnemyStrength;
    public Text ScoreText;
    public Image StageGO;
    public Sprite[] Stages;
    public Animator StageAnim;
    public int SpawnCount = 8;

    public EnergyBar EnemyCountBar;
    public GameObject EnemiesLeftParent;
    bool allowOpen;

    int zombieWaveCounter;

    public int WavesCleared = 0;

    void Awake()
    {
        WM = this;

        // if (UIManager.uim)
        ZombieMode = UIManager.ZombieMode;

    }

    // Use this for initialization
    void Start()
    {
        if (ZombieMode)
        {
            CurrentWave = 1;
        }
        else if (ChosenWaveHandler.instance)
        {
            CurrentWave = ChosenWaveHandler.instance.startingWave;
        }
        StartingtheGame();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        //		StageGO = GameObject.Find ("STAGE").GetComponent<Image> ();
        CountdownText.enabled = true;
        CountdownText.text = "NEXT WAVE STARTS IN...";
        allowOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCountBar.valueCurrent = EnemiesLeft;
        EnemyCountBar.valueMax = EnemiesSpawnedForCount.Count;

        if (Countdown)
        {
            countdownTimer -= Time.deltaTime;

            CountdownText.text = Mathf.CeilToInt(countdownTimer).ToString();
        }
        if (!ZombieMode)
        {
            EnemyHealth.text = $"x{HealthMultiplier}";
            EnemyStrength.text = $"x{DamageMultiplier}";
            ScoreText.text = $"x{ScoreMultiplier}";
        }
        WaveCounter.text = "Wave " + CurrentWave;

        if (EnemiesLeft <= 5)
        {
            EnemiesToKill.text = EnemiesLeft.ToString();
            EnemiesToKill.gameObject.SetActive(true);
            EnemyCountBar.gameObject.SetActive(false);
        }
        else
        {
            EnemyCountBar.gameObject.SetActive(true);
            EnemiesToKill.gameObject.SetActive(false);
        }

        if (spawn)
        {
            if (ZombieMode)
                ZombieSpawning();
            else
                RiotSpawning();

            EnemiesLeft = EnemiesSpawned.Count;
        }
        // ClosedWeaponScreen ()
        if (EnemiesLeft <= 0)
        {
            spawn = false;
            if (allowOpen)
            {
                PlayerModel.PM.CheckState = true;
                WaveCounter.enabled = false;
                EnemiesToKill.enabled = false;

                FinishedWave();

                if (PlayerPrefs.GetInt("Adverts") == 0 && AdvertHandler.instance)
                {
                    WavesCleared++;
                    if (WavesCleared > 4)
                    {
                        AdvertHandler.instance.WatchAdvert();
                        WavesCleared = 0;
                    }
                }
                EnemiesLeftParent.SetActive(false);

                allowOpen = false;
            }
        }

        if (!ZombieMode)
        {
            if (!spawn)
            {
                for (int i = 0; i < EnemiesSpawned.Count; i++)
                {
                    if (EnemiesSpawned[i] != null)
                    {
                        if (i <= MaxEnemiesOnScreen)
                        {
                            EnemiesSpawned[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            EnemiesSpawned[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        EnemiesSurroundPlayer();
    }

    void RiotSpawning()
    {
        if (WeightUsed < (maxEnemyDifficultyWeight * PlayerCount))
        {
            FindFurthestSpawn();
            int randomEnemyFromList = Random.Range(0, EnemiesToSpawn.Count);
            Transform temp = EnemiesToSpawn[randomEnemyFromList].Spawn(GeneralSpawnPosition.position, Quaternion.identity);
            WeightUsed += (int)temp.GetComponent<attackPlayer>().EnemyType.DifficultyWeight;
            EnemiesSpawned.Add(temp);
            EnemiesSpawnedForCount.Add(temp);
            // spawn from the enemytospawn list
            // add to enemiesspawned list
            spawnCount += 1;
            if (SpawnBrute)
            {
                if (CurrentWave != 5 && CurrentWave != 15 && CurrentWave != 25 && CurrentWave != 35 && CurrentWave != 45)
                {
                    EnemiesLeft += 1;
                    Transform tempBrute = BruteEnemiesToSpawn.Spawn(GeneralSpawnPosition.position, Quaternion.identity);
                    WeightUsed += (int)tempBrute.GetComponent<attackPlayer>().EnemyType.DifficultyWeight;
                    EnemiesSpawned.Add(tempBrute);
                    EnemiesSpawnedForCount.Add(tempBrute);
                }
                SpawnBrute = false;
            }
            if (SpawnBoss)
            {
                EnemiesLeft += 1;
                Transform tempBoss = BossEnemies[Random.Range(0, BossEnemies.Count)].Spawn(GeneralSpawnPosition.position, Quaternion.identity);
                EnemiesSpawned.Add(tempBoss);
                EnemiesSpawnedForCount.Add(tempBoss);
                SpawnBoss = false;
            }
        }
        else
        {
            spawn = false;
        }
    }

    void ZombieSpawning()
    {
        if (EnemiesSpawned.Count < MaxEnemiesOnScreen && spawnCount < spawnLimit)
        {
            FindFurthestSpawn();
            int randomEnemyFromList = Random.Range(0, EnemiesToSpawn.Count);
            Transform temp = EnemiesToSpawn[randomEnemyFromList].Spawn(GeneralSpawnPosition.position, Quaternion.identity);
            WeightUsed += (int)temp.GetComponent<attackPlayer>().EnemyType.DifficultyWeight;
            EnemiesSpawned.Add(temp);
            EnemiesSpawnedForCount.Add(temp);
            // spawn from the enemytospawn list
            // add to enemiesspawned list
            spawnCount += 1;
            if (SpawnBrute)
            {
                if (CurrentWave != 5 && CurrentWave != 15 && CurrentWave != 25 && CurrentWave != 35 && CurrentWave != 45)
                {
                    EnemiesLeft += 1;
                    Transform tempBrute = BruteEnemiesToSpawn.Spawn(GeneralSpawnPosition.position, Quaternion.identity);
                    WeightUsed += (int)tempBrute.GetComponent<attackPlayer>().EnemyType.DifficultyWeight;
                    EnemiesSpawned.Add(tempBrute);
                    EnemiesSpawnedForCount.Add(tempBrute);
                }
                SpawnBrute = false;
            }
        }

    }

    void EnemiesSurroundPlayer()
    {

        if (EnemiesSpawned.Count <= 0)
            return;

        ChooseRandomPosition();

    }

    [Button]
    public void KillAllEnemies()
    {
        attackPlayer[] enemies = GameObject.FindObjectsOfType<attackPlayer>();
        foreach (attackPlayer enemy in enemies)
            enemy.DamagedByPlayer(100000, false, false);
    }

    void ChooseRandomPosition()
    {
        for (int i = 0; i < EnemiesLeft; i++)
        {
            if (EnemiesSpawned[i] != null)
            {
                float rad = EnemiesSpawned[i].GetComponent<attackPlayer>().EnemyType.AttackDistance;
                float division = 5;
                //summon the enemies around this central GameObject
                float radian = i * Mathf.PI / (EnemiesLeft/ division) + division;
                Vector3 ePosition = new Vector3(rad * Mathf.Cos(radian), (rad * Mathf.Sin(radian)) - 1, transform.position.z);
                // EnemiesSpawned[i].position = ePosition;
                // print(ePosition);
                if (!System.Single.IsNaN(ePosition.x) && !System.Single.IsNaN(ePosition.y))
                {
                    if (Random.Range(0, 100) == 0)
                    {
                        EnemiesSpawned[i].GetComponent<attackPlayer>().targetPosition = (Player.position + ((Vector3)Random.insideUnitCircle * 1.1f)) + ePosition;
                    }
                }
            }
        }
    }

    public void FinishedWave()
    {
        WaveComplete = true;
        WeightUsed = 0;
        EnemiesSpawnedForCount.Clear();
        if (ZombieMode)
        {
            if (!CharacterStats.CS.Special)
            {
                if (CurrentWave == 50)
                {
                    AchievementHandler.WhichAchievement(6);
                }
                else if (CurrentWave == 100)
                {
                    AchievementHandler.WhichAchievement(7);
                    AchievementHandler.completedallgameID[2] = 1;
                    AchievementHandler.CheckCompletedAllGame();
                }
            }

            if (CharacterStats.CS.Special)
            {
                Invoke("BeginNextWave", 2);
            }
            else
            {
                zombieWaveCounter += 1;
                if (zombieWaveCounter >= 3)
                {
                    Invoke("ShowShopWindow", 2);
                    HideShopButton.SetActive(true);
                    zombieWaveCounter = 0;
                }
                else
                {
                    Invoke("BeginNextWave", 2);
                }
            }
        }
        else
        {
            WaveStats.gameObject.SetActive(false);
            // normal mode
            if (CurrentWave == 50)
            {
                Invoke("GameComplete", 2);
                if (!CharacterStats.CS.Special)
                {
                    AchievementHandler.WhichAchievement(5);
                    AchievementHandler.completedallgameID[1] = 1;
                    AchievementHandler.CheckCompletedAllGame();
                }
            }
            else if (CurrentWave == 10 || CurrentWave == 20 || CurrentWave == 30 || CurrentWave == 40)
            {
                // just beat the boss
                Invoke("ShowStageComplete", 2);
                if (!CharacterStats.CS.Special)
                {
                    switch (CurrentWave)
                    {
                        case 10:
                            AchievementHandler.WhichAchievement(1);
                            break;
                        case 20:
                            AchievementHandler.WhichAchievement(2);
                            break;
                        case 30:
                            AchievementHandler.WhichAchievement(3);
                            break;
                        case 40:
                            AchievementHandler.WhichAchievement(4);
                            break;
                    }
                }
            }
            else
            {
                if (CharacterStats.CS.Special)
                {
                    Invoke("BeginNextWave", 2);
                }
                else
                {
                    Invoke("ShowShopWindow", 2);
                }
            }
        }
        System.GC.Collect();
    }

    public void ShowShopWindow()
    {
        if (!CharacterStats.CS.Special)
        {
            WeaponStoreGO.SetActive(true);
            HideShopButton.SetActive(true);
        }
        else
        {
            Invoke("BeginNextWave", 2);
        }
    }

    void BeginNextWave()
    { // change the play button in shop window to use this instead
        HideShopButton.SetActive(false);
        WaveComplete = false;
        if (CurrentWave == 50)
        {
            // COMPLETED GAME!
            // go to menu
        }
        else
        {
            CurrentWave++;
            WaveCheck(); // then check what wave it is
                         // start the new wave in 3 seconds ....
            ItemSpawner.i.SpawnOutsideInmates();
        }
    }

    void ShowStageComplete()
    {
        // if we just beat the boss
        if (AdvertHandler.instance)
            AdvertHandler.instance.WatchAdvert();

        StageCompleteGO.SetActive(true);
    }
    void GameComplete()
    {
        // if we just beat the boss
        if (AdvertHandler.instance)
            AdvertHandler.instance.WatchAdvert();

        StageCompleteGO2.SetActive(true);
    }

    public void ClosedWeaponScreen()
    {
        BeginNextWave();
        WeaponStoreGO.SetActive(false);
    }

    void StartingtheGame()
    {
        WaveCheck();

        EnemiesSpawned.Clear();
    }

    void AssignMultipliers()
    {
        switch (CurrentWave)
        {
            case 1:
                HealthMultiplier = 1f;
                DamageMultiplier = 1f;
                ScoreMultiplier = 1f;
                break;
            case 11:
                HealthMultiplier = 2f;
                DamageMultiplier = 1f;
                ScoreMultiplier = 1.25f;
                break;
            case 21:
                HealthMultiplier = 2f;
                DamageMultiplier = 2f;
                ScoreMultiplier = 1.5f;
                break;
            case 31:
                HealthMultiplier = 2.5f;
                DamageMultiplier = 2f;
                ScoreMultiplier = 1.75f;
                break;
            case 41:
                HealthMultiplier = 2.5f;
                DamageMultiplier = 2.5f;
                ScoreMultiplier = 2f;
                break;
        }
    }

    void NextWave()
    {
        spawnCount = 0;
        if (ZombieMode)
        {
            HealthMultiplier += 0.35f;
        }
        if (!SkipCountdown)
            StartCoroutine(StartWave());
        else
        {
            StartRound();
        }

       
    }

    IEnumerator StartWave()
    {
        CountdownText.enabled = true;
        CountdownText.text = "NEXT WAVE STARTS IN...";
        //ThumbAreaHelpers[0].enabled = true;
        //ThumbAreaHelpers[1].enabled = true;

        yield return new WaitForSeconds(2);
        ItemSpawner.i.Spawn();
        Countdown = true;
        countdownTimer = 10;
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        CountDownTimer.Play();
        yield return new WaitForSeconds(1);
        StartRound();
    }

    void StartRound()
    {
        CountDownTimer.Play();
        //ThumbAreaHelpers[0].enabled = false;
        //ThumbAreaHelpers[1].enabled = false;
        EnemiesLeftParent.SetActive(true);
        Countdown = false;
        CountdownText.enabled = false;
        EnemiesToKill.gameObject.SetActive(true);
        EnemyCountBar.gameObject.SetActive(true);
        WaveHUDAnims[1].SetTrigger("ShowWave");
        Invoke("SpawnEnemies", 2);
        allowOpen = true;
    }

    void SpawnEnemies()
    {
        spawn = true;
        WaveCounter.enabled = true;
        EnemiesToKill.enabled = true;
    }

    public void RemoveEnemyFromList(Transform enemy)
    {
        EnemiesSpawned.Remove(enemy);
        EnemiesLeft = EnemiesSpawned.Count;
    }

    void FindFurthestSpawn()
    {
        foreach (Transform spawnlocations in EnemySpawnLocations)
        {
            float dist = Vector3.Distance(Player.position, spawnlocations.position);

            distDic.Add(dist, spawnlocations.gameObject);
        }

        List<float> distances = distDic.Keys.ToList();

        distances.Sort();

        GameObject furthestObj = distDic[distances[distances.Count - Random.Range(1, 4)]];
        GeneralSpawnPosition = furthestObj.transform;
        distDic.Clear();
        // Do something with furthestObj
    }

    void WaveCheck()
    {

        if (!ZombieMode)
        {

            if (CurrentWave == 1 || CurrentWave == 11 || CurrentWave == 21 || CurrentWave == 31 || CurrentWave == 41)
            {
                WaveStats.gameObject.SetActive(true);
                AssignMultipliers();
                // print("SHOW STAGE STATS");
                StageAnim.SetTrigger("ShowAreaStage");
                WaveStats.SetTrigger("ShowStats");

                spawnLimit = 15 * PlayerCount;
                EnemiesToSpawn = set1Enemies;
                BruteEnemiesToSpawn = BruteEnemies[1];
            }
            else if (CurrentWave == 2 || CurrentWave == 12 || CurrentWave == 22 || CurrentWave == 32 || CurrentWave == 42)
            {
                if (!CharacterStats.CS.Special)
                {
                    AchievementHandler.WhichAchievement(0);
                }
                spawnLimit = 19 * PlayerCount;
                EnemiesToSpawn = set2Enemies;
                BruteEnemiesToSpawn = BruteEnemies[1];
            }
            else if (CurrentWave == 3 || CurrentWave == 13 || CurrentWave == 23 || CurrentWave == 33 || CurrentWave == 43)
            {
                spawnLimit = 24 * PlayerCount;
                EnemiesToSpawn = set3Enemies;
                BruteEnemiesToSpawn = BruteEnemies[2];
            }
            else if (CurrentWave == 4 || CurrentWave == 14 || CurrentWave == 24 || CurrentWave == 34 || CurrentWave == 44)
            {
                spawnLimit = 29 * PlayerCount;
                EnemiesToSpawn = set4Enemies;
                BruteEnemiesToSpawn = BruteEnemies[2];
            }
            else if (CurrentWave == 5 || CurrentWave == 15 || CurrentWave == 25 || CurrentWave == 35 || CurrentWave == 45)
            {
                // Dog Round
                spawnLimit = 34 * PlayerCount;
                EnemiesToSpawn = set5Enemies;
                BruteEnemiesToSpawn = BruteEnemies[2];
            }
            else if (CurrentWave == 6 || CurrentWave == 16 || CurrentWave == 26 || CurrentWave == 36 || CurrentWave == 46)
            {
                spawnLimit = 39 * PlayerCount;
                EnemiesToSpawn = set6Enemies;
                BruteEnemiesToSpawn = BruteEnemies[3];
            }
            else if (CurrentWave == 7 || CurrentWave == 17 || CurrentWave == 27 || CurrentWave == 37 || CurrentWave == 47)
            {
                spawnLimit = 44 * PlayerCount;
                EnemiesToSpawn = set7Enemies;
                BruteEnemiesToSpawn = BruteEnemies[3];
            }
            else if (CurrentWave == 8 || CurrentWave == 18 || CurrentWave == 28 || CurrentWave == 38 || CurrentWave == 48)
            {
                spawnLimit = 49 * PlayerCount;
                EnemiesToSpawn = set8Enemies;
                BruteEnemiesToSpawn = BruteEnemies[4];
            }
            else if (CurrentWave == 9 || CurrentWave == 19 || CurrentWave == 29 || CurrentWave == 39 || CurrentWave == 49)
            {
                spawnLimit = 54 * PlayerCount;
                EnemiesToSpawn = set9Enemies;
                BruteEnemiesToSpawn = BruteEnemies[4];
            }
            if (CurrentWave == 10 || CurrentWave == 20 || CurrentWave == 30 || CurrentWave == 40 || CurrentWave == 50)
            {
                bossWave = true;
                spawnLimit = 54 * PlayerCount;
                EnemiesToSpawn = set10Enemies;
                BruteEnemiesToSpawn = BruteEnemies[4];
                SpawnBoss = true;
            }
        }
        else
        {
            SpawnCount += 2;
            spawnLimit = SpawnCount * PlayerCount;
            EnemiesToSpawn = ZombieEnemies;
            BruteEnemiesToSpawn = BruteEnemies[0];
        }
        EnemiesLeft = spawnLimit;

        if (!ZombieMode)
        {
            switch (CurrentWave)
            {
                case 1:
                    StageGO.sprite = Stages[0];
                    break;
                case 11:
                    StageGO.sprite = Stages[1];
                    break;
                case 21:
                    StageGO.sprite = Stages[2];
                    break;
                case 31:
                    StageGO.sprite = Stages[3];
                    break;
                case 41:
                    StageGO.sprite = Stages[4];
                    break;
            }
        }
        //		} // ADD HARDER CONDITIONS HERE /////////////////////////////////////
        //        WaveHUDAnims [0].SetTrigger ("ShowAreaStage");
        Invoke("NextWave", 3);
    }

    public void LoadZombieLevel()
    {
        UIManager.ZombieMode = true;
        PlayerPrefs.SetString("LevelToLoad", "GameMode");
        SceneManager.LoadScene("LoadingScreen");
    }

}