using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [Header("TMP Variables")]
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _description;
    [Header("Animator")]
    [SerializeField] Animator _animController;
    [Header("AudioSource")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    public void ActiveInterface(string title, string description)
    {
        _title.text = title;
        _description.text = description;
        _animController.SetTrigger("IsActive");
    }

    public void popUp_Sound()
    {
        _audioSource.PlayOneShot(_audioClip);
    }

}
