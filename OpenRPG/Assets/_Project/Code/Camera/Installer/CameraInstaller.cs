using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<CameraTarget>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container
            .BindInterfacesAndSelfTo<FirstPersonCamera>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container
            .BindInterfacesAndSelfTo<ThirdPersonCamera>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container
            .BindInstance(CameraModeType.ThirdPerson);
        
        
        Container
            .BindInterfacesAndSelfTo<CameraController>()
            .AsSingle()
            .NonLazy();;
        
        Container
            .Bind<IMovementDirection>()
            .To<CameraMovementDirection>()
            .AsSingle();
        
    }
}