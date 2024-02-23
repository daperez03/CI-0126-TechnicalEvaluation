using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Domain.CareerAggregate;
using TechnicalEvaluation.Presentation.Unity;
using UnityEngine;
using Zenject;

public class CareerManager : MonoBehaviour
{
    [Inject]
    ICareerUseCase _careerUseCase;

    public async Task DisplayCareers(string nameSearch)
    {
        Debug.Assert(_careerUseCase is not null, "CareerManager: The career use case cannot be null");
        var careers = await _careerUseCase.SearchCareersByNameAsync(nameSearch);
        _careerUseCase?.ShowCareers(careers);
    }



    public async Task DisplayCareer(string nameSearch)
    {
        var career = await _careerUseCase.GetCareerByIdAsync(nameSearch);
        _careerUseCase?.ShowCareers(new List<Career>{ career });
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
