public sealed class Card
{
    private ICardAction _flipper;
    private ICardAction _thrower;
    private ICardAction _returner;
    private CardDescriptionChanger _descriptionChanger;

    public Card(ICardAction fliper, ICardAction thrower, ICardAction returner, CardDescriptionChanger descriptionChanger)
    {
        _flipper = fliper;
        _thrower = thrower;
        _returner = returner;
        _descriptionChanger = descriptionChanger;
    }

    public void Flip()
    {
        _flipper?.Execute();
    }

    public void Throw()
    {
        _thrower?.Execute();
    }

    public void Return()
    {
        _returner?.Execute();
    }

    public void UpdateData(UpgradeData upgradeData)
    {
        _descriptionChanger.SetDataFromUpgrade(upgradeData);
    }
}
