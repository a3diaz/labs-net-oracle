using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Application.Extensions
{
    public static class StringExtensions
    {
        public static string MaskPhoneNumber(this string number)
        {
            return number.Substring(number.Length - 5, 4);
        }
    }
}
