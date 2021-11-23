using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitnessManager : MonoBehaviour
{
    [SerializeField] private int _witnessCount = 9;
    [SerializeField] private int _liarsMaximumCount = -1;
    private int _liarsCount = 0;



    private List<Witness> _witnesses = new List<Witness>();

    public void SpawnWitnesses(List<Vector3Int> _ingotsCoords)
    {
        for (int i = 0; i < _witnessCount; i++)
        {
            int randV = Random.Range(0, 2); // if 1 - liar
            _liarsCount += randV; 
            Witness witness = new Witness(_ingotsCoords, _liarsMaximumCount > 0 ? randV == 1 : false, i+1);
            _liarsMaximumCount -= randV;
            _witnesses.Add(witness);  
        }
    }

}

