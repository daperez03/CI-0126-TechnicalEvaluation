using Frontend.Presentation.Unity;
using System.Collections;
using System.Collections.Generic;
using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Domain.CareerAggregate;
using TMPro;
using UnityEngine;

namespace TechnicalEvaluation.Presentation.Unity
{

    public class UISpecificCareerPresenter : MonoBehaviour, ICareerUIPresenter
    {
        public TextMeshProUGUI careerName;
        public TextMeshProUGUI scholarshipValue;
        public GameObject scrollable;

        public void Render(Career career)
        {
            if (careerName is not null)
                careerName.text = career.Id.Value;

            if (scholarshipValue is not null)
                scholarshipValue.text = $"${career.ScholarshipBudget.Value.ToString()}";

            foreach(var content in career.Contents)
            {
                GameObject listRow = ContentRowGenerator.Create(content.ContentType.Value, content.Id.Value);
                if (scrollable is null)
                {
                    Debug.LogError("There's no scrollable object assigned in CareerManager, exiting.");
                    return;
                }
                listRow.transform.SetParent(scrollable.transform);
            }


        }

        public void ClearContents()
        {
            foreach (Transform child in scrollable.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
}
