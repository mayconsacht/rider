using BuildingBlocks.Shared.DTO.Account;

namespace Account.Tests.Mocks.Account;

public static class AccountMock
{
    public static AccountNoIdDto CreateNoIdDto()
    {
        return new AccountNoIdDto
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            CarPlate = "MBO8765",
            IsDriver = true,
            IsPassenger = true
        };
    }
}
