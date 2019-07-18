using UnityEngine;
using System.Collections;

public class CameraAspectRatio : MonoBehaviour {


	
	Camera cam;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
        float aspectRatio = (((float)Screen.width / (float)Screen.height)/2);
		float cameraHeight = 1920f / aspectRatio;
		cam.orthographicSize = cameraHeight/192f;
	}
}
