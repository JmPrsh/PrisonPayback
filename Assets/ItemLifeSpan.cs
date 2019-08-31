using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLifeSpan : MonoBehaviour {
    // Start is called before the first frame update
    public float lifeTime = 5;
    Animator anim;

    void Start () {
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update () {
        if (Time.time >= lifeTime) {
            transform.Recycle ();
        } else {
            if (Time.time >= lifeTime - 2) {
                anim.SetTrigger ("Flicker");
            }
        }
    }
}