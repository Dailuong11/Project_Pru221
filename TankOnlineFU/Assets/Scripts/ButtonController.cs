using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public List<Button> buttons;
    private void Start()
    {
        foreach (Button item in buttons)
        {
            item.onClick.AddListener(() => ButtonClick(item));
        }
    }
    public void OnButtonClick()
    {

        TMP_Text buttonText = GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            string text = buttonText.text;
            Debug.Log("Button Text: " + text);
        }
        else
        {
            print("đéo có");
        }
    }
    private void ButtonClick(Button button)
    {
        string buttonText = button.name;
        string id = buttonText.Replace("Button map ", "");
        PlayerPrefs.SetInt("id", int.Parse(id));
        SceneManager.LoadScene("Menu");

    }
}
