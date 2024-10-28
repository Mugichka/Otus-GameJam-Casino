using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public sealed class CardsController : MonoBehaviour
{
    //public event Action CardSelected;

    [Header("LeftCard")]
    [SerializeField] private CanvasGroup _groupFrontLeftCard;
    [SerializeField] private CanvasGroup _groupBackLeftCard;
    [SerializeField] private RectTransform _transformLeftCard;
    [SerializeField] private Button _buttonLeftCard;
    [SerializeField] private TextMeshProUGUI _nameLeftCard;
    [SerializeField] private TextMeshProUGUI _descriptionLeftCard;
    [SerializeField] private Image _imageLeftCard;
    [Header("MiddleCard")]
    [SerializeField] private CanvasGroup _groupFrontMiddleCard;
    [SerializeField] private CanvasGroup _groupBackMiddleCard;
    [SerializeField] private RectTransform _transformMiddleCard;
    [SerializeField] private Button _buttonMiddleCard;
    [SerializeField] private TextMeshProUGUI _nameMiddleCard;
    [SerializeField] private TextMeshProUGUI _descriptionMiddleCard;
    [SerializeField] private Image _imageMiddleCard;
    [Header("RightCard")]
    [SerializeField] private CanvasGroup _groupFrontRightCard;
    [SerializeField] private CanvasGroup _groupBackRightCard;
    [SerializeField] private RectTransform _transformRightCard;
    [SerializeField] private Button _buttonRightCard;
    [SerializeField] private TextMeshProUGUI _nameRightCard;
    [SerializeField] private TextMeshProUGUI _descriptionRightCard;
    [SerializeField] private Image _imageRightCard;
    [Space]
    [SerializeField] private float _flipDuration;
    [SerializeField] private float _throwDuration;
    [SerializeField] private GameObject _cardsContainer;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _fromShowToFlipDuration;
    [SerializeField] private float _fromFlipToFlipDuration;
    [SerializeField] private float _closeCardsDuration;
    [SerializeField] private FortuneWheel _fortuneWheel;
    [SerializeField] private CardSelector _cardSelector;

    private List<UpgradeData> _selectedCards;
    private Card[] _cards;
    private readonly int _countOfCards = 3;

    private void Awake()
    {
        _cards = new Card[_countOfCards];

        _cards[0] = new Card(
            new CardFliper(_groupFrontLeftCard, _groupBackLeftCard, _flipDuration, "FlipTweenLeft"),
            new CardThrower(_transformLeftCard, _throwDuration, "ThrowTweenLeft"),
            new CardReturner(_transformLeftCard),
            new CardDescriptionChanger(_nameLeftCard, _descriptionLeftCard, _imageLeftCard, _player)
            );

        _cards[1] = new Card(
            new CardFliper(_groupFrontMiddleCard, _groupBackMiddleCard, _flipDuration, "FlipTweenMiddle"),
            new CardThrower(_transformMiddleCard, _throwDuration, "ThrowTweenMiddle"),
            new CardReturner(_transformMiddleCard),
            new CardDescriptionChanger(_nameMiddleCard, _descriptionMiddleCard, _imageMiddleCard, _player)
            );

        _cards[2] = new Card(
            new CardFliper(_groupFrontRightCard, _groupBackRightCard, _flipDuration, "FlipTweenRight"),
            new CardThrower(_transformRightCard, _throwDuration, "ThrowTweenRight"),
            new CardReturner(_transformRightCard),
            new CardDescriptionChanger(_nameRightCard, _descriptionRightCard, _imageRightCard, _player)
            );
    }

    private void OnEnable()
    {
        _fortuneWheel.CardsFell += Run;
        _buttonLeftCard.onClick.AddListener(OnLeftCardButtonClick);
        _buttonMiddleCard.onClick.AddListener(OnMiddleCardButtonClick);
        _buttonRightCard.onClick.AddListener(OnRightCardButtonClick);
    }

    private void OnDisable()
    {
        _fortuneWheel.CardsFell -= Run;
        _buttonLeftCard.onClick.RemoveListener(OnLeftCardButtonClick);
        _buttonMiddleCard.onClick.RemoveListener(OnMiddleCardButtonClick);
        _buttonRightCard.onClick.RemoveListener(OnRightCardButtonClick);
    }

    private IEnumerator ShowCards()
    {
        _selectedCards = _cardSelector.GetRandomUpgrades();

        _cardsContainer.SetActive(true);

        for (int i = 0; i < _selectedCards.Count; i++)
        {
            if (_selectedCards[i] != null)
            {
                _cards[i].UpdateData(_selectedCards[i]);
            }

            _cards[i].Throw();
        }

        yield return new WaitForSeconds(_fromShowToFlipDuration);

        for (int i = 0; i < _countOfCards; i++)
        {
            _cards[i].Flip();
            yield return new WaitForSeconds(_fromFlipToFlipDuration);
        }
    }

    private IEnumerator CloseCards()
    {
        for (int i = 0; i < _countOfCards; i++)
        {
            _cards[i].Flip();
        }

        yield return new WaitForSeconds(_closeCardsDuration);

        _cardsContainer.SetActive(false);

        for (int i = 0; i < _countOfCards; i++)
        {
            _cards[i].Return();
        }
    }

    void Run()
    {
        StartCoroutine(ShowCards());
    }

    private void OnLeftCardButtonClick()
    {
        StartCoroutine(CloseCards());
        _selectedCards[0].ApplyUpgrade(_player);
        _cardSelector.UpgradeSelected(_selectedCards[0]);
        //CardSelected?.Invoke();
    }

    private void OnMiddleCardButtonClick()
    {
        StartCoroutine(CloseCards());
        _selectedCards[1].ApplyUpgrade(_player);
        _cardSelector.UpgradeSelected(_selectedCards[1]);
        //CardSelected?.Invoke();
    }

    private void OnRightCardButtonClick()
    {
        StartCoroutine(CloseCards());
        _selectedCards[2].ApplyUpgrade(_player);
        _cardSelector.UpgradeSelected(_selectedCards[2]);
        //CardSelected?.Invoke();
    }
}
