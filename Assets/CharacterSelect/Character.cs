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
    public string Objective;
    public string progress;
    public int ObjectiveMax;
    public bool Unlocked;

    public bool IsUnlocked
    {
        get
        {
            return (isFree || PlayerPrefs.GetInt(characterName, 0) == 1 );
        }
    }
    public bool IsUnlockedSPECIAL
    {
        get
        {
            return (Special && CharacterManager.SpecialsUnlocked == 1 || Special && ObjectiveComplete());
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

        if (IsUnlocked || IsUnlockedSPECIAL)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = CharacterScroller.CS.lockColor;
        }
    }

    public bool Unlock()
    {
        if (Special)
        {
            // add objective check unlocks here
            if (CharacterManager.SpecialsUnlocked == 1)
                return true;
        }
        else if (IsUnlocked)
            return true;

        
        else if (CoinManager.Instance.Coins >= price)
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
                ObjectiveMax = 5000;
                return StatManager.Instance.ZombieKills >= 5000;

            case 1:
                // cyborg objective
                ObjectiveMax = 50;
                return StatManager.Instance.WavesCleared >= 50;

            case 2:
                // gun slinger objective
                ObjectiveMax = 100;
                return StatManager.Instance.BrutesKilled >= 100;

            case 3:
                // agent objective
                ObjectiveMax = 10;
                return StatManager.Instance.BossesKilled >= 10;

            case 4:
                // samurai objective
                ObjectiveMax = 5000;
                return StatManager.Instance.CriticalsHighscore >= 5000;

        }
        return false;
    }
}
