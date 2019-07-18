//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class EnemyManager : MonoBehaviour
//{
//
//	public Vector2 EnemyGeneralPlacement;
//	public int spawnAmount;
//	public int numSelectors = 5;
//	public attackPlayer EnemyTemplate; //selected in the editor
//	public attackPlayer EnemyTemplateBoss; //selected in the editor
//	public Transform EnemyTemplateDog;
//	public Transform EnemyTemplateInmates;
//	public static bool spawn;
//	public static bool spawnBrute;
//	public static int EnemySpawnPositionID;
//	public int EnemySpawnPositionIDCopy;
//	PlayerModel playerModel;
//	public Transform[] EnemyPlacement;
//	public static bool checkNow;
//	
//	void Awake ()
//	{
//		playerModel = GameObject.FindGameObjectWithTag ("PlayerModel").GetComponent<PlayerModel> ();
//		EnemyTemplateDog.CreatePool ();
//		EnemyTemplateInmates.CreatePool ();
//	}
//
//	void Start ()
//	{
//		EnemySpawnPositionID = 0;
//
//		if (!PlayerPrefs.HasKey ("SpawnAmount")) {
//			playerModel.EnemySpawnAmount = 2;
//		}
//	}
//
//	void SpawnEnemies ()
//	{
//
//		spawn = true;
//	}
//
//
//
//
//
//	void Update ()
//	{
//
//		if (checkNow) {
//			Invoke ("AddLastRoom", 1);
//			checkNow = false;
//		}
//
//		EnemySpawnPositionIDCopy = EnemySpawnPositionID;
//
//
//		spawnAmount = playerModel.EnemySpawnAmount;
//
//		if (spawn && !PauseMenu.isPaused) {
//			playerModel.CheckEnemies ();
//			if (spawnAmount > 10) {
//			
//			}
//
//			int originalSpawnAmount = spawnAmount;
//
////			attackPlayer EnemyTempBoss = EnemyTemplateBoss.Spawn (BB.Rooms[BB.Rooms.Count -1].transform.position + (Vector3)Random.insideUnitCircle * 5, Quaternion.identity); // spawn boss in last room
//
//
////			switch (MaxEnemyTypeRange) {
////			case 2:
////				if (spawnAmount > 3) {
////					spawnAmount = 3;
////				} else {
////					spawnAmount = originalSpawnAmount;
////				}
////				break;
////			case 3:
////				if (spawnAmount > 7) {
////					spawnAmount = 7;
////				} else {
////					spawnAmount = originalSpawnAmount;
////				}
////				break;
////			case 4:
////				if (spawnAmount > 4) {
////					spawnAmount = 4;
////				} else {
////					spawnAmount = originalSpawnAmount;
////				}
////				break;
////			case 5:
////				if (spawnAmount > 3) {
////					spawnAmount = 3;
////				} else {
////					spawnAmount = originalSpawnAmount;
////				}
////				break;
////			case 6:
////				if (spawnAmount > 5) {
////					spawnAmount = 5;
////				} else {
////					spawnAmount = originalSpawnAmount;
////				}
////				break;
////			case 7:
////				if (spawnAmount > 5) {
////					spawnAmount = 5;
////				} else {
////					spawnAmount = originalSpawnAmount;
////				}
////				break;
////			}
//
//			for (int i = 0; i < spawnAmount; i++) {
//				
////				for (int s = 0; s < BB.Rooms.Count -1; s++) {
//				if (LevelMax > 0) {
//					attackPlayer EnemyTemp = EnemyTemplate.Spawn (EnemyGeneralPlacement + Random.insideUnitCircle * 2, Quaternion.identity);
////						attackPlayer EnemyTemp = EnemyTemplate.Spawn (BB.Rooms [s].transform.position + (Vector3)Random.insideUnitCircle * 3, Quaternion.identity);
////				EnemyTemp.transform.position = Random.insideUnitCircle * 10;
//
//					int SpawnEnemyChance = Random.Range (MinEnemyTypeRange, MaxEnemyTypeRange);
//					switch (SpawnEnemyChance) {
//					case 1:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.StandardGuard;
//
//						break;
//					case 2:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.Captain;
//					
//						EnemyTemplateDog.Spawn (EnemyTemp.transform.position + (Vector3)Random.insideUnitCircle * 0.3f, Quaternion.identity);
//						break;
//					case 3:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.Riot;
//
//						break;
//					case 4:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.Swat;
//						break;
//					case 5:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.SwatDemolition;
//						attackPlayer EnemyTemp2 = EnemyTemplate.Spawn (EnemyGeneralPlacement + Random.insideUnitCircle * 2, Quaternion.identity);
//						EnemyTemp2.enemyclass = attackPlayer.EnemyClass.Swat;
//						break;
//					case 6:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.RiotStun;
//						attackPlayer EnemyTemp3 = EnemyTemplate.Spawn (EnemyGeneralPlacement + Random.insideUnitCircle * 2, Quaternion.identity);
//						EnemyTemp3.enemyclass = attackPlayer.EnemyClass.Riot;
//						break;
//					case 7:
//						EnemyTemp.enemyclass = attackPlayer.EnemyClass.Sniper;
//						break;
//					
//					}
//				}
////					int SpawnInmateChance = Random.Range (1, 4);
////					if (SpawnInmateChance == 1) {
////						Transform temp1 = EnemyTemplateInmates.Spawn (BB.Rooms [s].transform.position + (Vector3)Random.insideUnitCircle * 3, Quaternion.identity);
//				if (LevelMax < 1) {
//					EnemyTemplateInmates.Spawn (EnemyGeneralPlacement + Random.insideUnitCircle * 2, Quaternion.identity);
//				}
//
//					
//			}
//		
//			spawn = false;
//		}
//
//
//		if (spawnBrute && !PauseMenu.isPaused) {
//			playerModel.CheckEnemies ();
//			for (int i = 0; i < 2; i++) {		
//				
//				attackPlayer EnemyTempBoss = EnemyTemplate.Spawn (EnemyGeneralPlacement + Random.insideUnitCircle * 2, Quaternion.identity);
//				attackPlayer EnemyTemp = EnemyTemplate.Spawn (EnemyGeneralPlacement + Random.insideUnitCircle * 2, Quaternion.identity);
//
//				int SpawnEnemyChance = Random.Range (MinEnemyTypeRange, MaxEnemyTypeRange);
//				switch (SpawnEnemyChance) {
//				case 0:
//					EnemyTempBoss.enemyclass = attackPlayer.EnemyClass.StandardGuardBoss;
//					EnemyTemp.enemyclass = attackPlayer.EnemyClass.StandardGuard;
//					break;
//				case 1:
//					EnemyTempBoss.enemyclass = attackPlayer.EnemyClass.CaptainBoss;
//					EnemyTemp.enemyclass = attackPlayer.EnemyClass.Captain;
//					break;
//				case 2:
//					EnemyTempBoss.enemyclass = attackPlayer.EnemyClass.RiotBoss;
//					EnemyTemp.enemyclass = attackPlayer.EnemyClass.Riot;
//					break;
//				case 3:
//					EnemyTempBoss.enemyclass = attackPlayer.EnemyClass.SwatBoss;
//					EnemyTemp.enemyclass = attackPlayer.EnemyClass.Swat;
//					break;
//					
//				}
//
//							
//			}
//			spawnBrute = false;
//		}
//
//
//	}
//
//
//}
