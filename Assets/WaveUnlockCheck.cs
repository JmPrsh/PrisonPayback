using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUnlockCheck : MonoBehaviour
{
    public int StageID;
    Button t;

    void Awake()
    {
        t = GetComponent<Button>();
        StageID = transform.GetSiblingIndex() * 10;
        t.interactable = PlayerPrefs.GetInt("RoomsHighscore") >= StageID;


    }
    // Start is called before the first frame update
    void OnEnable()
    {
       
    }
    private void Update()
    {
  
      
    }
    public void ChooseWave()
    {
        int WaveSection = StageID;

        int chosenWave = WaveSection + 1;
        ChosenWaveHandler.instance.ChooseStartingWave(chosenWave);
    }
}
