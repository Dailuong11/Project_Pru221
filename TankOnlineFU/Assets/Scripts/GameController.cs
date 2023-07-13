using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int id;
    private void Start()
    {
        id = PlayerPrefs.GetInt("id");
        Debug.Log(id);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
