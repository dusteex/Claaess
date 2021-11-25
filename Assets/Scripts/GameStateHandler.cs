using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameStateHandler : MonoBehaviour
{

    static public bool PlayerCanDig = true;

    [SerializeField] GameValues _gameValues;
    [SerializeField] TextMeshProUGUI _durabilityText;
    [SerializeField] TextMeshProUGUI _collectedIngotsText;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _scoreSound;

    //END GAME WINDOW OBJECTS
    [Header("End Game Window Components")]
    [SerializeField] private Animator _gameEndWindowAnimator;
    [SerializeField] private TextMeshProUGUI _gameEndWindowTextField;
    [SerializeField] private TextMeshProUGUI _gameEndWindowButtonTextField;
    [SerializeField] private Image _gameEndWindowButtonBackground;
    private bool isOpen = false;

    //WIN VARIABLES
    [Header("Win Window Variables")]
    [SerializeField] private string _winText;
    [SerializeField] private string _winButtonText;
    [SerializeField] private Sprite _winButtonSprite;

    [Header("Loose Window Variables")]
    [SerializeField] private string _looseText;
    [SerializeField] private string _looseButtonText;
    [SerializeField] private Sprite _looseButtonSprite;


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
        OpenGameEndWindow( _looseText , _looseButtonText , _looseButtonSprite );
    }
    private void Win()
    {
        OpenGameEndWindow(_winText, _winButtonText, _winButtonSprite);
    }

    private void OpenGameEndWindow( string windowText , string buttonText , Sprite buttonBackgroundSprite )
    {
        PlayerCanDig = false;
        _gameEndWindowTextField.text = windowText;
        _gameEndWindowButtonTextField.text = buttonText;
        _gameEndWindowButtonBackground.sprite = buttonBackgroundSprite;
        _gameEndWindowAnimator.SetBool("isOpen", true);
    }

    public void CloseGameEndWindow()
    {
        PlayerCanDig = true;
        _gameEndWindowAnimator.SetBool("isOpen", false); // AFTER THIS SCENE WILL RELOAD
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
