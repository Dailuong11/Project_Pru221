using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour
{
    FadeInOut fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>(); 
    }
    public IEnumerator ChangeScene()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Play_map_2");
    }
    public IEnumerator ChangeScene_2()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Play_map_3");
    }
    public IEnumerator ChangeScene_3()
    {
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Play_map_4");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "tank")
        {
            StartCoroutine(ChangeScene());
        }
        if (collision.gameObject.tag == "tank_2")
        {
            StartCoroutine(ChangeScene_2());
        }
        if (collision.gameObject.tag == "tank_3")
        {
            StartCoroutine(ChangeScene_3());
        }
   
    }
}
