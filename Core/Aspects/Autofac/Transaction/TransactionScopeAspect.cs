using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();//metodu çalıştır burada bir hata gelmezse
                    transactionScope.Complete();//transaction işlemini bitir.
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();//Hata aldıysan işlemi geri al.
                    throw;
                }
            }
        }
    }
}
