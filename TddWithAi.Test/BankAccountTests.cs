using Input = TddWithAi.Test.TestsCommonInputData;

namespace TddWithAi.Test;

public class BankAccountTests
{
    private const string InitialName = "InitialName";
    private readonly TransactionHistory _initialEmptyTransactionHistory = new(new List<Transaction>());
    private const decimal ExpectedInitialZeroBalance = 0;

    [Fact]
    public void ShouldGetAccountName()
    {
        // act
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);
        var actualAccountName = sut.GetName();

        // assert
        Assert.Equal(InitialName, actualAccountName);
    }

    [Fact]
    public void ShouldChangeAccountName()
    {
        // arrange
        const string newName = "UpdatedName";

        // act
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);
        sut.SetName(newName);
        var actualName = sut.GetName();

        // assert
        Assert.Equal(newName, actualName);
    }

    [Fact]
    public void ShouldGetZeroBalanceFromInitialBankAccount()
    {
        // act
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);
        var actualBalance = sut.GetBalance();

        // assert
        Assert.Equal(ExpectedInitialZeroBalance, actualBalance);
    }

    [Fact]
    public void ShouldCreateNewBankAccountWithInitialEmptyTransactionHistory()
    {
        // act
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);
        var actualBalance = sut.GetBalance();

        // assert
        Assert.Equal(ExpectedInitialZeroBalance, actualBalance);
    }

    [Fact]
    public void ShouldApplyDebit()
    {
        // arrange
        const decimal expectedBalance = -50.00m;

        // act
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);
        sut.ApplyDebit(Input.Fifty);
        var actualBalance = sut.GetBalance();

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldApplyCredit()
    {
        // act
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);
        sut.ApplyCredit(Input.Fifty);
        var actualBalance = sut.GetBalance();

        // assert
        Assert.Equal(Input.Fifty, actualBalance);
    }

    [Fact]
    public void ShouldCreateExistingBankAccountWithTransactionHistory()
    {
        // arrange
        var transactionHistory = new TransactionHistory(new List<Transaction>
        {
            new(Input.OneHundred, Input.Now),
            new(Input.NegativeFifty, Input.Yesterday)
        });
        var expectedBalance = transactionHistory.GetCumulativeBalance();

        // act
        var sut = new BankAccount(InitialName, transactionHistory);
        var actualBalance = sut.GetBalance();

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldGetSnapshotBalanceOfTransactionHistoryInDateRange()
    {
        // arrange
        var transactionHistory = new TransactionHistory(new List<Transaction>
        {
            new(Input.OneHundred, Input.Yesterday),
            new(Input.NegativeFifty, Input.SixDaysAgo)
        });
        var expectedBalance = transactionHistory.GetCumulativeBalanceInDateRange(
            Input.FourDaysAgo, 
            Input.Now
        );

        // act
        var sut = new BankAccount(InitialName, transactionHistory);
        var actualBalance = sut.GetSnapshotBalanceOfTransactionHistoryInDateRange(
            Input.FourDaysAgo, 
            Input.Now
        );

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldIncludeNewTransactionsInSnapshotBalance()
    {
        // arrange
        var initialTransactions = new List<Transaction>
        {
            new(Input.OneHundred, Input.Yesterday),
            new(Input.NegativeFifty, Input.SixDaysAgo)
        };
        var expectedReflectionOfTransactionHistory = new TransactionHistory(initialTransactions);
        expectedReflectionOfTransactionHistory.AddTransaction(new Transaction(Input.OneHundred, Input.Now));
        expectedReflectionOfTransactionHistory.AddTransaction(new Transaction(Input.NegativeFifty, Input.Now));
        var expectedBalance = expectedReflectionOfTransactionHistory.GetCumulativeBalanceInDateRange(
            Input.FourDaysAgo, 
            Input.Now.AddDays(1)
        );

        // act
        var sut = new BankAccount(InitialName, new TransactionHistory(new List<Transaction>
        {
            new(Input.OneHundred, Input.Yesterday),
            new(Input.NegativeFifty, Input.SixDaysAgo)
        }));
        sut.ApplyCredit(Input.OneHundred);
        sut.ApplyDebit(Input.Fifty);
        var actualBalance = sut.GetSnapshotBalanceOfTransactionHistoryInDateRange(
            Input.FourDaysAgo, 
            Input.Now.AddDays(1)
        );

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldThrowInvalidArgumentExceptionWhenNegativeAmountProvidedForDebit()
    {
        // arrange
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);

        // act
        var exception = Record.Exception(() => 
            sut.ApplyDebit(Input.NegativeFifty));

        // assert
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void ShouldThrowInvalidArgumentExceptionWhenNegativeAmountProvidedForCredit()
    {
        // arrange
        var sut = new BankAccount(InitialName, _initialEmptyTransactionHistory);

        // act
        var exception = Record.Exception(() => 
            sut.ApplyCredit(Input.NegativeFifty));

        // assert
        Assert.IsType<ArgumentException>(exception);
    }
}
