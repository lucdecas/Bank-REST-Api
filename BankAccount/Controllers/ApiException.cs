using System;

namespace BankAccount.Controllers
{
  public class ApiException : Exception {
      public int ErrorCode { get; set; }

      public Object ErrorContent { get; private set; }

      public ApiException() {}

      public ApiException(int errorCode, string message) : base(message) {
          this.ErrorCode = errorCode;
      }

      public ApiException(int errorCode, string message, Object errorContent = null) : base(message) {
          this.ErrorCode = errorCode;
          this.ErrorContent = errorContent;
      }
  }
}
