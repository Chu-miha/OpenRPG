using UnityEngine;
using Zenject;

public class CombatInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<ICombatControllerFactory>()
            .To<CombatControllerFactory>()
            .AsSingle();
    }
}
