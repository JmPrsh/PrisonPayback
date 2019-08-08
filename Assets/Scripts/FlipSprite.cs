using UnityEngine;
using System.Collections;

public class FlipSprite : MonoBehaviour
{
    float defaultScale;
    float originalsize;
    SpriteRenderer sr;
    float x;
    public bool PlayerGO;
    attackPlayer enemy;

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
        if (!PlayerGO)
        {
            enemy = transform.parent.parent.GetComponent<attackPlayer>();
        }
        sr = this.GetComponent<SpriteRenderer>();
        // originalscale = CharacterStats.CS.rightJoystick.GetInputDirection();
        defaultScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CharacterStats.CS)
            return;

        if (PlayerGO)
        {
            x = CharacterStats.CS.rightJoystick.GetInputDirection().x;
            float direction = x > 0 ? defaultScale : -defaultScale;
            transform.localScale = new Vector3(defaultScale, direction, defaultScale);
        }
        else
        {
           

            x = CharacterStats.CS.transform.position.x > enemy.ChildSprite.transform.position.x ? 1 : -1;
            float direction = x > 0 ? defaultScale : -defaultScale;
            enemy.flipLook = x;
            transform.localScale = new Vector3(defaultScale, direction, defaultScale);
        }
    }

}
