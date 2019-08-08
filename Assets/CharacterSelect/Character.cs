using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterSequenceNumber;
    public string characterName;
    public int price;
    public bool isFree = false;
    public bool Special;
    public int SpecialCharacterID;
    SpriteRenderer sr;
    public bool Unlocked;

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(characterName, 0) == 1);
        }
    }

    void Start()
    {
        characterName = characterName.ToUpper();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Invoke("UpdateMat", 0.1f);
    }

    void UpdateMat()
    {
        sr.material = CharacterManager.Instance.spriteMaterial;
    }

    void Update()
    {
        Unlocked = IsUnlocked;

        if (IsUnlocked)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.black;
        }
    }

    public bool Unlock()
    {
        if (Special)
        {
            // add objective check unlocks here
        }
        if (IsUnlocked)
            return true;

        if (CoinManager.Instance.Coins >= price)
        {
            PlayerPrefs.SetInt(characterName, 1);
            PlayerPrefs.Save();
            CoinManager.Instance.RemoveCoins(price);
            CharacterScroller.CS.UnlockedScreen.SetTrigger("Unlocked");
            return true;
        }

        return false;
    }

    bool ObjectiveComplete()
    {
        switch (SpecialCharacterID)
        {
            case 0:
                // zombie objective
                return StatManager.Instance.ZombieKills >= 1000;

            case 1:
                // cyborg objective
                return StatManager.Instance.HighestWave >= 50;

            case 2:
                // gun slinger objective
                return StatManager.Instance.BrutesKilled >= 50;

            case 3:
                // agent objective
                return StatManager.Instance.BossesKilled >= 20;

            case 4:
                // samurai objective
                return StatManager.Instance.CriticalsHighscore >= 50;

        }
        return false;
    }
}
