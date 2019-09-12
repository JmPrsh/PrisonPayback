using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLifeSpan : MonoBehaviour {
    // Start is called before the first frame update
    float lifeTime = 10;
   public float timer;
    Animator anim;
    bool trig;

    void Start () {
        anim = GetComponent<Animator> ();
    }

    void OnEnable()
    {
        lifeTime = 10;
        trig = false;
    }

    // Update is called once per frame
    void Update () {
        if (timer >= lifeTime) {
            transform.Recycle ();
            timer = 0;
        } else {
            timer += Time.deltaTime;
        }
        if (timer >= lifeTime - 2)
        {
            if (!trig)
            {
                anim.SetTrigger("Flicker");
                trig = true;
            }
        }
    }
}