using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager WM;

    public bool SkipCountdown;
    public bool ZombieMode;
    public bool WeaponStore;
    public GameObject WeaponStoreGO;

    public Image[] ThumbAreaHelpers;
    public Transform[] ItemsToSpawn;
    public int CurrentWave;
    public int MaxEnemiesOnScreen;
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
    public List<Transform> EnemiesSpawned;
    public Animator[] WaveHUDAnims;
    public Text WaveCounter;
    public Text EnemiesToKill;

    public bool spawn;
    public int PlayerCount = 1;
    Transform Player;
    public static bool SpawnBrute;
    public bool SpawnBoss;

    Dictionary<float, GameObject> distDic = new Dictionary<float, GameObject>();

    public static float DamageMultiplier = 1;
    public static float HealthMultiplier = 1;
    public static float ScoreMultiplier = 1;

    public Text CountdownText;
    bool Countdown;
    float countdownTimer = 10;

    public GameObject WaveStats;
    public Text EnemyHealth;
    public Text EnemyStrength;
    public Text ScoreText;

    public int SpawnCount = 8;

    public EnergyBar EnemyCountBar;
    public GameObject EnemiesLeftParent;
    bool allowOpen;

    void Awake()
    {
        WM = this;

        ZombieMode = UIManager.ZombieMode;

    }

    // Use this for initialization
    void Start()
    {
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
        EnemyCountBar.valueMax = spawnLimit;


        if (Countdown)
        {
            countdownTimer -= Time.deltaTime;

            CountdownText.text = Mathf.CeilToInt(countdownTimer).ToString();
        }
        if (!ZombieMode)
        {
            EnemyHealth.text = HealthMultiplier.ToString("F1");
            EnemyStrength.text = DamageMultiplier.ToString("F1");
            ScoreText.text = ScoreMultiplier.ToString("F1");
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
            if (EnemiesSpawned.Count < MaxEnemiesOnScreen && spawnCount < spawnLimit)
            {
                FindFurthestSpawn();
                int randomEnemyFromList = Random.Range(0, EnemiesToSpawn.Count);
                Transform temp = EnemiesToSpawn[randomEnemyFromList].Spawn(GeneralSpawnPosition.position, Quaternion.identity);
                EnemiesSpawned.Add(temp);
                // spawn from the enemytospawn list
                // add to enemiesspawned list
                spawnCount += 1;
                if (SpawnBrute)
                {
                    EnemiesLeft += 1;
                    Transform tempBrute = BruteEnemiesToSpawn.Spawn(GeneralSpawnPosition.position, Quaternion.identity);
                    EnemiesSpawned.Add(tempBrute);
                    SpawnBrute = false;
                }
                if (SpawnBoss)
                {
                    EnemiesLeft += 1;
                    Transform tempBoss = BossEnemies[Random.Range(0, BossEnemies.Count)].Spawn(GeneralSpawnPosition.position, Quaternion.identity);
                    EnemiesSpawned.Add(tempBoss);
                    SpawnBoss = false;
                }
            }
        }

        if (EnemiesLeft <= 0)
        {
            if (allowOpen)
            {
                PlayerModel.PM.CheckState = true;
                WaveCounter.enabled = false;
                EnemiesToKill.enabled = false;
                CountdownText.enabled = true;
                CountdownText.text = "WAVE CLEARED!";
                Invoke("OpenWeaponStore", 2);
                EnemiesLeftParent.SetActive(false);
                spawn = false;
                allowOpen = false;
            }
        }

        EnemiesSurroundPlayer();
    }

    void EnemiesSurroundPlayer()
    {
        int rad = 3;
        if (EnemiesSpawned.Count <= 0)
            return;
        for (int i = 0; i < EnemiesSpawned.Count; i++)
        {


            //summon the enemies around this central GameObject
            float radian = i * Mathf.PI / (EnemiesSpawned.Count / 2);
            Vector3 ePosition = new Vector3(rad * Mathf.Cos(radian), rad * Mathf.Sin(radian), transform.position.z);
            // EnemiesSpawned[i].position = ePosition;
            // print(ePosition);
            if (!System.Single.IsNaN(ePosition.x) && !System.Single.IsNaN(ePosition.y))
            {
                if (Random.Range(0, 10) == 0)
                {
                    EnemiesSpawned[i].GetComponent<attackPlayer>().targetPosition = Player.position + ePosition;
                }
            }
        }
    }

    void OpenWeaponStore()
    {
        WeaponStore = true;
        WeaponStoreGO.SetActive(true);
    }

    public void ClosedWeaponScreen()
    {
        BreakthenNextWave();
        WeaponStore = false;
        WeaponStoreGO.SetActive(false);
    }

    void StartingtheGame()
    {
        WaveCounter.enabled = false;
        EnemiesToKill.enabled = false;
        AssignMultipliers();
        CurrentWave++;

        WaveCheck();
        Invoke("NextWave", 3);

        EnemiesSpawned.Clear();
    }

    void BreakthenNextWave()
    {
        if (!ZombieMode)
        {
            AssignMultipliers();
        }
        CurrentWave++;
        spawnCount = 0;
        Invoke("NextWave", 3);
        WaveCheck();
    }

    void AssignMultipliers()
    {
        switch (CurrentWave)
        {
            case 0:
                HealthMultiplier = 1f;
                DamageMultiplier = 1f;
                ScoreMultiplier = 1f;
                break;
            case 10:
                HealthMultiplier = 1.25f;
                ScoreMultiplier = 1.25f;
                break;
            case 20:
                DamageMultiplier = 1.25f;
                ScoreMultiplier = 1.5f;
                break;
            case 30:
                HealthMultiplier = 1.5f;
                ScoreMultiplier = 1.75f;
                break;
            case 40:
                DamageMultiplier = 1.5f;
                ScoreMultiplier = 2f;
                break;
        }
    }

    void NextWave()
    {
        if (ZombieMode)
        {
            HealthMultiplier += 0.01f;
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
        CountdownText.text = "NEXT WAVE STARTS IN...";
        ThumbAreaHelpers[0].enabled = true;
        ThumbAreaHelpers[1].enabled = true;
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
        ThumbAreaHelpers[0].enabled = false;
        ThumbAreaHelpers[1].enabled = false;
        EnemiesLeftParent.SetActive(true);
        Countdown = false;
        CountdownText.enabled = false;
        EnemiesToKill.gameObject.SetActive(true);
        EnemyCountBar.gameObject.SetActive(true);
        WaveHUDAnims[1].SetTrigger("ShowWave");
        Invoke("SpawnEnemies", 2);
        allowOpen = true;
    }

    void TurnOffEnemyStats()
    {
        if (WaveStats.activeInHierarchy)
            WaveStats.SetActive(false);
    }

    void SpawnEnemies()
    {
        spawn = true;
        WaveCounter.enabled = true;
        EnemiesToKill.enabled = true;
    }

    public void RemoveEnemyFromList()
    {
        EnemiesSpawned.RemoveAt(0);
        EnemiesLeft -= 1;
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

        GameObject furthestObj = distDic[distances[distances.Count - 1]];
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
                WaveStats.SetActive(true);
                Invoke("TurnOffEnemyStats", 6);
                spawnLimit = 50 * PlayerCount;
                EnemiesToSpawn = set1Enemies;
                BruteEnemiesToSpawn = BruteEnemies[1];
            }
            if (CurrentWave == 2 || CurrentWave == 12 || CurrentWave == 22 || CurrentWave == 32 || CurrentWave == 42)
            {
                spawnLimit = 7 * PlayerCount;
                EnemiesToSpawn = set2Enemies;
                BruteEnemiesToSpawn = BruteEnemies[1];
            }
            if (CurrentWave == 3 || CurrentWave == 13 || CurrentWave == 23 || CurrentWave == 33 || CurrentWave == 43)
            {
                spawnLimit = 10 * PlayerCount;
                EnemiesToSpawn = set3Enemies;
                BruteEnemiesToSpawn = BruteEnemies[2];
            }
            if (CurrentWave == 4 || CurrentWave == 14 || CurrentWave == 24 || CurrentWave == 34 || CurrentWave == 44)
            {
                spawnLimit = 12 * PlayerCount;
                EnemiesToSpawn = set4Enemies;
                BruteEnemiesToSpawn = BruteEnemies[2];
            }
            if (CurrentWave == 5 || CurrentWave == 15 || CurrentWave == 25 || CurrentWave == 35 || CurrentWave == 45)
            {
                spawnLimit = 15 * PlayerCount;
                EnemiesToSpawn = set5Enemies;
                BruteEnemiesToSpawn = BruteEnemies[3];
            }
            if (CurrentWave == 6 || CurrentWave == 16 || CurrentWave == 26 || CurrentWave == 36 || CurrentWave == 46)
            {
                spawnLimit = 18 * PlayerCount;
                EnemiesToSpawn = set6Enemies;
                BruteEnemiesToSpawn = BruteEnemies[3];
            }
            if (CurrentWave == 7 || CurrentWave == 17 || CurrentWave == 27 || CurrentWave == 37 || CurrentWave == 47)
            {
                spawnLimit = 20 * PlayerCount;
                EnemiesToSpawn = set7Enemies;
                BruteEnemiesToSpawn = BruteEnemies[3];
            }
            if (CurrentWave == 8 || CurrentWave == 18 || CurrentWave == 28 || CurrentWave == 38 || CurrentWave == 48)
            {
                spawnLimit = 22 * PlayerCount;
                EnemiesToSpawn = set8Enemies;
                BruteEnemiesToSpawn = BruteEnemies[4];
            }
            if (CurrentWave == 9 || CurrentWave == 19 || CurrentWave == 29 || CurrentWave == 39 || CurrentWave == 49)
            {
                spawnLimit = 25 * PlayerCount;
                EnemiesToSpawn = set9Enemies;
                BruteEnemiesToSpawn = BruteEnemies[4];
            }
            if (CurrentWave == 10 || CurrentWave == 20 || CurrentWave == 30 || CurrentWave == 40 || CurrentWave == 50)
            {
                spawnLimit = 25 * PlayerCount;
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

        //		switch(CurrentWave){
        //		case 1:
        //			StageGO.sprite = Stages [0];
        //			break;
        //		case 11:
        //			StageGO.sprite = Stages [1];
        //			break;
        //		case 21:
        //			StageGO.sprite = Stages [2];
        //			break;
        //		case 31:
        //			StageGO.sprite = Stages [3];
        //			break;
        //		case 41:
        //			StageGO.sprite = Stages [4];
        //			break;
        //
        //		} // ADD HARDER CONDITIONS HERE /////////////////////////////////////
        //        WaveHUDAnims [0].SetTrigger ("ShowAreaStage");
    }

}
