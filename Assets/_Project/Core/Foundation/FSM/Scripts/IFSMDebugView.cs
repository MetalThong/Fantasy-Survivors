using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Foundation.FSM
{
    public interface IFsmDebugView
    {   
        GameObject Owner { get; }
        Type ContextType { get; }
        IReadOnlyCollection<Type> History { get; }
        string Transitions { get; }
    }
}
