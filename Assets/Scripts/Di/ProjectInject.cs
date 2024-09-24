using Client;
using Zenject;

public class ProjectInject : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSyncTime();
        BindTimeManager();
        BindClockController();
    }

    private void BindClockController() => Container.BindInterfacesAndSelfTo<ClockController>().AsSingle().NonLazy();

    private void BindTimeManager() => Container.BindInterfacesAndSelfTo<TimeManager>().AsSingle().NonLazy();

    private void BindSyncTime() => Container.BindInterfacesAndSelfTo<SyncClockTime>().AsSingle().NonLazy();
}
