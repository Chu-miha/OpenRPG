using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PlayerMovement>()
            .FromComponentsInHierarchy()
            .AsSingle();
        
        Container
            .BindInterfacesTo<PlayerRotation>()
            .FromComponentsInHierarchy()
            .AsSingle();
        
        Container.Bind<Player>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}