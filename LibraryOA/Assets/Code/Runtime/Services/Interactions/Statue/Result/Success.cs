namespace Code.Runtime.Services.Interactions.Statue.Result
{
    internal sealed class Success : Result
    {
        public readonly int LivesRestored;
        public readonly int MoneySpent;

        public Success(int livesRestored, int moneySpent)
        {
            LivesRestored = livesRestored;
            MoneySpent = moneySpent;
        }
    }
}