using NUnit.Framework;
using UnityEngine.UIElements;
using UnityEngine;
using Zenject;
using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Domain.CareerAggregate;
using System.Collections.Generic;
using TechnicalEvaluation.Presentation.Unity;
using FluentAssertions;
using System.Collections;
using UnityEngine.TestTools;
using TMPro;
using Frontend.Presentation.Unity;

public class UICareerSearchPresenterTests : ZenjectIntegrationTestFixture
{
    [Inject]
    private readonly ICareerUIPresenter careerUIPresenter;
    private List<Career> sampleCareers;

    private TextMeshProUGUI careerName;
    private GameObject simulatedScroll;


    [SetUp]
    public void SetUp()
    {
        // Configuración común para todas las pruebas
        PreInstall();

        InfLayerInstaller.Install(Container);
        PreLayerInstaller.Install(Container);

        var presenters = new GameObject("Presenters");

        Container.InstantiateComponent<UICareerSearchPresenter>(presenters);

        // Instala la escena
        PostInstall();

        var presenter = careerUIPresenter as UICareerSearchPresenter;
        
        simulatedScroll = new GameObject("Scrollable");
        presenter.scrollable = simulatedScroll;

        sampleCareers = new List<Career>();
    }

    [UnityTest]
    public IEnumerator DisplayCareers_GivenOneCareer_Successfully()
    {
        // Arrange
        var sampleCareer =
            new Career(
                    CareerName.Create("Test1"), Percentage.Create(0), Scholarship.Create(0)
                );

        // Act    
        careerUIPresenter.Render(sampleCareer);
        
        // Assert
        Transform career = simulatedScroll.transform.GetChild(0);
        var careerName = career.GetComponent<ListRow>().careerNameTextMesh.text;
        careerName.Should().Be(sampleCareer.Id.Value);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DisplayCareers_GivenMultipleCareers_Successfully()
    {
        // Arrange
        sampleCareers.Add(
            new Career(
                    CareerName.Create("Test1"), Percentage.Create(0), Scholarship.Create(0)
                )
        );
        sampleCareers.Add(
            new Career(
                    CareerName.Create("Test2"), Percentage.Create(0), Scholarship.Create(0)
                )
        );
        sampleCareers.Add(
            new Career(
                    CareerName.Create("Test3"), Percentage.Create(0), Scholarship.Create(0)
                )
        );

        // Act
        foreach (var career in sampleCareers)
        {
            careerUIPresenter.Render(career);
        }

        // Assert
        for (var i = 0; i < sampleCareers.Count; ++i)
        {
            Transform career = simulatedScroll.transform.GetChild(i);
            var careerName = career.GetComponent<ListRow>().careerNameTextMesh.text;
            careerName.Should().Be(sampleCareers[i].Id.Value);
        }
        yield return null;
    }
}