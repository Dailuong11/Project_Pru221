using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string firstMapSceneName = "First_Map";

    private void Start()
    {
        StartCoroutine(LoadFirstMapSceneAfterDelay());
    }

    private IEnumerator LoadFirstMapSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f); 

        SceneManager.LoadScene(firstMapSceneName); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
