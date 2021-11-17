using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    [SerializeField] private int _rowsCount;
    [SerializeField] private int _columnsCount;
    [SerializeField] private int _maxDepth;
    [SerializeField] private int _ingotsCount;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private CellType[] _cellTypes;

    public void GetGameValues(ref int rowsCount , ref int columnsCount , ref float offsetX , ref float offsetY,  ref int maxDepth , ref int ingotsCount, ref CellType[] cellTypes)
    {
        rowsCount    = _rowsCount;
        columnsCount = _columnsCount;
        offsetX      = _offsetX;
        offsetY      = _offsetY;
        maxDepth     = _maxDepth;
        cellTypes    = _cellTypes;
        ingotsCount  = _ingotsCount;
    }

}
