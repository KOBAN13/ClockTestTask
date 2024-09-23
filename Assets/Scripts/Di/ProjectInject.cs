using Client;
using Zenject;

public class ProjectInject : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSyncTime();
        BindTimeManager();
    }

    private void BindTimeManager() => Container.BindInterfacesAndSelfTo<TimeManager>().AsSingle().NonLazy();

    private void BindSyncTime() => Container.BindInterfacesAndSelfTo<SyncClockTime>().AsSingle().NonLazy();
}
