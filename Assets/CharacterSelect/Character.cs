using UnityEngine;

public class Character : MonoBehaviour
{
    public int characterSequenceNumber;
    public string characterName;
    public int price;
    public bool isFree = false;
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

    void UpdateMat(){
        sr.material = CharacterManager.Instance.spriteMaterial;
    }

    void Update(){
        Unlocked = IsUnlocked;

        if(IsUnlocked){
            sr.color = Color.white;
        }else{
            sr.color = Color.black;
        }
    }

    public bool Unlock()
    {
        if (IsUnlocked)
            return true;

        if (SgLib.CoinManager.Instance.Coins >= price)
        {
            PlayerPrefs.SetInt(characterName, 1);
            PlayerPrefs.Save();
            SgLib.CoinManager.Instance.RemoveCoins(price);
            CharacterScroller.CS.UnlockedScreen.SetTrigger("Unlocked");
            return true;
        }

        return false;
    }
}
