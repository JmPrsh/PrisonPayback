using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class FuryMode : MonoBehaviour
{

    public EnergyBar EB;
    public GameObject Fire;
    public static bool FuryActivated;
    public static float BarValue;
    public Camera cam;
    bool zoomIn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //		EB.valueCurrent = (int)BarValue;
        //		if (BarvalueCurrent> EB.valueMax) {
        //			BarvalueCurrent= EB.valueMax);
        //		}

        //if (Tutorial.AllowFury)
        //{
        //    if (Input.GetButtonDown("LeftAnalogueStickPushed") && Input.GetButtonDown("RightAnalogueStickPushed"))
        //    {
        //        if (EB.valueCurrent>= EB.valueMax)
        //        {
        //            EB.valueCurrent= EB.valueMax;
        //            FuryActivated = true;
        //            zoomIn = true;
        //            //					StartCoroutine (AdjustFuryBar ());
        //        }
        //        else
        //        {
        //            Debug.Log("Fury Bar not filled!");
        //        }
        //    }
        //}

        if (FuryActivated)
        {
            Fire.SetActive(true);
            if (EB.valueCurrent> 0)
            {
                EB.valueCurrent--;
            }
            else
            {
                FuryActivated = false;
            }
        }
        else
        {
            zoomIn = false;
            Fire.SetActive(false);
        }
        if (zoomIn)
        {
            if (cam.GetComponent<Camera>().orthographicSize > 5)
            {
                cam.GetComponent<Camera>().orthographicSize -= 4 * Time.deltaTime;
            }
        }
        else
        {
            if (cam.GetComponent<Camera>().orthographicSize < 6)
            {
                cam.GetComponent<Camera>().orthographicSize += 2 * Time.deltaTime;
            }
        }

    }

    public void FuryButton()
    {
        if (Tutorial.AllowFury)
        {
            if (EB.valueCurrent>= EB.valueMax)
            {
                EB.valueCurrent= EB.valueMax;
                FuryActivated = true;
                zoomIn = true;
                //					StartCoroutine (AdjustFuryBar ());
            }
            else
            {
                Debug.Log("Fury Bar not filled!");
            }

        }
    }

    IEnumerator AdjustFuryBar()
    {
        while (EB.valueCurrent!= 0)
        {
            if (EB.valueCurrent> 0)
            {
                EB.valueCurrent--;

            }
            else
            {
                FuryActivated = false;
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(AdjustFuryBar());
        }


    }


}
