using System;
using System.Collections.Generic;

namespace loja_virtual.Helpers
{
  public class ValidateException : Exception
  {
    public ValidateException() { }
    public ValidateException(string message)
    {
      Errors = new List<string>() { message };
    }
    public IEnumerable<string> Errors { get; set; }
  }
}