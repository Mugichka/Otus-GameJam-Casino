using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public sealed class CardsController : MonoBehaviour
{
    [Header("LeftCard")]
    [SerializeField] private CanvasGroup _groupFrontLeftCard;
    [SerializeField] private CanvasGroup _groupBackLeftCard;
    [SerializeField] private RectTransform _transformLeftCard;
    [SerializeField] private Button _buttonLeftCard;
    [Header("MiddleCard")]
    [SerializeField] private CanvasGroup _groupFrontMiddleCard;
    [SerializeField] private CanvasGroup _groupBackMiddleCard;
    [SerializeField] private RectTransform _transformMiddleCard;
    [SerializeField] private Button _buttonMiddleCard;
    [Header("RightCard")]
    [SerializeField] private CanvasGroup _groupFrontRightCard;
    [SerializeField] private CanvasGroup _groupBackRightCard;
    [SerializeField] private RectTransform _transformRightCard;
    [SerializeField] private Button _buttonRightCard;
    [Space]
    [SerializeField] private float _flipDuration;
    [SerializeField] private float _throwDuration;
    [SerializeField] private GameObject _cardsContainer;
    [SerializeField] private float _fromShowToFlipDuration;
    [SerializeField] private float _fromFlipToFlipDuration;
    [SerializeField] private float _closeCardsDuration;
    [SerializeField] private FortuneWheel _fortuneWheel;

    private Card[] _cards;
    private int _countOfCards = 3;

    private void Awake()
    {
        _cards = new Card[_countOfCards];

        _cards[0] = new Card(
            new CardFliper(_groupFrontLeftCard, _groupBackLeftCard, _flipDuration, "FlipTweenLeft"),
            new CardThrower(_transformLeftCard, _throwDuration, "ThrowTweenLeft")
            );

        _cards[1] = new Card(
            new CardFliper(_groupFrontMiddleCard, _groupBackMiddleCard, _flipDuration, "FlipTweenMiddle"),
            new CardThrower(_transformMiddleCard, _throwDuration, "ThrowTweenMiddle")
            );

        _cards[2] = new Card(
            new CardFliper(_groupFrontRightCard, _groupBackRightCard, _flipDuration, "FlipTweenRight"),
            new CardThrower(_transformRightCard, _throwDuration, "ThrowTweenRight")
            );
    }

    private void OnEnable()
    {
        _fortuneWheel.CardsFell += OnWheelFellOut;
        _buttonLeftCard.onClick.AddListener(OnCardButtonClick);
        _buttonMiddleCard.onClick.AddListener(OnCardButtonClick);
        _buttonRightCard.onClick.AddListener(OnCardButtonClick);
    }

    private void OnDisable()
    {
        _fortuneWheel.CardsFell -= OnWheelFellOut;
        _buttonLeftCard.onClick.RemoveListener(OnCardButtonClick);
        _buttonMiddleCard.onClick.RemoveListener(OnCardButtonClick);
        _buttonRightCard.onClick.RemoveListener(OnCardButtonClick);
    }

    private IEnumerator ShowCards()
    {
        _cardsContainer.SetActive(true);

        for (int i = 0; i < _countOfCards; i++)
        {
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
    }

    void OnWheelFellOut()
    {
        StartCoroutine(ShowCards());
    }

    private void OnCardButtonClick()
    {
        StartCoroutine(CloseCards());
    }
}
