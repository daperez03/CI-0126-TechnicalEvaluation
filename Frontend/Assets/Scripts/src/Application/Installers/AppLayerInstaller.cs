using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Application.UseCases;
using Zenject;

public class AppLayerInstaller : Installer<AppLayerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICareerUseCase>().To<CareerUseCase>().AsTransient();
    }
}