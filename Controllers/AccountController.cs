using AccountApi.Context;
using AccountApi.Dtos;
using AccountApi.Entities;
using AccountApi.ThirdPartyApis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountContext _context;

        public AccountController(AccountContext accountContext)
        {
            _context = accountContext;
        }
        /// <summary>
        ///  Get list of all available accounts
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AccountDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpGet(Name = "GetAccounts")]
        public ActionResult<List<AccountDto>> GetAccounts()
        {
            var accounts = _context.Accounts.ToList();

            if (accounts.Count == 0)
                return NotFound("No accounts found");

            var accountDtos = accounts.Select(a => a.ToDto());

            return Ok(accountDtos);

        }

        /// <summary>
        ///  Get account by Id
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpGet("{id}", Name = "GetAccountById")]
        public ActionResult<AccountDto> GetAccountById(Guid id)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Id == id);

            if (account == null)
                return NotFound("No account found");

            return Ok(account);
        }

        /// <summary>
        ///  Add new account
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost(Name = "AddAccount")]
        public ActionResult<string> AddAccount(AddAccountDto addAccount)
        {
            var accounts = _context.Accounts;

            var isValidGuid = Guid.TryParse(addAccount.Id.ToString(), out var id);
            var ibanAlreadyRegistered = accounts.Any(a => a.Iban == addAccount.Iban);
            var idAlreadyUsed = accounts.Any(a => a.Id == addAccount.Id);

            if (!isValidGuid)
                return BadRequest("Ïnvalid ID");

            if (idAlreadyUsed)
                return BadRequest("Id already in use");

            if (ibanAlreadyRegistered)
                return BadRequest("IBAN already assigned to account");


            var newAccount = new AccountEntity()
            {
                Id = id,
                Name = addAccount.Name,
                Iban = addAccount.Iban,
                FavoriteQuote = addAccount.FavoriteQuote,
            };

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            return StatusCode(201, "Account successfully created");
        }

        /// <summary>
        ///  Add a random fact to account
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("{id}/AddRandomFactToAccount", Name = "AddRandomFactToAccount")]
        public async Task<ActionResult<string>> AddRandomFactToAccount(Guid id)
        {
            var account = _context.Accounts.Find(id);

            if (account == null)
                return NotFound("No account find with this id");
            
                var fact = await RandomFact.GetFact();
                if(fact == null)
                return BadRequest("New fact: Something went wrong (This is a error message)");
            
            account.RandomFact = fact.Text;

            _context.SaveChanges();
            return Ok($"Fact '{account.RandomFact}' is added to account with id: '{account.Id}'");
        }

        /// <summary>
        ///  Update Account
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpPut("{id}", Name = "UpdateAccount")]
        public ActionResult<string> UpdateAccount(Guid id, EditAccountDto editAccount)
        {
            var account = _context.Accounts.Find(id);

            if (account == null)
                return NotFound("No account find with this id");

            // The method below could also be achieved by ignoring NULL values in the DTO
            if (editAccount.Name != null)
                account.Name = editAccount?.Name;

            if (editAccount.Iban != null)
                account.Iban = editAccount?.Iban;

            if (editAccount.FavoriteQuote != null)
                account.FavoriteQuote = editAccount?.FavoriteQuote;

            _context.SaveChanges();
            return Ok($"Account with id: '{account.Id}' is edited");
        }

        /// <summary>
        ///  Only update account name
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpPut("{id}/UpdateAccountName", Name = "UpdateAccountName")]
        public ActionResult<string> UpdateAccountName(Guid id, string name)
        {
            var account = _context.Accounts.Find(id);

            if (account == null)
                return NotFound("No account find with this id");

            account.Name = name;

            _context.SaveChanges();
            return Ok($"Account with id: '{account.Id}' is edited");
        }

        /// <summary>
        ///  Delete account
        /// </summary>
        [HttpDelete("{id}", Name = "DeleteAccount")]
        public ActionResult<string> Delete(Guid id)
        {
            var account = _context.Accounts.Find(id);

            if (account == null)
                return NotFound("No account find with this id");

            _context.Remove(account);
            _context.SaveChanges();
            return Ok($"Account with id: {id} deleted");
        }
    }
}
