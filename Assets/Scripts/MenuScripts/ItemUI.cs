using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _description;
    [SerializeField] Animator _animController;

    public void ActiveInterface(string title, string description)
    {
        _title.text = title;
        _description.text = description;
        _animController.SetTrigger("IsActive");
    }

}
