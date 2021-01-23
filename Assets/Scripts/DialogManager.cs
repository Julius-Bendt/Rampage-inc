using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Juto.Audio;

public class DialogManager : MonoBehaviour
{
    public DialogGroup[] dialogs;
    public TextMeshProUGUI text;

    private bool beenActive = false;

    private const float LERPTIME = 0.4f;
    Enemy e;
    Player p;

    private void OnTriggerEnter2D(Collider2D o)
    {
        if(o.gameObject.CompareTag("Player") && !beenActive)
        {
            beenActive = true;
            StartCoroutine(_Startdialog());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(e != null)
        {
            if (e.dead || p.dead)
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }
               
        }
    }

    IEnumerator _Startdialog()
    {
        //Stop player and enemy
        p = FindObjectOfType<Player>();
        e = FindObjectOfType<Enemy>();

        p.gameObject.GetComponent<Character>().rig.freezeRotation = true; 
        p.gameObject.GetComponent<Character>().charAnim.SetBool("moving", false);

        e.enabled = false;
        p.enabled = false;

        foreach (Dialog dialog in dialogs[0].dialog)
        {
            text.text = dialog.dialog;
            text.GetComponent<RectTransform>().anchoredPosition = dialog.position;
            AudioController.PlaySound(dialog.c);
            yield return new WaitForSeconds(1f);
        }

        p.gameObject.GetComponent<Character>().rig.freezeRotation = false;
        p.enabled = true;
        InteractText.ChangeText("Grab the knife, and aim it at your boss!",gameObject.GetInstanceID());

        float elapsedTime = 0;

        while (elapsedTime < LERPTIME)
        {
            elapsedTime += Time.deltaTime;
            Time.timeScale =  Mathf.Lerp(1, 0.6f, elapsedTime / LERPTIME);
        }

        //Slow mo
        yield return new WaitForSeconds(1.5f);
        e.enabled = true;
        if (InteractText.id == gameObject.GetInstanceID())
            InteractText.ChangeText("",0);

        yield return new WaitForSeconds(3);

        while (elapsedTime < LERPTIME && !e.enabled)
        {
            elapsedTime += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(0.6f, 1, elapsedTime / LERPTIME);
        }
    }
}

[System.Serializable]
public struct Dialog
{
    public string dialog;
    public Vector2 position;
    public AudioClip c;
}

[System.Serializable]
public struct DialogGroup
{
    public Dialog[] dialog;
}
