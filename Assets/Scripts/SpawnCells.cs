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

    private List<int> _ingotRows = new List<int>();
    private List<int> _ingotColumns = new List<int>();
    private List<int> _ingotDepths = new List<int>();
    private List<Vector2Int> _ingotCells = new List<Vector2Int>();



    private Vector3 _anchorPosition;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        CalculateIngotCellsCoordinates(_gameValues.GetRowsCount() , _gameValues.GetColumnsCount(), _gameValues.GetMaxDepth() , _gameValues.GetIngotsCount());
        _witnessManager.SpawnWitnesses(_ingotRows , _ingotColumns , _ingotDepths , _gameValues);
        _anchorPosition = _anchorTransform.position;
        for(int i = 0; i < _gameValues.GetRowsCount(); i++)
        {
            for(int j = 0; j < _gameValues.GetColumnsCount(); j++)
            {
                Vector3 cellPosition = _anchorPosition + new Vector3(j * _gameValues.GetOffsetX(), i * _gameValues.GetOffsetY(), 0);
                GameObject currentCell = Instantiate(_cell, cellPosition, Quaternion.identity);
                SetCorrectOrder(currentCell, i);
                currentCell.transform.SetParent(_fieldTransform);
                currentCell.GetComponent<Cell>().Init(_gameValues.GetMaxDepth(), _gameValues.GetCellTypes(),_gameStateHandler , GetIngotPositionInCell(new Vector2Int(j+1,i+1), _ingotCells) ) ;  
            }
        }
    }

    private void SetCorrectOrder(GameObject cell , int sortingOrderLevel)
    {
        cell.GetComponent<SpriteRenderer>().sortingOrder = sortingOrderLevel;
    }

    private void CalculateIngotCellsCoordinates(int rowsCount , int columnsCount , int depth ,  int ingotsCount )
    {
        Vector2Int randomXY;
        int randomDepth;
        for (int i = 0; i < ingotsCount; i++)
        {
            randomXY = new Vector2Int(Random.Range(1, columnsCount+1), Random.Range(1, rowsCount+1));
            randomDepth = Random.Range(1, depth+1);

            while (_ingotCells.Contains(randomXY)) 
                randomXY = new Vector2Int(Random.Range(1, columnsCount+1), Random.Range(1, rowsCount+1));
            
            _ingotColumns.Add(randomXY.x);
            _ingotRows.Add(randomXY.y);
            _ingotDepths.Add(randomDepth);

            _ingotCells.Add(randomXY);
        }
    }

    private int GetIngotPositionInCell(Vector2Int cellCoords,List<Vector2Int> ingotsCoords )
    {

        for (int i = 0; i < ingotsCoords.Count; i++)
        {
            if (cellCoords.x == ingotsCoords[i].x && cellCoords.y == ingotsCoords[i].y)
            {

                return _ingotDepths[i];
            }
            }
        return -1; // If ingot is not in this cell , return -1
    }


}
    