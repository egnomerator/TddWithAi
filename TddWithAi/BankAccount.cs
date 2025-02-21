namespace TddWithAi;

using System.Collections.Generic;
using System;

public class BankAccount
{
    private TransactionHistory TransactionHistory { get; }
    private string Name { get; set; }
    private const string NegativeAmountExceptionMessage = "Amount cannot be negative";

    public BankAccount(string name, TransactionHistory transactionHistory)
    {
        Name = name;
        TransactionHistory = transactionHistory ?? new TransactionHistory(new List<Transaction>());
    }

    public decimal GetBalance()
    {
        return TransactionHistory.GetCumulativeBalance();
    }

    public void ApplyDebit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException(NegativeAmountExceptionMessage);
        }
        TransactionHistory.AddTransaction(new Transaction(-amount, DateTime.Now));
    }

    public void ApplyCredit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException(NegativeAmountExceptionMessage);
        }
        TransactionHistory.AddTransaction(new Transaction(amount, DateTime.Now));
    }

    public string GetName()
    {
        return Name;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public decimal GetSnapshotBalanceOfTransactionHistoryInDateRange(DateTime startDate, DateTime endDate)
    {
        return TransactionHistory.GetCumulativeBalanceInDateRange(startDate, endDate);
    }
} 