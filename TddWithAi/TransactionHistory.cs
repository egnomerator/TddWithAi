namespace TddWithAi;

public class TransactionHistory
{
    private readonly List<Transaction> _transactions;

    public TransactionHistory(List<Transaction> transactions)
    {
        _transactions = transactions;
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public decimal GetCumulativeBalance()
    {
        return _transactions.Sum(transaction => transaction.GetAmount());
    }

    public decimal GetCumulativeBalanceInDateRange(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date cannot be after end date");
        }

        return _transactions
            .Where(t => startDate <= t.GetDate() && t.GetDate() <= endDate)
            .Sum(t => t.GetAmount());
    }
} 