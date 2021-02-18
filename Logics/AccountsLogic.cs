using AccountApi.Dtos;
using AccountApi.Entities;

namespace AccountApi.Logics
{
    public class AccountsLogic: AccountEntity
    {
        public static AccountDto MapAccountEntityToAccountDto(AccountEntity accountEntity)
        {
            return new AccountDto()
            {
                Id = accountEntity.Id,
                Name = accountEntity.Name,
                Iban = accountEntity.Iban,
                FavoriteQuote = accountEntity.FavoriteQuote,
                ImageUrl = accountEntity.Image?.ImageUrl,
                RandomFact = accountEntity.RandomFact
            };
        }
    }
}
