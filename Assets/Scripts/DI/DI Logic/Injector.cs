using System;
using System.Reflection;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Injector
    {
        internal void Inject(object target, ServiceLocator serviceLocator)
        {
            Type targetType = target.GetType();
            MethodInfo[] methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].IsDefined(typeof(InjectAttribute)))
                    InvokeConstruct(target, methods[i], serviceLocator);
            } 
        }
        
        internal void Inject(object target, ServiceLocator serviceLocator, ServiceLocator parentServiceLocator)
        {
            Type targetType = target.GetType();
            MethodInfo[] methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].IsDefined(typeof(InjectAttribute)))
                    InvokeConstruct(target, methods[i], serviceLocator, parentServiceLocator);
            }
        }

        private void InvokeConstruct(object target, MethodInfo method, ServiceLocator serviceLocator)
        {
            ParameterInfo[] parameters = method.GetParameters();
            int parametersLength = parameters.Length;

            object[] arguments = new object[parametersLength];

            for (int i = 0; i < parametersLength; i++)
            {
                Type parameterType = parameters[i].ParameterType;
                arguments[i] = serviceLocator.GetService(parameterType);
            }
            
            method.Invoke(target, arguments);
        }
        
        private void InvokeConstruct(object target, MethodInfo method, ServiceLocator serviceLocator, ServiceLocator parentServiceLocator)
        {
            ParameterInfo[] parameters = method.GetParameters();
            int parametersLength = parameters.Length;

            object[] arguments = new object[parametersLength];

            for (int i = 0; i < parametersLength; i++)
            {
                Type parameterType = parameters[i].ParameterType;

                if (serviceLocator.TryGetService(parameterType, out object service))
                    arguments[i] = service;
                else 
                    arguments[i] = parentServiceLocator.GetService(parameterType);
            }

            method.Invoke(target, arguments);
        }
    }
}