using System;
using BankAccount.Domain.Exceptions;
using BankAccount.Domain.Services;
using BankAccount.Repository;
using Microsoft.AspNetCore.Mvc;
using BankAccount.Domain.Models;
using System.Web.Script.Serialization;
using System.Net;

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
        public ActionResult DepositCurrency(int id, [FromBody] RequestBody request)
        {
            try
            {
                accountService.depositCurrency(id, request.amount);
                return Ok();
            }
            catch (Exception e)
            {
                return defineErrorReason(e);
            }
        }

        [HttpPut("{id}/withdraw")]
        public ActionResult WithdrawCurrency(int id,[FromBody] RequestBody request)
        {
            try
            {
                accountService.withdrawCurrency(id, request.amount);
                return Ok();
            }
            catch (Exception e)
            {
                return defineErrorReason(e);
            }
        }

        [HttpPost("{id}/transfer/{receiverId}")]
        public ActionResult TransferCurrency(int id, int receiverId, RequestBody request)
        {
            try
            {
                accountService.transferCurrency(id, receiverId, request.amount);
                return Ok();
            }
            catch (Exception e)
            {
                return defineErrorReason(e);
            }
        }

        private ActionResult defineErrorReason(Exception ex)
        {
            if (ex is OperationNotAllowedException || ex is AccountNotFoundException)
            {
                return BadRequest();
            }

            return StatusCode(500);
        }
    }
}
