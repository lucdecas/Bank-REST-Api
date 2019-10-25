using System;
using BankAccount.Domain.Exceptions;
using BankAccount.Domain.Services;
using BankAccount.Repository;
using Microsoft.AspNetCore.Mvc;
using BankAccount.Domain.Models;
using System.Web.Script.Serialization;

namespace BankAccount.Controllers
{

    [Route("Bank/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        public class RequestBody
        {
            public int amount { get; set; }
        }

        AccountService accountService = new AccountService(new AccountRepository());

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Account account = accountService.getAccountStatements(id);

            return new JavaScriptSerializer().Serialize(account);
        }

        [HttpPost("{id}/deposit")]
        public void DepositCurrency(int id, [FromBody] RequestBody request)
        {
            try
            {
                accountService.depositCurrency(id, request.amount);
            }
            catch (Exception e)
            {
                defineErrorReason(e);
            }
        }

        [HttpPut("{id}/withdraw")]
        public void WithdrawCurrency(int id,[FromBody] RequestBody request)
        {
            try
            {
                accountService.withdrawCurrency(id, request.amount);
            }
            catch (Exception e)
            {
                defineErrorReason(e);
            }
        }

        [HttpPost("{id}/transfer/{receiverId}")]
        public void TransferCurrency(int id, int receiverId, RequestBody request)
        {
            try
            {
                accountService.transferCurrency(id, receiverId, request.amount);
            }
            catch (Exception e)
            {
                defineErrorReason(e);
            }
        }

        private void defineErrorReason(Exception ex)
        {
            if (ex is OperationNotAllowedException || ex is AccountNotFoundException)
            {
                throw new ApiException(400, ex.Message);
            }

            throw new ApiException(500, ex.Message);
        }
    }


}
