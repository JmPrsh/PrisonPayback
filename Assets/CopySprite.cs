using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopySprite : MonoBehaviour
{
   Image i;
   private void Awake()
   {
       i = GetComponent<Image>();
   }
   private void OnEnable()
   {
       i.sprite = CharacterStats.CS.SpriteGORenderer.sprite;
   }
}
