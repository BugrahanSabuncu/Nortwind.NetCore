using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            //bu yapı appsetting içinde bulunan securityKey burada byteArray haline gelir.
            //Byte'ını alarak onu symetrik security key haline dönüştürür.
        }
    }
}
