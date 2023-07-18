using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;


public class HealDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 3;
    public int maxHealth = 3;

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] heart;

    void Start()
    {
        
    }
    void Update()
    {
		health = PlayerPrefs.GetInt("tankHP");
		for (int i = 0; i < heart.Length; i++)
        {
            if (heart[i] != null) // Kiểm tra null trước khi gán sprite
            {
                if (i < health)
                {
                    heart[i].sprite = fullHeart;
                }
                else
                {
                    heart[i].sprite = emptyHeart;
                }

                if (i < maxHealth)
                {
                    heart[i].enabled = true;
                }
                else
                {
                    heart[i].enabled = false;
                }
            }
        }
    }

}
