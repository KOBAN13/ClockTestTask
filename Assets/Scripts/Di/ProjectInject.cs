using Clear;
using Client;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ProjectInject : MonoInstaller
{
    [FormerlySerializedAs("_disposable")] [SerializeField] private Dispose dispose;
    public override void InstallBindings()
    {
        BindSyncTime();
        BindTimeManager();
        BindClockController();
        BindDisposable();
    }

    private void BindDisposable() => Container.BindInterfacesAndSelfTo<Dispose>().FromInstance(dispose).AsSingle().NonLazy();

    private void BindClockController() => Container.BindInterfacesAndSelfTo<ClockController>().AsSingle().NonLazy();

    private void BindTimeManager() => Container.BindInterfacesAndSelfTo<TimeManager>().AsSingle().NonLazy();

    private void BindSyncTime() => Container.BindInterfacesAndSelfTo<SyncClockTime>().AsSingle().NonLazy();
}
