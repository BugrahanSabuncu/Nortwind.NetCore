using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace Core.Aspects.Autofac.Cach
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
            //Bu metodun içinde silmemizin nedeni data bozulursa cach temizleme yapacağımız için
            //örneğin bir ekleme işlemi başarılı olmadıysa cach silme gibi bir işlem yapmak yerine
            //işlem başarılı ise bunu çalıştırmaktır.
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
