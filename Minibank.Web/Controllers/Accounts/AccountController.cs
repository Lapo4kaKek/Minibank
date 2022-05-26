using Microsoft.AspNetCore.Mvc;
using Minibank.Core;
using Minibank.Core.Domains.AccountsBank;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Web.Controllers.Accounts.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Minibank.Web.Controllers.Accounts
{
    [ApiController]
    [Route("/api/accounts")]
    public class AccountController
    {
        private readonly IAccountBankService _accountBankService;

        private readonly IDatabase _database;
        public AccountController(IAccountBankService accountBankService, IDatabase database)
        {
            _accountBankService = accountBankService;
            _database = database;
        }
        /// <summary>
        /// Денежный перевод
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="fromAccountBankId">аккаунт отправителя</param>
        /// <param name="toAccountBankId">аккаунт получателя</param>
        [HttpPost("/transfer")]
        public async Task TransferFeeAsync(double sum, string fromAccountBankId, string toAccountBankId)
        {
           await  _accountBankService.TransferFeeAsync(sum, fromAccountBankId, toAccountBankId, _database);
        }
        /// <summary>
        /// Узнать комиссию
        /// </summary>
        /// <param name="sum">сумма</param>
        /// <param name="fromAccountBankId">аккаунт отправителя</param>
        /// <param name="toAccountBankId">аккаунт получателя</param>
        /// <returns>Размер комиссии</returns>
        [HttpGet("/commission")]
        public double Transfeer(double sum, string fromAccountBankId, string toAccountBankId)
        {
            return _accountBankService.Comission(sum, fromAccountBankId, toAccountBankId);
        }
        /// <summary>
        /// Закрыть аккаунт
        /// </summary>
        /// <param name="id">id аккаунта который надо закрыть</param>
        /// <exception cref="ValidationException"></exception>
        [HttpPost("/{id}/close")]
        public async Task CloseAccountAsync(string id)
        {
            await _accountBankService.CloseAccountAsync(id);
            
        }
        /// <summary>
        /// Найти аккаунт по id
        /// </summary>
        /// <param name="id">id аккаунта</param>
        /// <returns>возвращает нужным аккаунт</returns>
        [HttpGet("/{id}")]
        public async Task<AccountBank> GetAccountAsync(string id)
        {
            return await _accountBankService.GetAccountAsync(id);
        }
        
        /// <summary>
        /// Показать все аккаунты пользователя
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <returns>список аккаунтов</returns>
        [HttpGet]
        public async Task<IEnumerable<AccountBank>> GetAccountsAsync(string userId)
        {
            return await _accountBankService.GetAsync(userId);
        }
        /// <summary>
        /// Создать аккаунт
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="currency">в какой валюте храним</param>
        /// <param name="balance">изначальный баланс</param>
        /// <exception cref="ValidationException"></exception>
        [HttpPost()]
        public async Task CreateAccountAsync(string UserId, string currency, double balance)
        {
            AccountDto model = new AccountDto()
            {
                UserId = UserId,
                Balance = balance,
                Currency = currency,
                OpenAccount = DateTime.Now,
                CloseAccount = DateTime.MaxValue,
                AccountPriv = true
            };
            await _accountBankService.CreateAsync(new AccountBank()
            {
                UserId = model.UserId,
                Id = model.Id,
                Balance = model.Balance,
                OpenAccount = model.OpenAccount,
                Currency = model.Currency,
                CloseAccount = model.CloseAccount,
                AccountPriv = model.AccountPriv
            });
        }

    }
}
