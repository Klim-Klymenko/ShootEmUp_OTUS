using System;
using System.Collections.Generic;
using Atomic.Elements;

namespace GameEngine
{
    [Serializable]
    public sealed class AndExpression : AtomicExpression<bool>
    {
        protected override bool Invoke(IReadOnlyList<IAtomicValue<bool>> members)
        {
            for (int i = 0; i < members.Count; i++)
            {
                if (!members[i].Value)
                    return false;
            }

            return true;
        }
    }
}