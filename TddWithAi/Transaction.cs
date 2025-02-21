namespace TddWithAi;

public class Transaction
{
    private readonly decimal _amount;
    private readonly DateTime _dateTime;

    public Transaction(decimal amount, DateTime dateTime)
    {
        _amount = amount;
        _dateTime = dateTime;
    }

    public decimal GetAmount()
    {
        return _amount;
    }

    public DateTime GetDate()
    {
        return _dateTime;
    }
} 