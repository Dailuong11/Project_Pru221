using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health, MaxHealth, Width, Height;

    [SerializeField]
    private RectTransform healthBar;

    public void setMaxHeal(float maxHealth)
    {
        MaxHealth = maxHealth;
    }
    public void setHealth(float health)
    {
        Health = health;
        float newWidth = (Health / MaxHealth)*Width;
        healthBar.sizeDelta= new Vector2(newWidth, Height);
    }
}
