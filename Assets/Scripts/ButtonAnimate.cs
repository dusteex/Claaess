using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimate : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isActive = false;

    public void Toggle()
    {
        _isActive = !_isActive;
        _animator.SetBool("isActive" , _isActive);
    }
}
