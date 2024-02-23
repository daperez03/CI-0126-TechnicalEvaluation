using System.Collections;
using System.Collections.Generic;
using TechnicalEvaluation.Application.Presenters;
using TechnicalEvaluation.Domain.CareerAggregate;
using UnityEngine;

namespace TechnicalEvaluation.Presentation.Unity
{

    public class UICareerSearchPresenter : MonoBehaviour, ICareerUIPresenter
    {
        public GameObject scrollable;

        public void Render(Career career)
        {
            GameObject listRow = ListRowGenerator.Create(career.Id.Value);
            if (scrollable is null)
            {
                Debug.LogError("There's no scrollable object assigned in CareerManager, exiting.");
                return;
            }
            listRow.transform.SetParent(scrollable.transform);

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
