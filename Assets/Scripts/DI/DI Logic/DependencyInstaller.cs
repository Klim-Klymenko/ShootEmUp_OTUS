using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ShootEmUp
{
    public abstract class DependencyInstaller : MonoBehaviour
    {
    public virtual IEnumerable<object> ProvideServices()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                 BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].IsDefined(typeof(ServiceAttribute)))
            {
                object value = fields[i].GetValue(this);

                if (value == null)
                    throw new NullReferenceException($"Field {fields[i].Name} is null");

                yield return value;
            }
        }
    }

    public virtual IEnumerable<object> ProvideInjectables()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                 BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        for (int i = 0; i < fields.Length; i++)
        {
            object value = fields[i].GetValue(this);

            if (value == null)
                throw new NullReferenceException($"Field {fields[i].Name} is null");

            yield return value;
        }
    }

    public virtual IEnumerable<IGameListener> ProvideGameListeners()
    {
        FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                 BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].IsDefined(typeof(ListenerAttribute)))
            {
                object value = fields[i].GetValue(this);

                if (value == null)
                    throw new NullReferenceException($"Field {fields[i].Name} is null");

                if (value is not IGameListener listener)
                    throw new NullReferenceException($"Field {fields[i].Name} is not IGameListener");

                yield return listener;
            }
        }
    }
    }
}