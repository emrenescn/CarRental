using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        //bu metodlar cachedeki datalar değişirse(add,delete,update) devreye girer
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)//burada OnSuccess metodunu çağırmamızın sebebi eğer ekleme,silme,güncelleme
                                                                 //başarılı olduysa cacheyi değiştirsin diye başarısız ise değiştirmeye gerek yok
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
