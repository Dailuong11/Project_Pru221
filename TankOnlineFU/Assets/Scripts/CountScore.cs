using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountScore : MonoBehaviour
{
	public Text txtScore;
	// Start is called before the first frame update
	void Start()
    {
		PlayerPrefs.SetInt("Point", 0);
	}

    // Update is called once per frame
    void Update()
    {
		txtScore.text = "Score: " + PlayerPrefs.GetInt("Point") * 100;
	}
}
