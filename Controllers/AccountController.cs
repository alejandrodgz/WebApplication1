using ATMApi.Models;
using ATMApi.Repository;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ATMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetAccountById([FromRoute] int accountid)
        {
            if (accountid == 0)
            {
                return NotFound();
            }
            var account = await _accountRepository.GetAccountByIdAsync(accountid);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpGet("balance/{accountid}")]
        public async Task<IActionResult> GetBalanceByid([FromRoute] int accountid)
        {
            if (accountid == 0)
            {
                return NotFound();
            }
            var account = await _accountRepository.GetAccountByIdAsync(accountid);
            if (account == null)
            {
                return NotFound();
            }
            return Ok("your balance is" + account.Balance);
        }

        [HttpPost("")]
        public async Task<IActionResult> NewAccount([FromBody] AccountModels accountmodel)
        {
            var id = await _accountRepository.NewAccountAsync(accountmodel);
            return CreatedAtAction(nameof(GetAccountById), new { accountid = id, controller = "account" }, id);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAccount([FromBody] AccountModels accountmodel, [FromRoute]int id)
        {
            await _accountRepository.UpdateAccountAsync(id, accountmodel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAccountPatch([FromBody]Microsoft.AspNetCore.JsonPatch.JsonPatchDocument accountmodel, [FromRoute]int id)
        {
            await _accountRepository.UpdateAccountPatchAsync(id, accountmodel);
            return Ok(accountmodel);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAccount([FromRoute]int id)
        {
            await _accountRepository.DeleteAccountAsync(id);
            return Ok();
        }

    }
}
