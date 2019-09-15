using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCollisions : MonoBehaviour {
    void OnTriggerEnter2D (Collider2D hitInfo) {

        if (hitInfo.gameObject.layer == LayerMask.NameToLayer ("Pickups")) {
            if (hitInfo.transform.CompareTag ("Cigs")) {
                CharacterStats.CS.playermodel.PlayerCigs += 10;
            }
            if (hitInfo.transform.CompareTag ("Pipe")) {
                CharacterStats.CS.PipePickup ();
            }
            if (hitInfo.transform.CompareTag ("Knife")) {
                CharacterStats.CS.KnifePickup ();
            }
            if (hitInfo.transform.CompareTag ("Pistol")) {
                CharacterStats.CS.PistolPickup (false, true);
            }
            if (hitInfo.transform.CompareTag ("Shotgun")) {
                CharacterStats.CS.ShotgunPickup (false, true);
            }
            if (hitInfo.transform.CompareTag ("Sniper")) {
                CharacterStats.CS.SniperPickup (false, true);
            }
            if (hitInfo.transform.CompareTag ("MachineGun")) {
                CharacterStats.CS.MachineGunPickup (false, true);
            }
            if (hitInfo.transform.CompareTag ("MiniGun")) {
                CharacterStats.CS.MiniGunPickup (false, true);
            }

            if (hitInfo.transform.tag == "PistolAmmo") {
                int tempAmmoCount = Random.Range (5, 50);
                CharacterStats.CS.ammo.PistolMaxAmmo += tempAmmoCount;
                Vector3 SpawnPos = new Vector2 (transform.position.x, transform.position.y - 6);
                SpawnText (CharacterStats.CS.AmmoCollectedText, SpawnPos, CharacterStats.CS.PickUpGUI[0], $"+{tempAmmoCount}");
            }

            if (hitInfo.transform.tag == "ShotgunAmmo") {
                int tempAmmoCount = Random.Range (5, 25);
                CharacterStats.CS.ammo.ShotgunMaxAmmo += tempAmmoCount;
                Vector3 SpawnPos = new Vector2 (transform.position.x, transform.position.y - 6);
                SpawnText (CharacterStats.CS.AmmoCollectedText, SpawnPos, CharacterStats.CS.PickUpGUI[1], $"+{tempAmmoCount}");
            }

            if (hitInfo.transform.tag == "SniperAmmo") {
                int tempAmmoCount = Random.Range (5, 20);
                CharacterStats.CS.ammo.SniperMaxAmmo += tempAmmoCount;
                Vector3 SpawnPos = new Vector2 (transform.position.x, transform.position.y - 6);
                SpawnText (CharacterStats.CS.AmmoCollectedText, SpawnPos, CharacterStats.CS.PickUpGUI[2], $"+{tempAmmoCount}");

            }

            if (hitInfo.transform.tag == "MachineGunAmmo") {
                int tempAmmoCount = Random.Range (5, 20);
                CharacterStats.CS.ammo.MachineGunMaxAmmo += tempAmmoCount;
                Vector3 SpawnPos = new Vector2 (transform.position.x, transform.position.y - 6);
                SpawnText (CharacterStats.CS.AmmoCollectedText, SpawnPos, CharacterStats.CS.PickUpGUI[3], $"+{tempAmmoCount}");
            }

            if (hitInfo.transform.tag == "HealthPack") {
                if (CharacterStats.CS.Health < CharacterStats.CS.HealthStarting) {
                    CharacterStats.CS.SteroidEffect.SetActive (false);
                    CharacterStats.CS.Health = CharacterStats.CS.HealthStarting;
                    SpawnText (CharacterStats.CS.AmmoCollectedText, transform.position, CharacterStats.CS.PickUpGUI[9], "Health++");
                }
            }

            if (hitInfo.transform.tag == "Milk") {
                if (DPadButtons.Milk < 5) {
                    DPadButtons.Milk += 1;
                    SpawnText (CharacterStats.CS.AmmoCollectedText, transform.position, CharacterStats.CS.PickUpGUI[8], "+1");
                }
            }

            if (hitInfo.transform.tag == "Needle") {
                if (DPadButtons.Needles < 5) {
                    DPadButtons.Needles += 1;
                    SpawnText (CharacterStats.CS.AmmoCollectedText, transform.position, CharacterStats.CS.PickUpGUI[8], "+1");
                }
            }

            if (hitInfo.transform.tag == "Pills") {
                if (DPadButtons.Pills < 5) {
                    DPadButtons.Pills += 1;
                    SpawnText (CharacterStats.CS.AmmoCollectedText, transform.position, CharacterStats.CS.PickUpGUI[8], "+1");
                }
            }

            if (hitInfo.transform.tag == "Powder") {
                if (DPadButtons.Powder < 5) {
                    DPadButtons.Powder += 1;
                    SpawnText (CharacterStats.CS.AmmoCollectedText, transform.position, CharacterStats.CS.PickUpGUI[8], "+1");
                }
            }

            hitInfo.transform.Recycle ();
        }
    }

    public void SpawnText (Transform prefab, Vector3 spawnPos, Transform pickupUI, string AmountAwarded) {
        Transform temp = prefab.Spawn (spawnPos, transform.rotation * Quaternion.AngleAxis (90, Vector3.forward));
        temp.GetComponentInChildren<Text> ().text = "+1";
        temp.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 15, 0);

        Transform temp2 = pickupUI.Spawn (transform.position, transform.rotation * Quaternion.AngleAxis (90, Vector3.forward)) as Transform;
        temp2.SetParent (CharacterStats.CS.PlayerCanvas);
        temp2.localScale = new Vector2 (1, 1);

    }
}