using NUnit.Framework;
using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Presentation.Unity;
using TMPro;
using UnityEngine.TestTools;
using UnityEngine;
using Zenject;
using FluentAssertions;
using System.Collections;
using Frontend.Presentation.Unity;
using System.Linq;

public class UISpecificCareerPresenterTests : ZenjectIntegrationTestFixture
{
    [Inject]
    private readonly ICareerUIPresenter careerUIPresenter;
    private Career sampleCareer;

    private TextMeshProUGUI careerName;
    private TextMeshProUGUI scholarshipValue;
    private GameObject scrollable;

    [SetUp]
    public void SetUp()
    {
        // Configuración común para todas las pruebas
        PreInstall();

        InfLayerInstaller.Install(Container);
        PreLayerInstaller.Install(Container);

        var presenters = new GameObject("Presenters");

        Container.InstantiateComponent<UISpecificCareerPresenter>(presenters);

        // Instala la escena
        PostInstall();

        var presenter = careerUIPresenter as UISpecificCareerPresenter;
        scrollable = new GameObject();
        careerName = new GameObject().AddComponent<TextMeshProUGUI>();
        scholarshipValue = new GameObject().AddComponent<TextMeshProUGUI>();
        
        presenter.careerName = careerName;
        presenter.scholarshipValue = scholarshipValue;
        presenter.scrollable = scrollable;
        
        sampleCareer =  
            new Career(
                CareerName.Create("Test"),
                Percentage.Create(0),
                Scholarship.Create(0)
            );
    }

    [UnityTest]
    public IEnumerator DisplayCareer_GivenZeroContents_Successfully()
    {
        // Act
        careerUIPresenter.Render(sampleCareer);

        // Assert
        careerName.text.Should().Be(sampleCareer.Id.Value);
        scholarshipValue.text.Should().Be(sampleCareer.ScholarshipBudget.Value.ToString());
        scrollable.transform.childCount.Should().Be(0);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DisplayCareer_GivenMultipleContents_Successfully()
    {
        // Arrange
        var content1 = new Content(ContentDescription.Create("Test1"), ContentTypeId.Create("Test"));
        var content2 = new Content(ContentDescription.Create("Test2"), ContentTypeId.Create("Test"));
        var content3 = new Content(ContentDescription.Create("Test3"), ContentTypeId.Create("Test"));
        sampleCareer.AddContent(content1);
        sampleCareer.AddContent(content2);
        sampleCareer.AddContent(content3);

        // Act
        careerUIPresenter.Render(sampleCareer);

        // Assert
        careerName.text.Should().Be(sampleCareer.Id.Value);
        scholarshipValue.text.Should().Be(sampleCareer.ScholarshipBudget.Value.ToString());
        for (var i = 0; i < sampleCareer.Contents.Count; ++i)
        {
            Transform contentTransform = scrollable.transform.GetChild(i);
            var contentRow = contentTransform.GetComponent<ContentRow>();
            var content = sampleCareer.Contents.ElementAt(i);
            contentRow.typeText.text.Should().Be(content.ContentType.Value);
            contentRow.descriptionText.text.Should().Be(content.Id.Value);
        }
        yield return null;
    }
}