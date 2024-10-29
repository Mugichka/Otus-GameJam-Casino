using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _loseSource;
    [SerializeField] private AudioClip _appearanceClip;
    [SerializeField] private AudioClip _lossClip;
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _loseTransform;
    [SerializeField] private RectTransform _homeTransform;
    [SerializeField] private RectTransform _retryTransform;
    [SerializeField] private RectTransform _cashBack;
    [SerializeField] private float _duration;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowLoseScreen();
        }
    }

    private void ShowLoseScreen()
    {
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
