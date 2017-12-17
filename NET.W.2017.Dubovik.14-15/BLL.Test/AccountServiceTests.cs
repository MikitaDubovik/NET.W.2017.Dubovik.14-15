using System;
using BLL.Interface.IDGenerator;
using DAL.Interface;
using DAL.Interface.DTO;
using Moq;
using NET.W._2017.Dubovik._14_15.AccountService;
using NUnit.Framework;

namespace BLL.Test
{
    [TestFixture]
    public class AccountServiceTests
    {
        #region Source

        private static string ownerFirstName;
        private static string ownerSecondName;
        private static string ownerAccountId;

        private static IAccountService accountService;

        private static Mock<IRepository> repositoryMock;
        private static Mock<IIdGenerator> accountIdGeneratorMock;

        [OneTimeSetUp]
        public void Init()
        {
            ownerFirstName = "Black";
            ownerSecondName = "Elephant";
            ownerAccountId = "123976-297346ssd";

            repositoryMock = new Mock<IRepository>();

            accountIdGeneratorMock = new Mock<IIdGenerator>(MockBehavior.Strict);
            accountIdGeneratorMock.Setup(service => service.GenerateAccountId()).Returns(ownerAccountId);

            accountService = new AccountService(repositoryMock.Object, accountIdGeneratorMock.Object);

            accountService.OpenAccount(ownerFirstName, ownerSecondName, 100m);
        }

        #endregion

        #region Test

        [Test]
        public void GenerateIdTest()
        {
            accountIdGeneratorMock.Verify(
                generator => generator.GenerateAccountId(), Times.Once);
        }

        [Test]
        public void AddAccountTest()
        {
            repositoryMock.Verify(
                repository => repository.AddAccount(It.Is<BankAccount>(account => string.Equals(account.AccountId, ownerAccountId, StringComparison.Ordinal))), Times.Once);
        }

        [Test]
        public void RemoveAccountTest()
        {
            string tempId = "9018234-2345978";

            accountIdGeneratorMock.Setup(service => service.GenerateAccountId()).Returns(tempId);

            accountService.OpenAccount(ownerFirstName, ownerSecondName, 79);

            accountService.CloseAccount(tempId);

            repositoryMock.Verify(
                repository => repository.RemoveAccount(It.Is<BankAccount>(account => string.Equals(account.AccountId, tempId, StringComparison.Ordinal))), Times.Once);
        }

        [Test]
        public void GetAccountsFromRepositoryTest()
        {
            repositoryMock.Verify(repository => repository.GetAccounts(), Times.Once);
        }

        [Test]
        public void UpdateAccountTest()
        {
            accountService.DepositMoney(ownerAccountId, 300m);
            accountService.WithdrawMoney(ownerAccountId, 100m);

            repositoryMock.Verify(
                repository => repository.UpdateAccount(It.Is<BankAccount>(account => string.Equals(account.AccountId, ownerAccountId, StringComparison.Ordinal))), Times.Exactly(2));
        }

        #endregion
    }
}
