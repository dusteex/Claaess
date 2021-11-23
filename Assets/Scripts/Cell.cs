using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    private int _depth;
    private int _maxDepth;
    private int _cellStrength;
    private int _ingotDepth = -1;
    private float _deviationY = 0.05f;
    private CellType[] _cellTypes;
    private SpriteRenderer _spriteRenderer;
    private GameStateHandler _gameStateHandler;
    public void Init(int maxDepth , CellType[] cellTypes, GameStateHandler gameStateHandler , int ingotDepth = -1)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _maxDepth = maxDepth;
        _depth = maxDepth;
        _ingotDepth = ingotDepth;

        _cellTypes = cellTypes;
        _gameStateHandler = gameStateHandler;

        DescendingSort(ref _cellTypes);
        SetStage(_cellTypes[_maxDepth - _depth]);
    }



    public void DigCell()
    {
        if(_depth != _ingotDepth)
        {
            _cellStrength--;
            _gameStateHandler.ReduceToolDurability(1);
            _gameStateHandler.PlaySound(_cellTypes[_maxDepth - _depth].GetRandomBreakSound());

            if (_cellStrength <= 0)
            {
                _depth--;
                if (_depth < 0) DestroyCell();
                else SetStage(_cellTypes[_maxDepth - _depth]);
            }
        }
        else
        {
            PickIngot();
        }

    }


    private void SetStage(CellType cellType)
    {
        _cellStrength = cellType.GetStrength();

        if (_depth == _ingotDepth)
        {
            ChangeSprite(cellType.GetIngotSprite());
        }
        else ChangeSprite(cellType.GetSprite());

        transform.position = new Vector3(transform.position.x,transform.position.y -_deviationY,transform.position.z);
    }

    private void PickIngot()
    {
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
