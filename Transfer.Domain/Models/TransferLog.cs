namespace Transfer.Domain.Models;

public class TransferLog
{
    public int Id { get; set; }
    public int AccountSourceId { get; set; }
    public int AccountTargetId { get; set; }
    public decimal Amount { get; set; }
}