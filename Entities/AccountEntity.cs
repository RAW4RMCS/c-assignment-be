using AccountApi.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AccountApi.Entities
{
    public class AccountEntity: BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Iban { get; set; }
        public ImageEntity Image { get; set; }
        public string FavoriteQuote { get; set; }
        public string RandomFact { get; set; }

        public AccountDto ToDto()
        {
            return new AccountDto()
            {
                Id = Id,
                Name = Name,
                Iban = Iban,
                ImageUrl = Image?.ImageUrl,
                FavoriteQuote = FavoriteQuote,
                RandomFact = RandomFact,
            };
        }
    }

}
