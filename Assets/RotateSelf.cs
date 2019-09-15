using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    float t;
    float length = 2f;

   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0, -100 * Time.deltaTime);
        t = Time.time;
        transform.localScale = new Vector3(Mathf.PingPong(t, length - 1) + 1, Mathf.PingPong(t, length - 1) + 1, 0);
    }
}
