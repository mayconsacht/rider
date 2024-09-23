using Shared.DTO.Account;

namespace Ride.Tests.Mocks;

public class AccountMock
{
    public class DTO()
    {
        public static AccountDto Create(Action<AccountDto> internalChange = null)
        { 
            var dto = new AccountDto
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john.doe@gmail.com",
                CarPlate = "MBO8765",
                IsDriver = true,
                IsPassenger = true
            };
            internalChange?.Invoke(dto);
            return dto;
        }
    }
}