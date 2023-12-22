using System;
using System.Reflection;

namespace ShootEmUp
{
    public sealed class DependencyInjector
    {
        public void Inject(object target, ServiceLocator serviceLocator)
        {
            Type targetType = target.GetType();
            MethodInfo[] methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];
                if (method.IsDefined(typeof(InjectAttribute)))
                    InvokeConstruct(target, method, serviceLocator);
            } 
        }

        private void InvokeConstruct(object target, MethodInfo method, ServiceLocator serviceLocator)
        {
            ParameterInfo[] parameters = method.GetParameters();
            int parametersLength = parameters.Length;

            object[] arguments = new object[parametersLength];

            for (int i = 0; i < parametersLength; i++)
            {
                ParameterInfo parameter = parameters[i];
                Type parameterType = parameter.ParameterType;
                arguments[i] = serviceLocator.GetService(parameterType);
            }

            method.Invoke(target, arguments);
        }
    }
}