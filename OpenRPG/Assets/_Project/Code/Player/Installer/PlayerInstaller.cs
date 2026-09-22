using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerDataStat playerDataStat;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PlayerMovement>()
            .FromComponentsInHierarchy()
            .AsSingle();
        
        Container
            .BindInterfacesTo<PlayerRotation>()
            .FromComponentsInHierarchy()
            .AsSingle();
        
        Container.Bind<PlayerStats>()
            .AsSingle()
            .WithArguments(playerDataStat);
        
        Container.Bind<Player>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container
            .Bind<FirstPersonInteractionDetector>()
            .AsSingle();

        Container
            .Bind<ThirdPersonInteractionDetector>()
            .AsSingle();

        Container
            .Bind<IInteractionDetector>()
            .To<PlayerInteractionDetector>()
            .AsSingle();
        
        Container.Bind<IInventoryFactory>()
            .To<InventoryFactory>()
            .AsSingle();
        
        Container
            .Bind<PlayerInventory>()
            .AsSingle();
        
        Container
            .Bind<PlayerInteraction>()
            .AsSingle();
        
    }
}