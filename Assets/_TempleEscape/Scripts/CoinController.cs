using UnityEngine;
using System.Collections;
using SgLib;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    Collider2D col;
    public float Cash;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(SoundManager.Instance)
            SoundManager.Instance.PlaySound(SoundManager.Instance.coin);
            col.enabled = false;
                CharacterStats.CS.Cash+= Cash;
            Transform temp = CharacterStats.CS.coinText.Spawn(new Vector3(CharacterStats.CS.PlayerHUD.transform.position.x + Random.Range(-5f, 5f), CharacterStats.CS.PlayerHUD.transform.position.y + 0.8f, CharacterStats.CS.PlayerHUD.transform.position.z), transform.rotation);
            temp.SetParent(CharacterStats.CS.PlayerHUD.transform, false);
            temp.localPosition = Vector3.zero;
            temp.GetChild(0).GetComponent<Text>().text = Cash.ToString();
            temp.localScale = Vector3.one * 0.7f;
            this.Recycle();
        }      
    }

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(Vector2.Distance(CharacterStats.CS.transform.position,transform.position) < 5 || WaveManager.WaveComplete)
        {
            transform.position = Vector2.MoveTowards(transform.position, CharacterStats.CS.transform.position, 10 * Time.deltaTime);
        }
    }

    //Move up
    IEnumerator Up()
    {
        float time = 0.2f;
        float startY = transform.position.y;
        float endY = startY + 1f;

        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            float fraction = t / time;
            float newY = Mathf.Lerp(startY, endY, fraction);
            Vector3 newPos = transform.position;
            newPos.y = newY;
            transform.position = newPos;
            yield return null;
        }
        this.Recycle();
        transform.eulerAngles = Vector3.zero;
        transform.position = Vector3.zero;
    }
}
