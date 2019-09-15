using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyStats : MonoBehaviour {
    Text t;
    public enum WhichStat{
        kills,
        combos,
        criticals,
        buffs,
        brutes,
        bosses,
        wavesCleared
    }
    public WhichStat whichstat;

    void Awake(){
        t = GetComponent<Text>();
    }

	// Use this for initialization
	void OnEnable () {
        switch(whichstat){
            case WhichStat.kills:
                t.text = StatManager.KillsTotal.ToString();
                break;
            case WhichStat.combos:
                t.text = StatManager.Instance.ComboHighscore.ToString();
                break;
            case WhichStat.criticals:
                t.text = StatManager.criticalsTotal.ToString();
                break;
            case WhichStat.buffs:
                t.text = StatManager.Instance.BuffsHighscore.ToString();
                break;
            case WhichStat.brutes:
                t.text = StatManager.brutesKilledTotal.ToString();
                break;
            case WhichStat.bosses:
                t.text = StatManager.bossesKilledTotal.ToString();
                break;
            case WhichStat.wavesCleared:
                t.text = StatManager.Instance.WavesCleared.ToString();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
