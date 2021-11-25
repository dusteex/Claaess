using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitnessMenu : MonoBehaviour
{
    private bool _isOpen = false;
    private Animator _animator;

    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }
    public void Toggle()
    {
        _isOpen = !_isOpen;
        GameStateHandler.PlayerCanDig = !_isOpen;
        _animator.SetBool("isOpen", _isOpen);
    } 

}
