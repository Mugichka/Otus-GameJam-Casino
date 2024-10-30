using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class JackpotController : MonoBehaviour
{
    [SerializeField] private RectTransform _jackpotTransform;
    [SerializeField] private FortuneWheel _fortuneWheel;
    [SerializeField] private Image[] _blockedJackpots;
    [SerializeField] private float _duration;
    [SerializeField] private float _rotationAmount = 360f;
    [SerializeField] private int _numberOfRotations = 2;
    [SerializeField] private Sprite _jackpotSprite;
    [SerializeField] private AudioSource _jackpotSource;
    [SerializeField] private AudioClip _jackpotClip;

    private int _jackpotCount = 0;
    private Vector2 _targetScaleJackpot = new Vector2(0.22f, 0.22f);

    private void OnEnable()
    {
        _fortuneWheel.JackpotFell += CheckWin;
    }

    private void OnDisable()
    {
        _fortuneWheel.JackpotFell -= CheckWin;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckWin();
        }
    }

    private void CheckWin()
    {
        Vector3 pivotWorldPosition = _blockedJackpots[_jackpotCount].rectTransform.TransformPoint(_blockedJackpots[_jackpotCount].rectTransform.pivot);
        Vector2 targetLocalPosition = _jackpotTransform.parent.InverseTransformPoint(pivotWorldPosition);

        _jackpotSource.PlayOneShot(_jackpotClip);
        _jackpotTransform.DOScale(Vector3.one, _duration).SetEase(Ease.OutBack);
        _jackpotTransform.DORotate(Vector3.forward * _rotationAmount * _numberOfRotations, _duration, RotateMode.FastBeyond360)
              .OnComplete(() =>
              {
                  _jackpotTransform.DOAnchorPos(targetLocalPosition, _duration).SetEase(Ease.OutQuad);
                  _jackpotTransform.DOScale(_targetScaleJackpot, _duration).SetEase(Ease.OutBack);
                  _jackpotTransform.DORotate(Vector3.back * _rotationAmount * _numberOfRotations, _duration, RotateMode.FastBeyond360)
                        .OnComplete(() =>
                        {
                            _blockedJackpots[_jackpotCount].sprite = _jackpotSprite;
                            _jackpotTransform.anchoredPosition = Vector2.zero;
                            _jackpotTransform.localScale = Vector2.zero;
                            _jackpotCount++;

                            if (_jackpotCount >= 3)
                            {
                                Debug.Log("Показать победное окно");
                            }
                        });
              });
    }
}
