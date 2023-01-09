using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EnginCan.Core.Helpers.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        //public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        //{
        //    var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
        //        (true).ToList();
        //    var methodAttributes = type.GetMethod(method.Name, new[] { typeof(string) });

        //    if (methodAttributes != null)
        //    {
        //        var methodAttributes2 = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
        //        classAttributes.AddRange(methodAttributes2);
        //    }


        //    return classAttributes.OrderBy(x => x.Priority).ToArray();
        //}

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
