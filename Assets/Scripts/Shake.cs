using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {
    public static Shake s;
	public static float shake = 0f;
	public float Shaking;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	public Transform camTransform;
	Vector3 originalPos;

	void Awake()
	{
        s = this;
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Shaking = shake;
		if (shake > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			camTransform.localPosition = originalPos;
		}


	}
}
