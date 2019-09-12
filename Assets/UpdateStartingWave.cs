using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStartingWave : MonoBehaviour
{
    Dropdown d;
    // Start is called before the first frame update
    void Start()
    {
        d = GetComponent<Dropdown>();
        UpdateWave();
    }

    // Update is called once per frame
    public void UpdateWave()
    {
        int WaveSection = (d.value * 10);

        int chosenWave = WaveSection + 1;
        ChosenWaveHandler.instance.ChooseStartingWave(chosenWave);

        d.options.Clear();
    }
}
