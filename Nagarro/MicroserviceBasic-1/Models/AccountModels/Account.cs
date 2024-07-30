namespace Models.AccountModels;

public class Account
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Balance { get; set; }
}