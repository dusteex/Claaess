using System.Collections.Generic;
using UnityEngine;
public class SpawnCells : MonoBehaviour
{
    [SerializeField] private GameObject _cell;
    [SerializeField] private Transform _anchorTransform; // basically anchor position is  top left 
    [SerializeField] private Transform _fieldTransform;
    [SerializeField] private GameValues _gameValues;
    [SerializeField] private GameStateHandler _gameStateHandler;
    [SerializeField] private WitnessManager _witnessManager;

    private List<Vector3Int> _ingotCells = new List<Vector3Int>();
    private Vector3 _anchorPosition;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        CalculateIngotCellsCoordinates(_gameValues.GetRowsCount() , _gameValues.GetColumnsCount(), _gameValues.GetMaxDepth() , _gameValues.GetIngotsCount());
        _witnessManager.SpawnWitnesses(_ingotCells);
        _anchorPosition = _anchorTransform.position;
        for(int i = 0; i < _gameValues.GetRowsCount(); i++)
        {
            for(int j = 0; j < _gameValues.GetColumnsCount(); j++)
            {
                Vector3 cellPosition = _anchorPosition + new Vector3(j * _gameValues.GetOffsetX(), i * _gameValues.GetOffsetY(), 0);
                GameObject currentCell = Instantiate(_cell, cellPosition, Quaternion.identity);
                SetCorrectOrder(ref currentCell, i);
                currentCell.transform.SetParent(_fieldTransform);
                currentCell.GetComponent<Cell>().Init(_gameValues.GetMaxDepth(), _gameValues.GetCellTypes(),_gameStateHandler , GetIngotPositionInCell(new Vector2(j,i), _ingotCells) ) ;
            }
        }
    }

    private void SetCorrectOrder(ref GameObject cell , int sortingOrderLevel)
    {
        cell.GetComponent<SpriteRenderer>().sortingOrder = sortingOrderLevel;
    }

    private void CalculateIngotCellsCoordinates(int rowsCount , int columnsCount , int depth ,  int ingotsCount )
    {
        Vector3Int randomCoords;
        for (int i = 0; i < ingotsCount; i++)
        {
            randomCoords = new Vector3Int(Random.Range(0, columnsCount), Random.Range(0, rowsCount), Random.Range(0,depth));

            while (_ingotCells.Contains(randomCoords)) // It`s need because 2 or more ingots shouldn`t be in 1 cell 
            {
                randomCoords = new Vector3Int(Random.Range(0, columnsCount), Random.Range(0, rowsCount), Random.Range(0, depth));
            }                                                               
            _ingotCells.Add( randomCoords ) ;
            Debug.Log(randomCoords);
        }
    }

    private int GetIngotPositionInCell(Vector2 cellCoords,List<Vector3Int> ingotsCoords )
    {
        for(int i = 0; i < ingotsCoords.Count; i++)
        {
            if (ingotsCoords[i].x == cellCoords.x && ingotsCoords[i].y == cellCoords.y) return ingotsCoords[i].z;
        }
        return -1; // If ingot is not in this cell , return -1
    }

    public List<Vector3Int> IngotCells()
    {
        return _ingotCells;   
    }
}
