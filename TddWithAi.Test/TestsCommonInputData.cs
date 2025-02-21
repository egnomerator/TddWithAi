namespace TddWithAi.Test;

public static class TestsCommonInputData
{
    public const decimal OneHundred = 100.00m;
    public const decimal NegativeOneHundred = -100.00m;
    public const decimal NegativeFifty = -50.00m;
    public const decimal Fifty = 50.00m;

    public static readonly DateTime Now = DateTime.Now;
    public static readonly DateTime Yesterday = DateTime.Now.AddDays(-1);
    public static readonly DateTime TwoDaysAgo = DateTime.Now.AddDays(-2);
    public static readonly DateTime FourDaysAgo = DateTime.Now.AddDays(-4);
    public static readonly DateTime FiveDaysAgo = DateTime.Now.AddDays(-5);
    public static readonly DateTime SixDaysAgo = DateTime.Now.AddDays(-6);
    public static readonly DateTime SevenDaysAgo = DateTime.Now.AddDays(-7);
} 