using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private JackpotController _jackpotController;
    [SerializeField] private AudioSource _winSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _wheelSpinClip;
    [SerializeField] private AudioClip _appearingClip;
    [SerializeField] private AudioClip _appearingJackpotClip;
    [SerializeField] private RectTransform _winTransform;
    [SerializeField] private RectTransform _miniJackpotOne;
    [SerializeField] private RectTransform _miniJackpotTwo;
    [SerializeField] private RectTransform _miniJackpotThree;
    [SerializeField] private RectTransform _unlockedJackpotOne;
    [SerializeField] private RectTransform _unlockedJackpotTwo;
    [SerializeField] private RectTransform _unlockedJackpotThree;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Image _background;
    [SerializeField] private float _duration;
    [SerializeField] private RectTransform _winText;
    [SerializeField] private int _numberOfRotations = 2;
    [SerializeField] private Animator _backgroundAnimator;

    private void OnEnable()
    {
        _jackpotController._AllJackpotsRecieved += ShowWinScreen;
        _resetButton.onClick.AddListener(ResetScene);
    }

    private void OnDisable()
    {
        _jackpotController._AllJackpotsRecieved -= ShowWinScreen;
        _resetButton.onClick.RemoveListener(ResetScene);
    }

    private float _rotationAmount = 360f;

    private void ShowWinScreen()
    {
        _backgroundAnimator.SetTrigger("MakeRainbow");
        _winSource.clip = _winClip;
        _winSource.Play();
        _winSource.PlayOneShot(_wheelSpinClip);
        _musicSource.DOFade(0f, _duration);
        _winTransform.DORotate(Vector3.back * _rotationAmount * _numberOfRotations, _duration, RotateMode.FastBeyond360).SetEase(Ease.OutSine);
        _winTransform.DOAnchorPos(Vector2.zero, _duration).SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                _winSource.PlayOneShot(_appearingClip);
                _winSource.PlayOneShot(_wheelSpinClip);
                _winText.DOScale(Vector3.one, _duration / 4)
                .OnComplete(() =>
                {
                    _winTransform.DORotate(Vector3.forward * _rotationAmount * _numberOfRotations, _duration / 1.5f, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        _winSource.PlayOneShot(_appearingJackpotClip);
                        _miniJackpotOne.DOScale(Vector3.one, _duration / 10);
                        _unlockedJackpotOne.DOScale(Vector3.zero, _duration / 10)
                        .OnComplete(() =>
                        {
                            _winSource.PlayOneShot(_appearingJackpotClip);
                            _miniJackpotTwo.DOScale(Vector3.one, _duration / 10);
                            _unlockedJackpotTwo.DOScale(Vector3.zero, _duration / 10)
                            .OnComplete(() =>
                            {
                                _winSource.PlayOneShot(_appearingJackpotClip);
                                _miniJackpotThree.DOScale(Vector3.one, _duration / 10);
                                _unlockedJackpotThree.DOScale(Vector3.zero, _duration / 10)
                                .OnComplete(() =>
                                {
                                    _winSource.PlayOneShot(_appearingClip);
                                    _resetButton.transform.DOScale(Vector3.one, _duration / 5);
                                });
                            });
                        });
                    });
                });
            });
    }

    private void ResetScene()
    {
        SceneManager.LoadScene("Maks");
    }
}
