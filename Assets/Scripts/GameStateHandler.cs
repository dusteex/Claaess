using UnityEngine;
using TMPro;

public class GameStateHandler : MonoBehaviour
{

    [SerializeField] GameValues _gameValues;
    [SerializeField] TextMeshProUGUI _durabilityText;
    [SerializeField] TextMeshProUGUI _collectedIngotsText;

    private float _toolDurability;
    private int _collectedIngotsCount;

    private void Start()
    {
        _toolDurability = _gameValues.GetToolDurability();
        _durabilityText.text = _toolDurability.ToString();
        _collectedIngotsText.text = "0";
    }

    public void ReduceToolDurability(float reduceValue)
    {
        _toolDurability -= reduceValue;
        _durabilityText.text = _toolDurability.ToString();
        if(_toolDurability <= 0)
        {
            Loose();
        }
    }

    public void CollectIngot()
    {
        _collectedIngotsCount += 1;
        _collectedIngotsText.text = _collectedIngotsCount.ToString();

        if (_collectedIngotsCount >= _gameValues.GetIngotsCount())
        {
            Win();
        }
    }

    private void Loose()
    {
        Debug.Log("You loose");
    }
    private void Win()
    {
        Debug.Log("You Win");
        
    }
}
