using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarCrash : MonoBehaviour
{

    public Rigidbody2D car, truck;
    Camera cam;
    bool done = false, still = true;
    public AudioSource slut, _car, _truck;

    public TextMeshProUGUI kills, deaths;
    public TextMeshProUGUI[] texts = new TextMeshProUGUI[5];
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        for (int i = 0; i <= 4; i++)
        {
            texts[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (done == true && still == true) cam.transform.position = new Vector3(car.transform.position.x, car.transform.position.y, cam.transform.position.z);
    }

    IEnumerator Crash()
    {
        _car.Play();
        car.velocity = new Vector3(-10, 0, 0);
        _truck.Play();
        truck.velocity = new Vector3(0, 10, 0);

        yield return new WaitForSeconds(6.6f);
        car.velocity = new Vector3(0, 0, 0);
        truck.velocity = new Vector3(0, 0, 0);
        still = false;
        cam.transform.position = new Vector3(0, 13.2f, -10);
        _car.Stop();
        _truck.Stop();

        slut.Play();
        yield return new WaitForSeconds(slut.clip.length+2);

        for (int i = 0; i <= 4; i++)
        {
            texts[i].enabled = true;
        }

        kills.text = App.Instance.killed.ToString();
        deaths.text = App.Instance.death.ToString();

        StartCoroutine(fadeOut(texts, 5f));
    }


    IEnumerator fadeOut(TextMeshProUGUI[] MyRenderer, float duration)
    {
        float v = 0f;
        Color spriteColor = new Color(255, 255, 255, v);

        while (spriteColor.a < 1.0f)
        {
            for (int i = 0; i <= 4; i++)
            {
                MyRenderer[i].color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, v);
            }

            v += (Time.deltaTime / duration);
            yield return null;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(col.gameObject);
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy e in enemies)
            {
                Destroy(e);
            }

            done = true;
            StartCoroutine(Crash());
        }
    }
}
