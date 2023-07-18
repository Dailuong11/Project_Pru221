using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CountDefeated : MonoBehaviour
{
    public TextMeshProUGUI txt_count;
    public TextMeshProUGUI txt_target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        txt_target.text = "Target: " + PlayerPrefs.GetInt("id") * 2;
        txt_count.text = "Defeated: " + PlayerPrefs.GetInt("Point"); ;
    }
}
