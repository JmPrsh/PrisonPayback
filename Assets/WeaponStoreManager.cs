using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreManager : MonoBehaviour
{
    public static WeaponStoreManager WSM;
    public Text ShopText;
    // Start is called before the first frame update
    void Start()
    {
        WSM = this;
    }

    // Update is called once per frame
    public void ShowText(string desc)
    {
        StartCoroutine(TextFromShop(desc));
    }

    IEnumerator TextFromShop(string desc)
    {
        ShopText.text = desc;
        ShopText.enabled = true;
        yield return new WaitForSeconds(3);
        ShopText.enabled = false;
        ShopText.text = string.Empty;
    }
}
