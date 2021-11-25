using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WitnessManager : MonoBehaviour
{
    [SerializeField] private int _witnessCount = 9;
    [SerializeField] private int _liarsMaximumCount = -1;
    [SerializeField] private TextMeshProUGUI[] _textFields;
    [SerializeField] private TextMeshProUGUI _liarsCountField;
    [SerializeField] private bool _debugMode;
    private int _liarsCount = 0;
    private List<Witness> _witnesses = new List<Witness>();

    public void SpawnWitnesses(List<int> ingotRows , List<int> ingotColumns , List<int> ingotDepths , GameValues gameValues)
    {

        for (int i = 0; i < _witnessCount; i++)
        {
            int randV = Random.Range(0, 2); // if 1 - liar
            _liarsCount += randV; 
            Witness witness = new Witness(ingotRows , ingotColumns , ingotDepths, gameValues , _liarsMaximumCount > _liarsCount ? randV == 1 : false, i+1 , _debugMode);
            _witnesses.Add(witness);
            
            _textFields[i].text = witness.GetSuggestion();

        }
        _liarsCount = _liarsCount > _liarsMaximumCount ? _liarsMaximumCount : _liarsCount;
        _liarsCountField.text = _liarsCount.ToString() + " свидетелей врут";
    }


}

