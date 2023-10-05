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
    public class CacheAspect:MethodInterception
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
            //burada metodun ismini almak için reflectedtype kısmında metodun namespacesini ve classın ismini alıyoruz ve sona metodun adını alıyoruz
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList(); //metodun parametrelerini alıyoruz
            //çift soru işareti x?.ToString diye bir eleman varsa onu yoksa null döndür anlamına gelir
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//parametre varsa parametreleri döner key oluşturur
            if (_cacheManager.IsAdd(key))//böyle bir key varmı onu araştırır
            {
                invocation.ReturnValue = _cacheManager.Get(key);//return value metodun çalışmasını engeller cachedeki değeri dönderir(return değerini değiştirdik)
                return;
            }
            invocation.Proceed();//eğer cachede değer yoksa metodu çalıştırır veri tabanından kodları getirir
            _cacheManager.Add(key, invocation.ReturnValue, _duration);//sonra bu metod cachede olmadığı için cacheye ekleriz
        }
    }
}
