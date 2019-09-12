using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenWaveHandler : MonoBehaviour
{
    public static ChosenWaveHandler instance;
    public int startingWave;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void ChooseStartingWave(int i)
    {
        startingWave = i;
    }
}
