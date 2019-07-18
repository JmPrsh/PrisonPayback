using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class tolevelmap : MonoBehaviour {

    public string levelName;

	
	// Update is called once per frame
	public void LoadScene () {
        PlayerPrefs.SetString("LevelToLoad", levelName);
        SceneManager.LoadScene("LoadingScreen");
	}

    public void LoadSceneInstantly(){
        SceneManager.LoadScene(levelName);
    }
}
