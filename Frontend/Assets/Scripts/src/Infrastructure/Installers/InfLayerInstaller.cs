using System.Net.Http;
using TechnicalEvaluation.Application.Repositories;
using TechnicalEvaluation.Infrastructure;
using TechnicalEvaluation.Infrastructure.Repositories;
using Zenject;

public class InfLayerInstaller : Installer<InfLayerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ApiClient>().ToSelf().AsTransient()
            .WithArguments("https://localhost:7245", new HttpClient());
        Container.Bind<ICareerRepository>().To<CareerRepository>().AsTransient();
    }
}