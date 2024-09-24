using Ui;
using UnityEngine;
using Zenject;

namespace Di
{
    public class UiInject : MonoInstaller
    {
        [SerializeField] private ClockView _clockView;
        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<ClockView>().FromInstance(_clockView).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ClockModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ClockPresenter>().AsSingle().NonLazy();
        }
    }
}