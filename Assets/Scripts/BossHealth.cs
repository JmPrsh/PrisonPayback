using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class BossHealth : MonoBehaviour {

	public GameObject ChildIcon;
	public GameObject ParentBoss;
	RectTransform RT;
    float health;

	// Use this for initialization
	void Awake () {
		RT = GetComponent<RectTransform> ();
//		transform.SetParent(GameObject.Find("PlayersHUD").transform);
		RT.anchoredPosition = new Vector2(0,-40);
        
	}

    private void OnEnable()
    {
        health = (int)ParentBoss.GetComponent<attackPlayer>().health;
    }

    // Update is called once per frame
    void Update () {
        health = (int)ParentBoss.GetComponent<attackPlayer>().health;


        if (health <= 0) {
			Invoke("RemoveHealthBar", 3);
		}

	}

	void RemoveHealthBar(){
		this.Recycle ();
	}
}
