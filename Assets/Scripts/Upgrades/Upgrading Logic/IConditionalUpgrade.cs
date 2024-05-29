namespace Upgrades.UpgradingLogic
{
    public interface IConditionalUpgrade
    {
        void HandleConditions(Upgrade[] upgrades);
    }
}