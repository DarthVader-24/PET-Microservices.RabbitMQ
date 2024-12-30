namespace Banking.Application.Dtos;

public class AccountTransferDto
{
    public int AccountSourceId { get; init; }
    public int AccountDestinationId { get; init; }
    public decimal TransferAmount { get; init; }
}