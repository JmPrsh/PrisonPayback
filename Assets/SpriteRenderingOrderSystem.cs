using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRenderingOrderSystem : MonoBehaviour
{
    float timer = 1;
    float tempTimer;

    private void Start()
    {
        tempTimer = timer;
    }

    void Update()
    {
        if (Time.time > timer)
        {
            SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

            foreach (SpriteRenderer renderer in renderers)
            {
                if(!renderer.GetComponent<IgnoreSpriteOrder>())
                renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
            }
            timer = Time.time + tempTimer;
        }
    }
}
