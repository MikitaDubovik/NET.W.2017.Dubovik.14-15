using System;

namespace DAL.Interface.DTO
{
    public class BankAccount : IComparable<BankAccount>, IEquatable<BankAccount>, IComparable
    {
        public Type AccountType { get; set; }

        public string Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerSecondName { get; set; }

        public decimal CurrentSum { get; set; }

        public int BonusPoints { get; set; }

        protected int BonusValue { get; set; }

        #region object override methods

        public override string ToString() =>
            $"ID-{Id} Owner's first name-{OwnerFirstName} Owner's second name-{OwnerSecondName} Current sum-{CurrentSum} Bonus points-{BonusPoints}";

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((BankAccount)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AccountType != null ? AccountType.GetHashCode() : 0;
                hashCode = (hashCode * 322) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 322) ^ (OwnerFirstName != null ? OwnerFirstName.GetHashCode() : 0);
                hashCode = (hashCode * 322) ^ (OwnerSecondName != null ? OwnerSecondName.GetHashCode() : 0);
                hashCode = (hashCode * 322) ^ CurrentSum.GetHashCode();
                hashCode = (hashCode * 322) ^ BonusPoints;
                return hashCode;
            }
        }

        #endregion

        #region Implementation

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() != this.GetType() ? 1 : this.CompareTo((BankAccount)obj);
        }

        /// <inheritdoc />
        public int CompareTo(BankAccount other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return other.Id == Id ? 0 : -1;
        }

        /// <inheritdoc />
        public bool Equals(BankAccount other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Id == other.Id;
        }

        #endregion 
    }
}
