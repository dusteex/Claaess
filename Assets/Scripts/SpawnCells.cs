using System.Collections.Generic;
using UnityEngine;
public class SpawnCells : MonoBehaviour
{
    [SerializeField] private GameObject _cell;
    [SerializeField] private Transform _anchorTransform; // basically anchor position is  top left 
    [SerializeField] private Transform _fieldTransform;
    [SerializeField] private GameValues _gameValues;
    [SerializeField] private GameStateHandler _gameStateHandler;

    private List<Vector2> _ingotCells = new List<Vector2>();
    private Vector3 _anchorPosition;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        CalculateIngotCellsCoordinates(_gameValues.GetRowsCount() , _gameValues.GetColumnsCount(), _gameValues.GetIngotsCount());

        _anchorPosition = _anchorTransform.position;
        for(int i = 0; i < _gameValues.GetRowsCount(); i++)
        {
            for(int j = 0; j < _gameValues.GetColumnsCount(); j++)
            {
                Vector3 cellPosition = _anchorPosition + new Vector3(j * _gameValues.GetOffsetX(), i * _gameValues.GetOffsetY(), 0);
                GameObject currentCell = Instantiate(_cell, cellPosition, Quaternion.identity);
                SetCorrectOrder(ref currentCell, i);
                currentCell.transform.SetParent(_fieldTransform);
                currentCell.GetComponent<Cell>().Init(_gameValues.GetMaxDepth(), _gameValues.GetCellTypes(),_gameStateHandler , _ingotCells.Contains(new Vector2(i, j))) ;
            }
        }
    }

    private void SetCorrectOrder(ref GameObject cell , int sortingOrderLevel)
    {
        cell.GetComponent<SpriteRenderer>().sortingOrder = sortingOrderLevel;
    }

    private void CalculateIngotCellsCoordinates(int rowsCount , int columnsCount , int ingotsCount )
    {
        Vector2 randomCoords;
        for (int i = 0; i < ingotsCount; i++)
        {
            randomCoords = new Vector2(Random.Range(0, rowsCount), Random.Range(0, columnsCount));

            while (_ingotCells.Contains(randomCoords)) // It`s need because 2 or more ingots shouldn`t be in 1 cell 
            {
                randomCoords = new Vector2(Random.Range(0, rowsCount), Random.Range(0, columnsCount));
            }
            _ingotCells.Add( randomCoords ) ;
        }
    }
}
