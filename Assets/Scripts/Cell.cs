using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private int _depth;
    private int _strength;
    private int _ingotDepth = -1;
    private int _stageIndex = 0;
    private float _deviationY = 0.05f;
    private int _treasureDepth; // if -1 , treasure is not in this cell;
    private CellType[] _cellTypes;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(int depth , CellType[] cellTypes , bool hasIngot = false )
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _depth = depth;
        _cellTypes = cellTypes;
        DescendingSort(ref _cellTypes);
        if (hasIngot)
        {
            CalculateIngotDepth();
        }
        SetStage(_cellTypes[_stageIndex]);
    }

    private void DescendingSort(ref CellType[] arr)
    {
        CellType t;
        for (int i = 0; i < arr.Length - 1 ; i++)
        {
            for (int j = i; j < arr.Length; j++)
            {
                if(arr[i].GetSpawnDepth() < arr[j].GetSpawnDepth())
                {
                    t      = arr[j];
                    arr[j] = arr[i];
                    arr[i] = t;
                }
            }
        }
    }

    private void CalculateIngotDepth()
    {
        _ingotDepth = Random.Range(1 , _depth);
    }

    private void SetStage(CellType cellType)
    {
        _strength = cellType.GetStrength();
        if (_depth == _ingotDepth) ChangeSprite(cellType.GetIngotSprite());
        else ChangeSprite(cellType.GetSprite());
        transform.position = new Vector3(transform.position.x,transform.position.y -_deviationY,transform.position.z);
    }

    public void DigCell()
    {
        _strength--;
        if(_strength<=0)
        {
            _depth--;
            _stageIndex++;
            if (_stageIndex >= _cellTypes.Length) DestroyCell();
            else SetStage(_cellTypes[_stageIndex]);
        }
    }

    void ChangeSprite(Sprite cellSprite)
    {
        _spriteRenderer.sprite = cellSprite;
    }

    void DestroyCell()
    {
        Destroy(this.gameObject);
    }
}
