using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public void Map()
    {
        //SceneManager.LoadScene("Menu");
    }
    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ReturnConstruction()
    {
        SceneManager.LoadScene("Menu");
    }
    public void SaveMap()
    {
        SceneManager.LoadScene("SaveMap");
    }
    public void Play()
    {
        int id = PlayerPrefs.GetInt("id");
        SceneManager.LoadScene("Play");
    }
    public void LoadMap()
    {
        SceneManager.LoadScene("LoadMap");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("First_Map");
    }
}
