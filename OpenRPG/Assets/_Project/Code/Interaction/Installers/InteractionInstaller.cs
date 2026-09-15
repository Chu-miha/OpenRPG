using UnityEngine;
using Zenject;

public class InteractionInstaller :  MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InteractionController>()
            .AsSingle();
    }
}
