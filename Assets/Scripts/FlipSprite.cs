using UnityEngine;
using System.Collections;

public class FlipSprite : MonoBehaviour
{

    public Vector3 originalscale;
    float originalsize;
    SpriteRenderer sr;
    float x;
    public bool flip;

    public enum Weapon
    {
        Melee,
        Gun}
    ;
    //set the original weapon ( can be set in the inspector )
    public Weapon TypeofWeapon;
	

    // Use this for initialization
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        originalscale = CharacterStats.CS.rightJoystick.GetInputDirection();
    }
	
    // Update is called once per frame
    void Update()
    {
        x = CharacterStats.CS.rightJoystick.GetInputDirection().x;

//        if (CharacterStats.CS.Dead == false) {
        if (CharacterStats.xAmount <= 0)
        {
            flip = true;

//            originalsize = -originalscale.y;
        }
        else{
            flip = false;
//            originalsize = originalscale.y;
        }
        sr.flipY = flip;
//        Debug.Log(CharacterStats.xAmount);
//        else if (x > 0)
//        {
//            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
//        }
//		}

//        transform.localScale = new Vector3(originalscale.x, originalsize, originalscale.z);
    }
}
