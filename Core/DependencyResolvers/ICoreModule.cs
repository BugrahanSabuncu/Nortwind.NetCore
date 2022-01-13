using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
        //startup tarafında ConfigureServices(IServiceCollection services)
        // kısmında bulunan serviceCollectionları burada vereceğiz 
    }
}
