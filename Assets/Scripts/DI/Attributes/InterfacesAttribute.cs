using System;
using System.Collections.Generic;
using JetBrains.Annotations;
namespace ShootEmUp
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InterfacesAttribute : Attribute
    {
        public readonly IEnumerable<Type> InterfacesType;

        public InterfacesAttribute(params Type[] interfacesType)
        {
            InterfacesType = interfacesType;
        }
    }
}