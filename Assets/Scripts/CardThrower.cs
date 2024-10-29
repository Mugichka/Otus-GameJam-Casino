using UnityEngine;
using DG.Tweening;

public sealed class CardThrower : ICardAction
{
    private RectTransform _cardTransform;
    private float _throwDuration;
    private Vector2 _targetPosition;
    private string _throwTweenId;

    public CardThrower(RectTransform cardTransform, float throwDuration, string cardID)
    {
        _cardTransform = cardTransform;
        _throwDuration = throwDuration;
        _throwTweenId = cardID;
        _targetPosition = new Vector2(_cardTransform.localPosition.x, 0);
    }

    public void Execute()
    {
        ThrowCard();
    }

    private void ThrowCard()
    {
        DOTween.Kill(_throwTweenId);

        _cardTransform.DOAnchorPos(_targetPosition, _throwDuration).SetId(_throwTweenId)
           .SetEase(Ease.OutBounce);
    }
}
