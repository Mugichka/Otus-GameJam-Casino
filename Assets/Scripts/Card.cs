public sealed class Card
{
    private ICardAction _flipper;
    private ICardAction _thrower;
    private CardDescriptionChanger _descriptionChanger;

    public Card(ICardAction fliper, ICardAction thrower, CardDescriptionChanger descriptionChanger)
    {
        _flipper = fliper;
        _thrower = thrower;
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

    public void UpdateData(UpgradeData upgradeData)
    {
        _descriptionChanger.SetDataFromUpgrade(upgradeData);
    }
}
