using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelload : MonoBehaviour
{

	public string LTL;
	AsyncOperation async;
	public GameObject[] progressGO;
	bool load;
	public Texture2D LoadingBar;

	void Awake ()
	{
		LTL = PlayerPrefs.GetString ("LevelToLoad");
//		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeOut");
	}
	// Use this for initialization

	void Start(){
		StartCoroutine (waitAsecond ());
	}



	IEnumerator waitAsecond () 
	{
		yield return new WaitForSeconds (2);
		StartCoroutine(Load ());
	}

	IEnumerator Load () 
	{
        async = SceneManager.LoadSceneAsync(LTL);
		async.allowSceneActivation = false;
		yield return async.isDone;
//		yield return new WaitForSeconds (1);
//		GameObject.Find ("Fade").GetComponent<Animator> ().SetTrigger ("FadeIn");
		yield return new WaitForSeconds (1);
		SwitchScene();
	}

	private void SwitchScene()
	{
		if (async != null)
			async.allowSceneActivation = true;
	}

	

}
