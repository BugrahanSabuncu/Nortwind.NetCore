using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
        //Api sistemine Client kadi ve şifre ile giriş yaptı.
        //Eğer şifre doğruysa ilgili kullanıcı için veritabanında ilgili claimleri bulup
        //onun için bir jwt üretecek ve onları buraya verecek.
    }
}
