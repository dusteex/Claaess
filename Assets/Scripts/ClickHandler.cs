using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }

    private void Click()
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit)
        {
            GameObject clickedCell = hit.collider.gameObject;
            clickedCell.GetComponent<Cell>().DigCell();
        }
    }

}
