using MWTest;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameDataService>()
            .To<GameDataService>()
            .FromNewComponentOnRoot()
            .AsSingle()
            .NonLazy();

        Container.Bind<ILocalSaveService>()
            .To<LocalFileSaveService>()
            .FromNewComponentOnRoot()
            .AsSingle()
            .NonLazy();

        Container.Bind<IResourceProvider>()
            .To<BundleResourceProvider>()
            .FromNewComponentOnRoot()
            .AsSingle()
            .NonLazy();
    }
}