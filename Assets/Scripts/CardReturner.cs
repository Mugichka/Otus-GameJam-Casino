using UnityEngine;

public class CardReturner : ICardAction
{
    private Vector2 _offScreenPosition;
    private RectTransform _cardTransform;
    private readonly float _offScreenY = 1100f;

    public CardReturner(RectTransform cardTransform)
    {
        _offScreenPosition = new Vector2(cardTransform.localPosition.x, _offScreenY);
        _cardTransform = cardTransform;
    }

    public void Execute()
    {
        ReturnCard();
    }

    private void ReturnCard()
    {
        _cardTransform.anchoredPosition = _offScreenPosition;
    }
}
