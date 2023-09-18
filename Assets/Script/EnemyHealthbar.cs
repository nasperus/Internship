using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    private void OnEnable()
    {
       
        Enemy.instance.OnHealthChanged += SetHealth;
    }

    private void OnDestroy()
    {

        Enemy.instance.OnHealthChanged -= SetHealth;
    }

    public void SetHealth(int health)
    {
        
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
