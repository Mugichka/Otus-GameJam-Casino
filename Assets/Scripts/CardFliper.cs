using UnityEngine;
using DG.Tweening;

public sealed class CardFliper : ICardAction
{
    private CanvasGroup _front;
    private CanvasGroup _back;
    private float _flipDuration;
    private string _flipTweenId;

    private bool isFlipped = false;

    public CardFliper(CanvasGroup front, CanvasGroup back, float flipDuration, string cardID)
    {
        _front = front;
        _back = back;
        _flipDuration = flipDuration;
        _flipTweenId = cardID;
        front.gameObject.SetActive(false);
    }

    public void Execute()
    {
        FlipCard();
    }

    private void FlipCard()
    {
        DOTween.Kill(_flipTweenId);

        if (isFlipped)
        {
            Flip(_front, _back);
        }
        else
        {
            Flip(_back, _front);
        }

        isFlipped = !isFlipped;
    }

    private void Flip(CanvasGroup from, CanvasGroup to)
    {
        to.gameObject.SetActive(true);

        from.transform.DORotate(new Vector3(0, 180, 0), _flipDuration / 2).SetId(_flipTweenId)
            .OnComplete(() =>
            {
                from.gameObject.SetActive(false);

                to.transform.DORotate(new Vector3(0, 0, 0), _flipDuration / 2).SetId(_flipTweenId)
                    .OnStart(() =>
                    {
                        to.GetComponent<CanvasGroup>().DOFade(1, _flipDuration / 2).SetId(_flipTweenId);
                    });
            });

        from.DOFade(0, _flipDuration / 2).SetId(_flipTweenId);
    }
}
