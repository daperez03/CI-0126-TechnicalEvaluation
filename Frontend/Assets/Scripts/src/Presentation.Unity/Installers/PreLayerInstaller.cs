using TechnicalEvaluation.Application.Presenters;
using Zenject;

public class PreLayerInstaller : Installer<PreLayerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICareerUIPresenter>().FromComponentInHierarchy().AsSingle();
    }
}