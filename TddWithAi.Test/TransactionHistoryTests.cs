using Input = TddWithAi.Test.TestsCommonInputData;

namespace TddWithAi.Test;

public class TransactionHistoryTests
{
    [Fact]
    public void ShouldGetCumulativeBalance()
    {
        // arrange
        var transactionHistory = new List<Transaction>
        {
            new(Input.OneHundred, Input.Now),
            new(Input.NegativeFifty, Input.Yesterday)
        };
        
        var expectedBalance = GetExpectedBalance(transactionHistory);

        // act
        var sut = new TransactionHistory(transactionHistory);
        var actualBalance = sut.GetCumulativeBalance();

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldGetCumulativeBalanceInDateRange()
    {
        // arrange
        var transactionHistory = new List<Transaction>
        {
            new(Input.OneHundred, Input.Now),
            new(Input.NegativeFifty, Input.Yesterday),
            new(Input.NegativeFifty, Input.FourDaysAgo),
            new(Input.NegativeFifty, Input.FiveDaysAgo),
            new(Input.OneHundred, Input.SevenDaysAgo)
        };

        var expectedBalance = GetExpectedBalanceInDateRange(
            transactionHistory, 
            Input.SixDaysAgo, 
            Input.TwoDaysAgo
        );

        // act
        var sut = new TransactionHistory(transactionHistory);
        var actualBalance = sut.GetCumulativeBalanceInDateRange(
            Input.SixDaysAgo, 
            Input.TwoDaysAgo
        );

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldAddTransactionToHistory()
    {
        // arrange
        var transactionHistory = new List<Transaction>();
        var transactionToAdd = new Transaction(Input.OneHundred, Input.Now);
        var expectedBalance = GetExpectedBalance([transactionToAdd]);

        // act
        var sut = new TransactionHistory(transactionHistory);
        sut.AddTransaction(transactionToAdd);
        var actualBalance = sut.GetCumulativeBalance();

        // assert
        Assert.Equal(expectedBalance, actualBalance);
    }

    [Fact]
    public void ShouldThrowInvalidArgumentExceptionWhenDateRangeIsInvalid()
    {
        // arrange
        var transactionHistory = new List<Transaction>();
        var startDate = Input.Now;
        var endDate = Input.Yesterday;

        // act
        var sut = new TransactionHistory(transactionHistory);
        var exception = Record.Exception(() => 
            sut.GetCumulativeBalanceInDateRange(startDate, endDate));

        // assert
        Assert.IsType<ArgumentException>(exception);
    }

    private static decimal GetExpectedBalance(List<Transaction> transactions)
    {
        var expectedBalance = 0m;
        transactions.ForEach(transaction => 
            expectedBalance += transaction.GetAmount());
        return expectedBalance;
    }

    private static decimal GetExpectedBalanceInDateRange(
        List<Transaction> transactions, 
        DateTime startDate, 
        DateTime endDate)
    {
        var expectedBalance = 0m;
        transactions.ForEach(transaction => {
            var isInDateRange = startDate <= transaction.GetDate() && transaction.GetDate() <= endDate;
            if (!isInDateRange) return;
            expectedBalance += transaction.GetAmount();
        });
        return expectedBalance;
    }
}
