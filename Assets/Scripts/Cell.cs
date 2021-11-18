using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    private int _depth;
    private int _maxDepth;
    private int _strength;
    private int _ingotDepth = -1;
    private float _deviationY = 0.05f;
    private CellType[] _cellTypes;
    private SpriteRenderer _spriteRenderer;
    private GameStateHandler _gameStateHandler;
    private bool _stageHasIngot;
    public void Init(int depth , CellType[] cellTypes, GameStateHandler gameStateHandler ,  bool hasIngot = false)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _depth = depth;
        _maxDepth = depth;
        _cellTypes = cellTypes;
        _gameStateHandler = gameStateHandler;

        DescendingSort(ref _cellTypes);

        if (hasIngot)
        {
            CalculateIngotDepth();
        }

        SetStage(_cellTypes[_maxDepth - _depth]);
    }


    private void CalculateIngotDepth()
    {
        _ingotDepth = Random.Range(1 , _depth);
    }

    public void DigCell()
    {
        if(_depth == _ingotDepth)
        {
            PickIngot();
        }
        else
        {
            _strength--;
            _gameStateHandler.ReduceToolDurability(1);

            if (_strength <= 0)
            {
                _depth--;
                if (_depth <= 0) DestroyCell();
                else SetStage(_cellTypes[_maxDepth - _depth]);
            }
        }

    }


    private void SetStage(CellType cellType)
    {
        _strength = cellType.GetStrength();

        if (_depth == _ingotDepth)
        {
            ChangeSprite(cellType.GetIngotSprite());
        }
        else ChangeSprite(cellType.GetSprite());

        transform.position = new Vector3(transform.position.x,transform.position.y -_deviationY,transform.position.z);
    }

    private void PickIngot()
    {
        Debug.Log("Picked");
        _gameStateHandler.CollectIngot();
        ChangeSprite(_cellTypes[_maxDepth - _depth].GetSprite());
        _ingotDepth = -1;
    }
    private void ChangeSprite(Sprite cellSprite)
    {
        _spriteRenderer.sprite = cellSprite;
    }

    private void DestroyCell()
    {
        Destroy(this.gameObject);
    }

    private void DescendingSort(ref CellType[] arr)
    {
        CellType t;
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = i; j < arr.Length; j++)
            {
                if (arr[i].GetSpawnDepth() < arr[j].GetSpawnDepth())
                {
                    t = arr[j];
                    arr[j] = arr[i];
                    arr[i] = t;
                }
            }
        }
    }
}
