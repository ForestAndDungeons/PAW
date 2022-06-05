using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer
{
    Image[] _imageUIHearts;
    Sprite[] _spriteHeart;
    
    Image[] _imageUIArmor;
    Sprite[] _spriteArmor;

    public UIPlayer(Image[] imageUIHearts, Sprite[] spriteHeart, Image[] imageUIArmor, Sprite[] spriteArmor)
    {
        _imageUIHearts = imageUIHearts;
        _spriteHeart = spriteHeart;

        _imageUIArmor = imageUIArmor;
        _spriteArmor = spriteArmor;
    }

    public void UIArtificialUpdate(float maxHealth, float currentHealth, float armor)
    {
        var _maxHealth = maxHealth;
        var _currentHealth = currentHealth;
        var _armor = armor;

        //Iguala la cantidad de corazones a mi vida maxima, y decide si mostrar el corazon vacio o lleno de acuerdo a mi vida actual
        for (int i = 0; i < _imageUIHearts.Length; i++)
        {
            if (i < _currentHealth)
            {
                _imageUIHearts[i].sprite = _spriteHeart[0];
            }
            else
            {
                _imageUIHearts[i].sprite = _spriteHeart[1];
            }

            if (i < _maxHealth)
            {
                _imageUIHearts[i].enabled = true;
            }
            else
            {
                _imageUIHearts[i].enabled = false;
            }
        }

        for (int i = 0; i < _imageUIArmor.Length; i++)
        {
            if (i < armor)
            {
                _imageUIArmor[i].enabled = true;
                _imageUIArmor[i].sprite = _spriteArmor[0];
            }
            else
            {
                _imageUIArmor[i].enabled = false;
            }
        }
    }
}