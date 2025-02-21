namespace TddWithAi.Test;

public class TransactionTests
{
    [Theory]
    [InlineData(100.00)]
    [InlineData(-100.00)]
    public void ShouldGetTransactionAmountAndDate(decimal amount)
    {
        // arrange
        var transactionDate = DateTime.Now;

        // act
        var sut = new Transaction(amount, transactionDate);
        var actualAmount = sut.GetAmount();
        var actualDate = sut.GetDate();

        // assert
        Assert.Equal(amount, actualAmount);
        Assert.Equal(transactionDate, actualDate);
    }   

}
