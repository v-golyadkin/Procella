public class RefillManaGA : GameAction
{
    public int Amount { get; set; }

    public RefillManaGA(int amount)
    {
        Amount = amount;
    }
}
