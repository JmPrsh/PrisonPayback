using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Settings : MonoBehaviour
{
    public GameObject MusicBar;
    public GameObject SoundFXBar;
    public EnergyBar musicBar;
    public EnergyBar soundBar;

    public static Color c;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            MusicBar.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
            SoundFXBar.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundFXVolume");
        }

        MusicBar.GetComponent<Slider>().value = 1;
        SoundFXBar.GetComponent<Slider>().value = 1;

        musicBar.valueCurrent = (int)MusicBar.GetComponent<Slider>().value;
        soundBar.valueCurrent = (int)SoundFXBar.GetComponent<Slider>().value;


    }

    // Update is called once per frame
    public void UpdateSound(int id)
    {
        switch (id)
        {
            case 0:
                musicBar.valueCurrent = (int)MusicBar.GetComponent<Slider>().value;
                PlayerPrefs.SetFloat("Volume", MusicBar.GetComponent<Slider>().value);
                break;
            case 1:
                soundBar.valueCurrent = (int)SoundFXBar.GetComponent<Slider>().value;
                PlayerPrefs.SetFloat("SoundFXVolume", SoundFXBar.GetComponent<Slider>().value);
                break;
        }
    }


    public void LoadScene()
    {
        Application.LoadLevel("TitleScreen");
    }

    public void hightbutton()
    {
        GetComponent<AudioSource>().clip = Menu.staticHighlightsound;
        GetComponent<AudioSource>().Play();

    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().clip = Menu.staticPressedsound;
        GetComponent<AudioSource>().Play();

    }
}
