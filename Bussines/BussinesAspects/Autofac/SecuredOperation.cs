using Castle.DynamicProxy;

using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using Core.Extensions;
using Bussines.Constants;
using Core.Utilities.IoC;

namespace Bussines.BussinesAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; //her client için bir context yaratıyor.Araştır

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');            
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            // Autofac'te olan injectionları burada çekip kullanabiliyoruz.GetService içine yazılanın karşılığı döner.

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
