using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public int currentHP; 
    public int maxHP = 100;

    public bool spottedByEnemy, takingDamage, regenerating, alreadyRegenerating;

    public WaitForSeconds regenTime = new WaitForSeconds(0.075f);
    public WaitForSeconds damageTime = new WaitForSeconds(0.5f);

    private void Start()
    {
        SetMaxHealth(maxHP);
        currentHP = maxHP;
        SetHealth(currentHP);
        spottedByEnemy = false;
        takingDamage = false;
        regenerating = false;
        alreadyRegenerating = false;
    }

    private void Update()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
        
        if (spottedByEnemy && !takingDamage)
            StartCoroutine(TakeDamage());
        else if (!spottedByEnemy && takingDamage)
        {
            takingDamage = false;
            regenerating = true;
        }
        else if (regenerating && !alreadyRegenerating)
            StartCoroutine(RegenerateHealth());
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public IEnumerator TakeDamage()
    {
        takingDamage = true;
        
        while (currentHP > 0 && takingDamage)
        {
            currentHP -= 1;
            SetHealth(currentHP);
            yield return damageTime;
        }
        
    }
    
    public IEnumerator RegenerateHealth()
    {
        alreadyRegenerating = true;
        
        yield return new WaitForSeconds(2);
        
        while (currentHP < maxHP && !spottedByEnemy && !takingDamage && regenerating)
        {
            currentHP += 1;
            SetHealth(currentHP);
            yield return regenTime;

            if (currentHP >= maxHP)
            {
                regenerating = false;
                alreadyRegenerating = false;
            } 
        }
        
    }

    public void UsePotion()
    {
        if (currentHP >= 100)
            return;
        
        if (currentHP < 50)
            currentHP += 50;
        else
            currentHP = 95;
    }

}
