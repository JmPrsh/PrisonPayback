using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Util_TTFDisableBilinearFilter : MonoBehaviour
{

	public Font[] _ttf;

	public FilterMode _filter = FilterMode.Point;

	// Use this for initialization
	void Awake ()
	{
//        if(_ttf.material.mainTexture.filterMode != _filter) {
//
//            _ttf.material.mainTexture.filterMode = _filter;
//        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < _ttf.Length; i++) {
			if (_ttf[i].material.mainTexture) {
				if (_ttf[i].material.mainTexture.filterMode != _filter) {
			
					_ttf[i].material.mainTexture.filterMode = _filter;
				}
			}
		}
	}
}
