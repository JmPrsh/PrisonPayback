using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{

    public static ItemSpawner i;
    public Transform[] Items;
    public static int ItemAmount;
    public static Transform[] ItemsStatic;
    public Transform ItemSpawnPos;

    void Awake()
    {
        i = this;
        ItemsStatic = Items;
        ItemAmount = 0;
        // ItemAmount = PlayerPrefs.GetInt("ItemAmount");
        //Spawn(transform.position);
    }

    // Use this for initialization
    public void Spawn(Vector3 pos)
    {
        // ItemAmount = 5;
        if (CharacterStats.CS.Special)
            return;
        int Amt = Random.Range(1, 3);
        for (int j = 0; j < Amt; j++)
        {
            if(pos == Vector3.zero)
            Items[Random.Range(0, Items.Length)].Spawn(transform.position + (Vector3)Random.insideUnitCircle * 5, Quaternion.identity);
            else
                Items[Random.Range(0, Items.Length)].Spawn(pos + (Vector3)Random.insideUnitCircle * 5, Quaternion.identity);
        }
        // PlayerPrefs.SetInt("ItemAmount",ItemAmount);
    }

    public void SpawnOutsideInmates()
    {
        Items[Random.Range(0, 4)].Spawn(ItemSpawnPos.GetChild(Random.Range(0, ItemSpawnPos.childCount)).position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

        if (ItemAmount < 0) { ItemAmount = 0; }
        if (ItemAmount > 5) { ItemAmount = 5; }

    }
}
