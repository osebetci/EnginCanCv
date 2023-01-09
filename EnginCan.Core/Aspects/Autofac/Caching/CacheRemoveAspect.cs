using Castle.DynamicProxy;
using EnginCan.Caching;
using EnginCan.Core.Helpers.Interceptors;
using EnginCan.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace EnginCan.Aspects.Autofac.Caching
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
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
