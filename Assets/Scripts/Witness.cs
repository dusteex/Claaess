using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Witness 
{
    private List<Vector3Int> _ingotsCoords;
    private bool _isLiar;
    private int _mode;
    private string _suggestion;
    private int _ingotsCount;
    private List<int> _ingotRows;
    private List<int> _ingotColumns;
    private List<int> _ingotDepths;
    private List<int> _rows;
    private List<int> _columns;
    private List<int> _depths;



    public Witness(List<int> ingotRows, List<int> ingotColumns, List<int> ingotDepths, GameValues gameValues,  bool isLiar , int mode , bool debugMode = false)
    {
        _ingotsCount = ingotRows.Count;
        _isLiar = isLiar;
        _mode = mode;

        _rows = GetListFromStartToEnd(1,gameValues.GetRowsCount());
        _columns = GetListFromStartToEnd(1, gameValues.GetColumnsCount());
        _depths = GetListFromStartToEnd(1, gameValues.GetMaxDepth());

        _ingotColumns = new List<int>(ingotColumns);
        _ingotRows = new List<int>(ingotRows);
        _ingotDepths = new List<int>(ingotDepths);

        if (_mode == 1)
        {
            Debug.Log("IngotRows : " + string.Join(" ", _ingotRows));
            Debug.Log("IngotColumns : " + string.Join(" ", _ingotColumns));
            Debug.Log("IngotDepths : " + string.Join(" ", _ingotDepths));
        }

        _suggestion = CreateSuggestion() + (debugMode ? (_isLiar ? " ЛОЖЬ" : " ПРАВДА") : "" );

    }

    private List<int> GetListFromStartToEnd( int start , int end )
    {
        List<int> resultList = new List<int>();
        for (int i = start; i <= end; i++)
        {
            resultList.Add(i);
        }
        return resultList;
    }

    public string GetSuggestion()
    {
        return _suggestion;
    }

    private string CreateSuggestion( )
    {
        int randomArea;
        string suggestion;
        switch (_mode)
        {
            // Which row has the biggest count of ingots
            case 1:
                suggestion = GetAreaWithMaxIngots(_ingotRows, _ingotsCount, _isLiar);
                if (suggestion == "EQUAL") return "Во всех строках одинаковое количество слитков" ;
                return "Больше всего слитков в строках: " + suggestion  ;
            // Which column has the biggest count of ingots
            case 2:
                suggestion = GetAreaWithMaxIngots(_ingotColumns, _ingotsCount ,_isLiar);
                if (suggestion == "EQUAL") return "Во всех столбцах одинаковое количество слитков" ;
                return "Больше всего слитков в столбцах: " + suggestion ;
            // Which depth has the biggest count of ingots
            case 3:
                suggestion = GetAreaWithMaxIngots(_ingotDepths, _ingotsCount ,  _isLiar);
                if (suggestion == "EQUAL") return "На всех глубинах одинаковое количество слитков" ;
                return "Больше всего слитков в глубинах: " + suggestion;
            // Which row has the biggest count of ingots
            case 4: 
                suggestion = GetAreaWithoutIngots(_rows , _ingotRows , _isLiar);
                if (suggestion == "") return "Золото есть во всех строках" ;
                return "Слитков нет в строках:  " + suggestion ;
            // Which column has the biggest count of ingots
            case 5: 
                suggestion = GetAreaWithoutIngots(_columns, _ingotColumns, _isLiar);
                if (suggestion == "") return "Золото есть во всех cтолбцах" ;
                return "Слитков нет в столбцах:  " + suggestion ;
            // Which depth has the biggest count of ingots
            case 6:
                suggestion = GetAreaWithoutIngots(_depths, _ingotDepths, _isLiar);
                if (suggestion == "") return "Золото есть во всех глубинах" ;
                return "Слитков нет в глубинах:  " + suggestion ;
            // Which row has the biggest count of ingots
            case 7:
                randomArea = Random.Range(0,_rows.Count());
                suggestion = GetIngotsCountInArea(_rows , randomArea , _ingotsCount , _isLiar).ToString();
                return "Слитков в " + (randomArea).ToString() + " строке : " + suggestion ;
            // Which column has the biggest count of ingots
            case 8: 
                randomArea = Random.Range(0, _columns.Count());
                suggestion = GetIngotsCountInArea(_columns, randomArea, _ingotsCount, _isLiar).ToString();
                return "Слитков в " + (randomArea).ToString() + " столбце : " + suggestion ;
            // Which depth has the biggest count of ingots
            case 9: 
                randomArea = Random.Range(0, _depths.Count());
                suggestion = GetIngotsCountInArea(_depths, randomArea, _ingotsCount, _isLiar).ToString();
                return "Слитков на " + (randomArea).ToString() + " глубине : " + suggestion ;
        }
        return "Ошибка";
    }


    // For modes 1 , 2 and 3 , Finds the row , column or depth with biggest count of ingots
    
    private string GetAreaWithMaxIngots (List<int> ingotsArea , int ingotsCount , bool isLiar)
    {
        if (isLiar == false)
        {
            if (GetMaxElementsInList(ingotsArea).Count == ingotsArea.Count) return "EQUAL";
            return string.Join(",", GetMaxElementsInList(ingotsArea));
        }
        else return string.Join(",", GetRandomValuesFromList(ingotsArea, ingotsCount / 2));
    }

    private List<int> GetMaxElementsInList( List<int> list ) 
    {
        int maxCount = 0;
        int count = 0;
        List<int> maximum = new List<int>();
        for (int i = 0; i < list.Count(); i++)
        {
            for (int j = i; j < list.Count(); j++)
            {
                if (list[i] == list[j]) count++;
            }
            if( count == maxCount )
            {
                maximum.Add(list[i]);
            }
            else if ( count > maxCount)
            {
                maxCount = count;
                maximum = new List<int> { list[i] };
            }
            count = 0;
                   
        }
        return maximum;
    }
    // FOR LIE
    private List<int> GetRandomValuesFromList(List<int> originList , int maxLength)
    {
        List<int> resultList = new List<int>();
        int randomIndex = Random.Range(0, originList.Count);
        resultList.Add(originList[randomIndex]);
        originList.RemoveAt(randomIndex);

        int resultListLength = Random.Range(1, maxLength + 1);

        for (int i = 0; i < resultListLength - 1; i++)
        {
            randomIndex = Random.Range(0, originList.Count);
            // Ascending Sort
            if (originList[randomIndex] >= resultList[0])
            {
                for (int j = resultList.Count - 1; j > -1; j--)
                {
                    if (resultList[j] < originList[randomIndex])
                    {
                        resultList.Insert(j + 1, originList[randomIndex]);
                        break;
                    }
                }
            }
            else resultList.Insert(0, originList[randomIndex]);
            originList.RemoveAt(randomIndex);

        }
        return resultList;
    }

    //For modes 4 , 5 and 6 . Find rows , columns or depths ,  that have no ingot 

    private string GetAreaWithoutIngots(List<int> areaList , List<int> ingotsArea, bool isLiar)
    {
        if (isLiar == false) return string.Join(",", GetListDiscrepancies(areaList, ingotsArea));
        else return string.Join("," , GetRandomValuesFromList(areaList , areaList.Count-1));
    }

    private List<int> GetListDiscrepancies(List<int> parentList , List<int> childList )
    {
        return parentList.Where(x => childList.Contains(x) == false ).ToList();
    }

    // For modes 7 , 8 and 9 . Find count of ingots in row , column or depth
    
    private int GetIngotsCountInArea(List<int> areaList , int areaIndex , int ingotsCount , bool isLiar)
    {
        int trueAnswer = GetCountOfItemInList(areaList, areaIndex);
        if (isLiar == false) return trueAnswer;
        else
        {
            int falseAnswer = trueAnswer;
            while( falseAnswer == trueAnswer )
            {
                falseAnswer = Random.Range(0, ingotsCount);
            }
            return falseAnswer;
        } 
    }
    private int GetCountOfItemInList(List<int> list , int i)
    {
        int c = 0;
        foreach (var item in list)
        {
            if (i == item) c++;
        }
        return c;
    }

}
