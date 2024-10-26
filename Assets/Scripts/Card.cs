public sealed class Card
{
    private ICardAction _flipper;
    private ICardAction _thrower;

    public Card(ICardAction fliper, ICardAction thrower)
    {
        _flipper = fliper;
        _thrower = thrower;
    }

    public void Flip()
    {
        _flipper?.Execute();
    }

    public void Throw()
    {
        _thrower?.Execute();
    }
}
