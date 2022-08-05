using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoss 
{
    Slider _sliderHealthBar;
    public UIBoss(Slider sliderHealthbar)
    {
        _sliderHealthBar = sliderHealthbar;
    }
    public void UIBossArtifitialUptade(float maxHealth,float currentHealth)
    {
        var _maxHealth = maxHealth;
        var _currentHealth = currentHealth;
        _sliderHealthBar.value = _currentHealth / _maxHealth;
    }
}