using System;
using JetBrains.Annotations;

namespace ShootEmUp
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute { }
}