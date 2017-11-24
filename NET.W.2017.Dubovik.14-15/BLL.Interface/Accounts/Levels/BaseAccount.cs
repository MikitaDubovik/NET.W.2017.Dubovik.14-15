namespace NET.W._2017.Dubovik._14_15.Levels
{
    /// <inheritdoc />
    /// <summary>
    /// Successor of the basic account
    /// </summary>
    public class BaseAccount : Account
    {
        /// <inheritdoc />
        public BaseAccount(string id, string ownerFirstName, string ownerSecondName, decimal currentSum, int bonusPoints) :
            base(id, ownerFirstName, ownerSecondName, currentSum, bonusPoints)
        {
            this.BonusValue = 11;
        }

        /// <inheritdoc />
        public override string ToString() => "Base account: " + base.ToString();

        /// <inheritdoc />
        protected override int AddBonusPoints(decimal sum, int bonusValue) =>
            (int)(((sum + bonusValue) % bonusValue) + bonusValue) / 2;

        /// <inheritdoc />
        protected override int WithdrawBonusPoints(decimal sum, int bonusValue) =>
            (int)(((sum + bonusValue) % bonusValue) - bonusValue) / 2;
    }
}
