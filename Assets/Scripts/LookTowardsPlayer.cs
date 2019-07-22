using UnityEngine;
using System.Collections;

public class LookTowardsPlayer : MonoBehaviour
{
    attackPlayer aP;
    GameObject Player;
    // public Transform[] Targets;
    public GameObject Target;

    // Update is called once per frame

    void Start()
    {
        aP = GetComponentInParent<attackPlayer>();
    }

    void Update()
    {

        if (Target)
        {

            if (aP.seenPlayer && !aP.reload)
            {

                Player = GameObject.FindGameObjectWithTag("Player");
                if (Vector2.Distance(Target.transform.position, transform.position) <= aP.EnemyType.AttackDistance)
                    aP.seenPlayer = true;

                var dir = Target.transform.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                var newRotation = Quaternion.AngleAxis(angle - 90f, transform.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * aP.EnemyType.AimSpeed);


            }
            else
            {
                var dir = Target.transform.position - transform.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                var newRotation = Quaternion.AngleAxis(angle - 90f, transform.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime / 2);
            }
        }
        else
        {
            Target = aP.Target;
        }
    }

}
