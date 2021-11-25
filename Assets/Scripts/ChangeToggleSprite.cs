using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeToggleSprite : MonoBehaviour
{
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _unactiveSprite;
    [SerializeField] private Image _toggleImage;
    private void Start()
    {
        
    }

    public void Change(bool isActive)
    {
        if (isActive == true) _toggleImage.sprite = _activeSprite;
        else _toggleImage.sprite = _unactiveSprite;
    }

}
