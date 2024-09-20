namespace BuildingBlocks.Shared.DTO.Account;

public class AccountNoIdDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? CarPlate { get; set; }
    public bool IsPassenger { get; set; }
    public bool IsDriver { get; set; }
}