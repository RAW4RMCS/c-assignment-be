using AccountApi.Dtos;
using System.ComponentModel.DataAnnotations;

namespace AccountApi.Entities
{
    public class AccountEntity: BaseEntity
    {
        [MaxLength(length: 100, ErrorMessage = "MaxLength is 100 characters"), Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [MinLength(16, ErrorMessage = "MinLength is 16 characters"), Required(ErrorMessage = "Iban is required")]
        public string Iban { get; set; }
        public ImageEntity Image { get; set; }
        public string FavoriteQuote { get; set; }
        public string RandomFact { get; set; }
    }

}
