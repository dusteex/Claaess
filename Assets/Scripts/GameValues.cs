using UnityEngine;

public class GameValues:MonoBehaviour
{
    [SerializeField] private int _rowsCount;
    [SerializeField] private int _columnsCount;
    [SerializeField] private int _maxDepth;
    [SerializeField] private int _ingotsCount;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _toolDurability;
    [SerializeField] private CellType[] _cellTypes;

    public int GetRowsCount()
    {
        return _rowsCount;
    }
    public int GetColumnsCount()
    {
        return _columnsCount;
    }
    public int GetMaxDepth()
    {
        return _maxDepth;
    }
    public int GetIngotsCount()
    {
        return _ingotsCount;
    }
    public float GetOffsetX()
    {
        return _offsetX;
    }

    public float GetOffsetY()
    {
        return _offsetY;
    }
    public float GetToolDurability()
    {
        return _toolDurability;
    }

    public CellType[] GetCellTypes()
    {
        return _cellTypes;
    }




}
