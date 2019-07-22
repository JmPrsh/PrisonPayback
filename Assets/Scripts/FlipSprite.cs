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
        Gun
    }
    ;
    //set the original weapon ( can be set in the inspector )
    public Weapon TypeofWeapon;


    // Use this for initialization
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        // originalscale = CharacterStats.CS.rightJoystick.GetInputDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterStats.CS)
            return;
            
        x = CharacterStats.CS.rightJoystick.GetInputDirection().x;
        flip = CharacterStats.xAmount <= 0;
        sr.flipY = flip;
    }
}
