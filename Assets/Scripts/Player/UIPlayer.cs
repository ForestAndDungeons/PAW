using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer
{
    float _maxHealth;
    float _currentHealth;

    Image[] _imageUIHearts;
    Sprite[] _spriteHeart;

    public UIPlayer(Image[] hearts, Sprite[] spriteHeart)
    {
        _imageUIHearts = hearts;
        _spriteHeart = spriteHeart;
    }

    public void UIArtificialUpdate(float maxHealth, float currentHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = currentHealth;

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
    }
}