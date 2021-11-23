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



    public Witness(List<Vector3Int> ingotsCoords , bool isLiar , int mode)
    {
        _ingotsCoords = ingotsCoords;
        _ingotsCount = ingotsCoords.Count;
        _isLiar = isLiar;
        _mode = mode;

        _rows = new List<int>() { 0, 1, 2, 3, 4 };
        _columns = new List<int>() { 0, 1, 2, 3, 4 };
        _depths = new List<int>() { 0, 1, 2};


        _ingotRows = GetListFromVectorsCoord(_ingotsCoords , 1);
        _ingotColumns = GetListFromVectorsCoord(_ingotsCoords, 0);
        _ingotDepths = GetListFromVectorsCoord(_ingotsCoords, 2);


        _suggestion = CreateSuggestion();
        Debug.Log(_suggestion);

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
                return "Больше всего слитков в строках: " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА") ;
            // Which column has the biggest count of ingots
            case 2:
                suggestion = GetAreaWithMaxIngots(_ingotColumns, _ingotsCount ,_isLiar); 
                return "Больше всего слитков в столбцах: " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which depth has the biggest count of ingots
            case 3:
                suggestion = GetAreaWithMaxIngots(_ingotDepths, _ingotsCount ,  _isLiar);
                return "Больше всего слитков в глубинах: " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which row has the biggest count of ingots
            case 4: 
                suggestion = GetAreaWithoutIngots(_rows , _ingotRows , _isLiar);
                if (suggestion == "") return "Золото есть во всех строках" + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
                return "Слитков нет в строках:  " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which column has the biggest count of ingots
            case 5: 
                suggestion = GetAreaWithoutIngots(_columns, _ingotColumns, _isLiar);
                if (suggestion == "") return "Золото есть во всех строках" + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
                return "Слитков нет в столбцах:  " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which depth has the biggest count of ingots
            case 6:
                suggestion = GetAreaWithoutIngots(_depths, _ingotDepths, _isLiar);
                if (suggestion == "") return "Золото есть во всех строках" + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
                return "Слитков нет в глубинах:  " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which row has the biggest count of ingots
            case 7:
                randomArea = Random.Range(0,_rows.Count());
                suggestion = GetIngotsCountInArea(_rows , randomArea , _ingotsCount , _isLiar).ToString();
                return "Слитков в " + (randomArea).ToString() + " строке : " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which column has the biggest count of ingots
            case 8: 
                randomArea = Random.Range(0, _columns.Count());
                suggestion = GetIngotsCountInArea(_columns, randomArea, _ingotsCount, _isLiar).ToString();
                return "Слитков в " + (randomArea).ToString() + " столбце : " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
            // Which depth has the biggest count of ingots
            case 9: 
                randomArea = Random.Range(0, _rows.Count());
                suggestion = GetIngotsCountInArea(_depths, randomArea, _ingotsCount, _isLiar).ToString();
                return "Слитков на " + (randomArea).ToString() + " глубине " + suggestion + (_isLiar ? " ЛОЖЬ" : " ПРАВДА");
        }
        return "Ошибка";
    }

    /* Creates a rows list , columns list or depths list
    For example , GetListFromVectorsCoord( [(1, 0, 0),(2, 0, 0),(3, 0, 0)] , 0)  return [1,2,3] */
    private List<int> GetListFromVectorsCoord(List<Vector3Int> list , int coordIndex) 
    {
        List<int> resultList = new List<int>();
        foreach(Vector3Int i in list)
        {
            resultList.Add(i[coordIndex]);
        }
        return resultList; 
    }

    // For modes 1 , 2 and 3 , Finds the row , column or depth with biggest count of ingots
    
    private string GetAreaWithMaxIngots (List<int> ingotsArea , int ingotsCount , bool isLiar)
    {
        if (isLiar == false) return string.Join(",", GetMaxElementsInList(ingotsArea));
        else return string.Join("," , GetRandomValuesFromList(ingotsArea,ingotsCount/2));
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
        else return string.Join("," , GetRandomValuesFromList(ingotsArea , ingotsArea.Count));
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
