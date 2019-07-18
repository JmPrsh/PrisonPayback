using UnityEngine;
using System.Collections;

public class BlastDamage : MonoBehaviour
{

    public int Damage;
    Collider2D col;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<Collider2D>().CompareTag("Player"))
        {
            col.enabled = false;
            try
            {

                CharacterStats.CS.Health -= Damage;
                Shake.shake = 1;
                if (CharacterStats.CS.Health <= 0)
                {

                    if (this.transform.position.x > other.transform.position.x)
                    {
                        Time.timeScale = 0.3f;

                        CharacterStats.flipped = true;
                    }
                }

            }
            catch
            {
            }
        }
        if (other.gameObject.GetComponent<Collider2D>().CompareTag("enemytarget"))
        {
            Debug.Log("Hit target");
            //			GetComponent<CircleCollider2D> ().enabled = false;
            Debug.Log("Damage " + Damage);
            try
            {

                Debug.Log("Damage " + Damage);
                other.GetComponentInParent<attackPlayer>().SendMessage("dmg", (Damage / 2));
                Shake.shake = 1;
                if (other.GetComponentInParent<attackPlayer>().health <= 0)
                {
                    CharacterStats.Combo += 1;
                    int ScoreGiven = 10 * CharacterStats.Combo;
                    CharacterStats.Score += ScoreGiven;

                    if (this.transform.position.x > other.transform.position.x)
                    {
                        Time.timeScale = 0.3f;

                        other.GetComponentInParent<attackPlayer>().flipped = true;
                    }
                }

            }
            catch
            {
            }
        }
    }

}
