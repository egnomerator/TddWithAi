namespace TddWithAi.Test;

public class BankAccountTests
{
    [Fact]
    public void DummyTest()
    {
        Assert.True(true);
    }
    
    [Fact]
    public void ShouldGetAccountName()
    {
        // arrange
        const string accountName = "TestAccount";

        // act
        var sut = new BankAccount(accountName);
        var actualAccountName = sut.GetName();

        // assert
        Assert.Equal(accountName, actualAccountName);

    }
}
