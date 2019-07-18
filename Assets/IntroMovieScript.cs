using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroMovieScript : MonoBehaviour
{

    //Can't be Texture, you need create an Object and later convert in Texture
    public Object[] botao;
    public Object[] botao2;

    //Change this for speed
    public float framesPerSecond = 10;

    Image i;
    public int introID;
    

    void Awake()
    {
       
//        Application.targetFrameRate = 600;
        i = GetComponent<Image>();
        //Change "MaoEsquerda" for the Folder name (thanks robertbu)

        botao = Resources.LoadAll<Sprite>("MenuBeginning");
        botao2 = Resources.LoadAll<Sprite>("LoopMenu");
    }
    public int index;
    public int index2;
    float timetoadd;
    float timetoadd2;
    void Update()
    {
       

        //Now you convert the Object to texture
        if (introID == 0)
        {
            timetoadd += Time.deltaTime;
            index = (int)(Time.time * framesPerSecond) % botao.Length;
            i.sprite = botao[index] as Sprite;
        }
        else
        {
            timetoadd2 += Time.deltaTime * framesPerSecond;
            index2 = (int)timetoadd2;
            i.sprite = botao2[index2] as Sprite;
        }

        if (introID == 0)
        {
            if (index > botao.Length - framesPerSecond)
            {
                index = 0;
                if (introID < 1)
                {
                    introID = 1;
                   
                }
               
            }
        }
        else
        {
            if (index > botao2.Length - framesPerSecond)
            {
                index = 0;
            }
        }
    }
}
