using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using TechnicalEvaluation.Presentation.Unity;
using Moq;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using Zenject;
using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Infrastructure.Repositories;

public class CareerManagerSpecificCareerUITests : ZenjectIntegrationTestFixture
{
    private TMP_InputField inputFieldComponent;
    private GameObject simulatedScroll;
    private UISpecificCareerPresenter uiSpecificCareerPresenter;
    private SearchButton searchButton;
    private Mock<ICareerUseCase> careerUseCaseMock;
    private GameObject careerManagerGameObject;
    private CareerManager careerManager;
    private GameObject presenters;

    private Career career1;
    private Career career2;
    private Career career3;

    private Content content1;
    private Content content2;
    private Content content3;
    private Content content4;

    [Inject]
    ICareerUIPresenter _careerUIPresenter;

    [SetUp]
    public void SetUp()
    {
        PreInstall();
        InfLayerInstaller.Install(Container);
        PreLayerInstaller.Install(Container);

        careerManagerGameObject = new GameObject("CareerManager");
        var careerPresenterGameObject = new GameObject("CareerPresenter");

        uiSpecificCareerPresenter = Container
            .InstantiateComponent<UISpecificCareerPresenter>(careerPresenterGameObject);

        CreateObjects();

        // Create the career use case mock.
        careerUseCaseMock = new Mock<CareerUseCase>().As<ICareerUseCase>();

        careerUseCaseMock
            .Setup(useCase => useCase.ShowCareers(It.IsAny<List<Career>>()))
            .Callback((List<Career> careers) =>
            {
                Debug.Log("Starting show careers implemented.");
                if (_careerUIPresenter is not null)
                {
                    _careerUIPresenter.ClearContents();
                    foreach (var career in careers)
                    {
                        _careerUIPresenter.Render(career);
                    }
                }
                else
                {
                    Debug.LogError("The CareerUIPresenter was not found!");
                }

            });

        Container.Bind<ICareerUseCase>()
            .FromInstance(careerUseCaseMock.Object);

        PostInstall();
    }

    private void CreateObjects()
    {
        // Create SearchButton's TMP_InputField
        inputFieldComponent = new GameObject("CareerInput")
            .AddComponent<TMP_InputField>(); // not needed?
        inputFieldComponent.text = "Comp";

        // Create SearchButton
        searchButton = new GameObject("SearchButton")
            .AddComponent<SearchButton>(); // not needed?

        // Create simulated scroll gameobject.
        simulatedScroll = new GameObject("Scrollable");

        // Assign the scrollable to the presenter.
        Assert.That(simulatedScroll is not null, "Simulated scroll is null");

        uiSpecificCareerPresenter.scrollable = simulatedScroll;


        // Create careers for later addition.
        career1 = new Career(
            CareerName.Create("Career1"),
            Percentage.Create(0.5f),
            Scholarship.Create(200f)
            );

        career2 = new Career(
            CareerName.Create("Career2"),
            Percentage.Create(0.4f),
            Scholarship.Create(200f)
            );

        career3 = new Career(
            CareerName.Create("Career3"),
            Percentage.Create(0.8f),
            Scholarship.Create(200f)
            );

        content1 = new Content(
                ContentDescription.Create("Desc"),
                ContentTypeId.Create("Tecnologico")
            );

        content2 = new Content(
                ContentDescription.Create("Desc2"),
                ContentTypeId.Create("Ambiental")
            );

        content3 = new Content(
                ContentDescription.Create("Desc3"),
                ContentTypeId.Create("Social")
            );

        content4 = new Content(
                ContentDescription.Create("Desc4"),
                ContentTypeId.Create("Social")
            );



    }


    [UnityTest]
    public IEnumerator DisplayCareer_GivenCareerWithNoContents_DoesNotFillScrollable()
    {
        // Arrange
        careerUseCaseMock
            .Setup(useCase => useCase.GetCareerByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(career1);

        Debug.Log("Started Test");
        var careerManage = Container.InstantiateComponent<CareerManager>(careerManagerGameObject);

        // Act
        yield return null;
        Assert.That(careerManage is not null, "The career manager is null.");
        Debug.Log("Starting display careers");
        yield return careerManage.DisplayCareer("");


        // Assert
        Debug.Log($"Count: {simulatedScroll.transform.childCount}");

        Assert.That(simulatedScroll.transform.childCount == 0);
    }




    [UnityTest]
    public IEnumerator DisplayCareer_GivenOneContent_FillsOneRow()
    {
        // Arrange
        var tempCareer = career1;
        tempCareer.AddContent(content1 );

        careerUseCaseMock
            .Setup(useCase => useCase.GetCareerByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(tempCareer);

        Debug.Log("Started Test");
        var careerManage = Container.InstantiateComponent<CareerManager>(careerManagerGameObject);

        // Act
        yield return null;
        Assert.That(careerManage is not null, "The career manager is null.");
        Debug.Log("Starting display careers");
        yield return careerManage.DisplayCareer("");


        // Assert
        Debug.Log($"Count: {simulatedScroll.transform.childCount}");

        Assert.That(simulatedScroll.transform.childCount == 1);
    }


    [UnityTest]
    public IEnumerator DisplayCareer_GivenMultipleContents_FillsTable()
    {
        // Arrange
        var tempCareer = career1;
        tempCareer.AddContent(content1);
        tempCareer.AddContent(content2);
        tempCareer.AddContent(content3);
        tempCareer.AddContent(content4);

        careerUseCaseMock
            .Setup(useCase => useCase.GetCareerByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(tempCareer);

        Debug.Log("Started Test");
        var careerManage = Container.InstantiateComponent<CareerManager>(careerManagerGameObject);

        // Act
        yield return null;
        Assert.That(careerManage is not null, "The career manager is null.");
        Debug.Log("Starting display careers");
        yield return careerManage.DisplayCareer("");


        // Assert
        Debug.Log($"Count: {simulatedScroll.transform.childCount}");

        Assert.That(simulatedScroll.transform.childCount == 4);
    }

}
