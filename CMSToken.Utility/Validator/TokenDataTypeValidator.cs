using System;

namespace Evolent.Utility.Validator
{
    public class TokenDataTypeValidator : ITokenDataTypeValidator
    {
        /// <summary>
        /// Validate the DataType of Input Token and return True if exists in Stage5 else return False
        /// </summary>
        /// <param name="inputToken"></param>
        /// <returns></returns>
        public bool ValidateTokenDataType(string inputToken)
        {
            if (!string.IsNullOrEmpty(inputToken))
            {
                inputToken = inputToken.ToLower().Trim();
                var allowedDataTypes = System.Configuration.ConfigurationManager.AppSettings["CMSAllowedTokenDataType"].Split(',');
                var result = Array.Exists(allowedDataTypes, s => s.ToLower().Trim().Equals(inputToken));
                return result;
            }
            return false;
        }
    }
}