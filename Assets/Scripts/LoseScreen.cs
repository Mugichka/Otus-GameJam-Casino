using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MoneyCounter _moneyCounter;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _loseSource;
    [SerializeField] private AudioClip _appearanceClip;
    [SerializeField] private AudioClip _lossClip;
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _loseTransform;
    [SerializeField] private RectTransform _homeTransform;
    [SerializeField] private RectTransform _retryTransform;
    [SerializeField] private RectTransform _cashBack;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private TextMeshProUGUI _textCashback;
    [SerializeField] private float _duration;

    private bool _isLose = false;

    private void OnEnable()
    {
        //_player.PlayerDead += ShowLoseScreen;
        _resetButton.onClick.AddListener(ReloadScene);
        _homeButton.onClick.AddListener(LoadHome);
    }

    private void OnDisable()
    {
        //_player.PlayerDead -= ShowLoseScreen;
        _resetButton.onClick.RemoveListener(ReloadScene);
        _homeButton.onClick.RemoveListener(LoadHome);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("Maks");
    }

    private void LoadHome()
    {
        SceneManager.LoadScene("Marsel");
    }

    public void ShowLoseScreen()
    {
        if (_isLose == true)
        {
            return;
        }

        _isLose = true;
        _textCashback.text = $"{_moneyCounter._totalMoney}";
        _loseSource.PlayOneShot(_lossClip);
        _musicSource.DOFade(0f, _duration);
        _background.DOFade(0.5f, _duration);
        _loseTransform.DOAnchorPos(Vector2.zero, _duration).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                _loseSource.PlayOneShot(_appearanceClip);
                _cashBack.DOScale(Vector3.one, _duration / 2)
                .OnComplete(() =>
                {
                    _loseSource.PlayOneShot(_appearanceClip);
                    _homeTransform.DOScale(Vector3.one, _duration / 2);
                    _retryTransform.DOScale(Vector3.one, _duration / 2);
                });
            });
    }
}
