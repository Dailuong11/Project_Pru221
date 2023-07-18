using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LockMap : MonoBehaviour
{
	public UnityEngine.UI.Button map1;
	public UnityEngine.UI.Button map2;
	public UnityEngine.UI.Button map3;
	public UnityEngine.UI.Button map4;
	void Start()
	{
		
	}
	// Update is called once per frame
	void Update()
	{
		int id = PlayerPrefs.GetInt("id");
		if (id <= 1)
		{

			map1.interactable = true;
			map2.interactable = false;
			map3.interactable = false;
			map4.interactable = false;
		}
		else if (id <= 2)
		{
			map1.interactable = true;
			map2.interactable = true;
			map3.interactable = false;
			map4.interactable = false;
		}
		else if (id <= 3)
		{
			map1.interactable = true;
			map2.interactable = true;
			map3.interactable = true;
			map4.interactable = false;
		}
		else if (id <= 4)
		{
			map1.interactable = true;
			map2.interactable = true;
			map3.interactable = true;
			map4.interactable = true;
		}

		Debug.Log(id);

	}
}
