using System;

namespace BLL.Interface.Accounts
{
    public abstract class Account : IComparable, IComparable<Account>, IEquatable<Account>
    {
        #region Fields

        private string id;
        private string ownerFirstName;
        private string ownerSecondName;

        #endregion

        #region Cotr

        /// <summary>
        /// Initializes the a new instance of the <see cref="Account"/> class
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="ownerFirstName">Owner name</param>
        /// <param name="ownerSecondName">Owner surname</param>
        /// <param name="currentSum">Account's sum</param>
        /// <param name="bonusPoints">Bonus points</param>
        /// <exception cref="ArgumentException">If one of the parameters is incorrect</exception>
        protected Account(string id, string ownerFirstName, string ownerSecondName, decimal currentSum, int bonusPoints)
        {
            CheckInput(id, ownerFirstName, ownerSecondName, currentSum, bonusPoints);

            Id = id;
            OwnerFirstName = ownerFirstName;
            OwnerSecondName = ownerSecondName;
            CurrentSum = currentSum;
            BonusPoints = bonusPoints;
        }

        #endregion 

        #region public 

        #region properties

        /// <summary>
        /// Id of owner
        /// </summary>
        public string Id
        {
            get => id;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(Id));
                }

                id = value;
            }
        }

        /// <summary>
        /// Owner's first name
        /// </summary>
        public string OwnerFirstName
        {
            get => ownerFirstName;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(OwnerFirstName));
                }

                ownerFirstName = value;
            }
        }

        /// <summary>
        /// Owner's second name
        /// </summary>
        public string OwnerSecondName
        {
            get => ownerSecondName;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(OwnerSecondName));
                }

                ownerSecondName = value;
            }
        }

        /// <summary>
        /// Current sum on account
        /// </summary>
        public decimal CurrentSum { get; private set; }

        /// <summary>
        /// Bonus points on account
        /// </summary>
        public int BonusPoints { get; private set; }

        /// <summary>
        /// Bonus value on account
        /// </summary>
        protected int BonusValue { get; set; }

        #endregion 

        #region Methods

        /// <summary>
        /// Add money and bonus points to the account
        /// </summary>
        /// <param name="sum">deposit money</param>
        /// <exception cref="ArgumentException">If <paramref name="sum"/> &lt;= 0</exception>
        public void DepositMoney(decimal sum)
        {
            if (sum <= 0)
            {
                throw new ArgumentException("Sum must be greater than zero", nameof(sum));
            }

            CurrentSum += sum;
            BonusPoints += AddBonusPoints(sum, BonusValue);
        }

        /// <summary>
        /// Withdraws money from the account.
        /// </summary>
        /// <param name="sum">withdrawal amount</param>
        /// <exception cref="ArgumentException">If <paramref name="sum"/> sum &lt;= 0.</exception>
        public void WithdrawMoney(decimal sum)
        {
            if (sum <= 0)
            {
                throw new ArgumentException("Sum must be greater than zero", nameof(sum));
            }

            if (CurrentSum < sum)
            {
                return;
            }

            CurrentSum -= sum;
            BonusPoints -= WithdrawBonusPoints(sum, BonusValue);
        }

        #endregion

        #region Override methods

        /// <summary>
        /// Returns a string representation of a account
        /// </summary>
        /// <returns>String representation of a account</returns>
        public override string ToString() =>
            $"ID-{Id} Owner's first name-{OwnerFirstName} Owner's second name-{OwnerSecondName} Current sum-{CurrentSum} Bonus points-{BonusPoints}";

        /// <summary>
        /// Verify the equivalence of the current account and the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>True if objects are equivalent, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((Account)obj);
        }

        /// <summary>
        /// Returns account hash code
        /// </summary>
        /// <returns>Account hash code</returns>
        public override int GetHashCode() => 322 * Id.GetHashCode();

        #endregion

        #region Implementation

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() != this.GetType() ? 1 : this.CompareTo((Account)obj);
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares two account
        /// </summary>
        /// <param name="other">Account for comparison</param>
        /// <returns>1 if ither account is null, 0 if they are identical, -1 in other options</returns>
        public int CompareTo(Account other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return other.Id == Id ? 0 : -1;
        }

        /// <inheritdoc />
        /// <summary>
        /// Сhecks the equivalence of the current account and the <paramref name="other" /> account
        /// </summary>
        /// <param name="other">Account to compare</param>
        /// <returns>True if accounts are equivalent, false otherwise</returns>
        public bool Equals(Account other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Id == other.Id;
        }

        #endregion 

        #endregion 

        #region protected

        #region methods

        /// <summary>
        /// Calculate the bonus increment when depositing money into an account
        /// </summary>
        /// <param name="sum">Deposit sum</param>
        /// <param name="bonusValue">Bonus factor</param>
        /// <returns>Bonus increment</returns>
        protected abstract int AddBonusPoints(decimal sum, int bonusValue);

        /// <summary>
        /// Calculate the increment of the bonus when withdrawing money from the account
        /// </summary>
        /// <param name="sum">Deposit sum</param>
        /// <param name="bonusValue">Bonus factor</param>
        /// <returns>Bonus decrement</returns>
        protected abstract int WithdrawBonusPoints(decimal sum, int bonusValue);

        #endregion 

        #endregion 

        #region private

        private static void CheckInput(string id, string ownerFirstName, string ownerSecondName, decimal currentSum, int bonusPoints)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrEmpty(ownerFirstName))
            {
                throw new ArgumentException(nameof(ownerFirstName));
            }

            if (string.IsNullOrEmpty(ownerSecondName))
            {
                throw new ArgumentException(nameof(ownerSecondName));
            }

            if (currentSum < 0)
            {
                throw new ArgumentException(nameof(currentSum));
            }

            if (bonusPoints < 0)
            {
                throw new ArgumentException(nameof(bonusPoints));
            }
        }

        #endregion
    }
}
