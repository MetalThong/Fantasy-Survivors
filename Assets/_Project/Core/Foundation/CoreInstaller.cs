using System.Collections;
using System.Collections.Generic;
using Core.Foundation.Events;
using Reflex.Core;
using UnityEngine;

namespace Core.Foundation
{
    public class CoreInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(new EventBus(), typeof(IEventBus));
        }
    }
}
