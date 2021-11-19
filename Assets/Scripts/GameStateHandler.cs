using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateHandler : MonoBehaviour
{

    [SerializeField] GameValues _gameValues;
    [SerializeField] TextMeshProUGUI _durabilityText;
    [SerializeField] TextMeshProUGUI _collectedIngotsText;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _scoreSound;

    private Dictionary<TextMeshProUGUI,Animator> _textAnimators;
    private float _toolDurability;
    private int _collectedIngotsCount;

    private void Start()
    {
        _textAnimators = new Dictionary<TextMeshProUGUI, Animator>()
        {
            { _durabilityText , _durabilityText.GetComponent<Animator>() },
            { _collectedIngotsText , _collectedIngotsText.GetComponent<Animator>() }
        };
        _toolDurability = _gameValues.GetToolDurability();
        ChangeText(_durabilityText, _toolDurability);
        ChangeText(_collectedIngotsText, 0);
    }

    public void ReduceToolDurability(float reduceValue)
    {
        _toolDurability -= reduceValue;
        ChangeText(_durabilityText, _toolDurability);
        if(_toolDurability <= 0)
        {
            Loose();
        }
    }


    public void CollectIngot()
    {
        PlaySound(_scoreSound);
        _collectedIngotsCount += 1;
        ChangeText(_collectedIngotsText, _collectedIngotsCount);
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

    public void ChangeText(TextMeshProUGUI textField , string text)
    {
        textField.text = text;
        _textAnimators[textField].SetTrigger("ChangeValue");
    }

    public void ChangeText(TextMeshProUGUI textField , double text)
    {
        textField.text = text.ToString();
        _textAnimators[textField].SetTrigger("ChangeValue");

    }

    public void PlaySound(AudioClip sound)
    {
        _audioSource.clip = sound;
        _audioSource.Play();
    }

}
