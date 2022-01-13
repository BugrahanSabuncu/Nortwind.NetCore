using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Cach
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //Burada key oluşturuyoruz.
            //reflectedType Namespace+interface verir.Örneğin : Bussines.Abstract.IProductService
            var arguments = invocation.Arguments.ToList(); //Metodun parametrelerini listeye çevir.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//? varsa o değeri ekle ?? yoksa null ekle
            //key kısmında yapılan işlem ise yukarıdaki işlemim devamı niteliğinde burada metoda ulaşıp parametreleri giriyoruz.
            //örneğin : Bussines/.../IProductService.GetById(int key) parametre yoksada null geçilir.
            if (_cacheManager.IsAdd(key)) //bellekte bu var mı kontrol edilir.
            {
                invocation.ReturnValue = _cacheManager.Get(key); //şayet bellekte varsa metot çalışmadan geri döner.Data _cacheManagerdan alınır
                return;
            }
            invocation.Proceed();// metodu devam ettir.Yani metot çalıştı data geldi.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //gelen data cach'e eklenmiş oldu.
        }
    }
}
