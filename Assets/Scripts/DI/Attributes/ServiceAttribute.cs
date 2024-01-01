using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace ShootEmUp
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ServiceAttribute : Attribute
    {
        public readonly IEnumerable<Type> InterfacesType;

        public ServiceAttribute(params Type[] interfacesType)
        {
            InterfacesType = interfacesType;
        }
    }
}