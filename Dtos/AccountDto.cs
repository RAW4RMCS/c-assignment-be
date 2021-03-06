﻿using System;

namespace AccountApi.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Iban { get; set; }
        public string ImageUrl { get; set; }
        public string FavoriteQuote { get; set; }
        public string RandomFact { get; set; }
    }
}
