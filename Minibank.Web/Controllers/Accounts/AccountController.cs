using Microsoft.AspNetCore.Mvc;
using Minibank.Core;
using Minibank.Core.Domains.AccountsBank;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Web.Controllers.Accounts.Dto;
using System;
using System.Collections.Generic;


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
        public void TransferFee(double sum, string fromAccountBankId, string toAccountBankId)
        {
            _accountBankService.TransferFee(sum, fromAccountBankId, toAccountBankId, _database);
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
        public void CloseAccount(string id)
        {
            _accountBankService.CloseAccount(id);
            
        }
        /// <summary>
        /// Найти аккаунт по id
        /// </summary>
        /// <param name="id">id аккаунта</param>
        /// <returns>возвращает нужным аккаунт</returns>
        [HttpGet("/{id}")]
        public AccountBank GetAccount(string id)
        {
            return _accountBankService.GetAccount(id);
        }
        
        /// <summary>
        /// Показать все аккаунты пользователя
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <returns>список аккаунтов</returns>
        [HttpGet]
        public IEnumerable<AccountBank> GetAccounts(string UserId)
        {
            return _accountBankService.Get(UserId);
        }
        /// <summary>
        /// Создать аккаунт
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="currency">в какой валюте храним</param>
        /// <param name="balance">изначальный баланс</param>
        /// <exception cref="ValidationException"></exception>
        [HttpPost()]
        public void CreateAccount(string UserId, string currency, double balance)
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
            _accountBankService.Create(new AccountBank()
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
