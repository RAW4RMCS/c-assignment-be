using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Dtos
{
    public class AddAccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public string FavoriteQuote { get; set; }
    }
}
