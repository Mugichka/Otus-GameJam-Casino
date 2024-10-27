using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
//using DG.Tweening;

public sealed class CardsController : MonoBehaviour
{
    [Header("LeftCard")]
    [SerializeField] private CanvasGroup _groupFrontLeftCard;
    [SerializeField] private CanvasGroup _groupBackLeftCard;
    [SerializeField] private RectTransform _transformLeftCard;
    [SerializeField] private Button _buttonLeftCard;
    [SerializeField] private TextMeshProUGUI _nameLeftCard;
    [SerializeField] private TextMeshProUGUI _descriptionLeftCard;
    [Header("MiddleCard")]
    [SerializeField] private CanvasGroup _groupFrontMiddleCard;
    [SerializeField] private CanvasGroup _groupBackMiddleCard;
    [SerializeField] private RectTransform _transformMiddleCard;
    [SerializeField] private Button _buttonMiddleCard;
    [SerializeField] private TextMeshProUGUI _nameMiddleCard;
    [SerializeField] private TextMeshProUGUI _descriptionMiddleCard;
    [Header("RightCard")]
    [SerializeField] private CanvasGroup _groupFrontRightCard;
    [SerializeField] private CanvasGroup _groupBackRightCard;
    [SerializeField] private RectTransform _transformRightCard;
    [SerializeField] private Button _buttonRightCard;
    [SerializeField] private TextMeshProUGUI _nameRightCard;
    [SerializeField] private TextMeshProUGUI _descriptionRightCard;
    [Space]
    [SerializeField] private float _flipDuration;
    [SerializeField] private float _throwDuration;
    [SerializeField] private GameObject _cardsContainer;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _fromShowToFlipDuration;
    [SerializeField] private float _fromFlipToFlipDuration;
    [SerializeField] private float _closeCardsDuration;
    [SerializeField] private FortuneWheel _fortuneWheel;
    [SerializeField] private UpgradeData[] _upgradeData;

    private Card[] _cards;
    private int _countOfCards = 3;

    private void Awake()
    {
        _cards = new Card[_countOfCards];

        _cards[0] = new Card(
            new CardFliper(_groupFrontLeftCard, _groupBackLeftCard, _flipDuration, "FlipTweenLeft"),
            new CardThrower(_transformLeftCard, _throwDuration, "ThrowTweenLeft"),
            new CardDescriptionChanger(_nameLeftCard, _descriptionLeftCard, _player)
            );

        _cards[1] = new Card(
            new CardFliper(_groupFrontMiddleCard, _groupBackMiddleCard, _flipDuration, "FlipTweenMiddle"),
            new CardThrower(_transformMiddleCard, _throwDuration, "ThrowTweenMiddle"),
            new CardDescriptionChanger(_nameMiddleCard, _descriptionMiddleCard, _player)
            );

        _cards[2] = new Card(
            new CardFliper(_groupFrontRightCard, _groupBackRightCard, _flipDuration, "FlipTweenRight"),
            new CardThrower(_transformRightCard, _throwDuration, "ThrowTweenRight"),
            new CardDescriptionChanger(_nameRightCard, _descriptionRightCard, _player)
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
        _cardsContainer.SetActive(true);

        for (int i = 0; i < _countOfCards; i++)
        {
            _cards[i].UpdateData(_upgradeData[i]);
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

    void Run()
    {
        StartCoroutine(ShowCards());
    }

    private void OnLeftCardButtonClick()
    {
        StartCoroutine(CloseCards());
        _upgradeData[0].ApplyUpgrade(_player);
    }

    private void OnMiddleCardButtonClick()
    {
        StartCoroutine(CloseCards());
        _upgradeData[1].ApplyUpgrade(_player);
    }

    private void OnRightCardButtonClick()
    {
        StartCoroutine(CloseCards());
        _upgradeData[2].ApplyUpgrade(_player);
    }
}
