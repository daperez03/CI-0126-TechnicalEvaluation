using System.Collections;
using System.Collections.Generic;
using TechnicalEvaluation.Presentation.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DetailsButton : MonoBehaviour
{
    public TextMeshProUGUI careerNameTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (careerNameTextMesh is null)
        {
            Debug.LogError("DetailButton's career name is null.");
            return;
        }
        UIManager.Instance.ChangeToUIScene("SpecificCareer", StartDisplay);
    }

    private void StartDisplay()
    {
        var careerManager = FindAnyObjectByType<CareerManager>();
        if (careerManager != null)
        {
            careerManager.DisplayCareer(careerNameTextMesh.text);
        }
        else
        {
            Debug.LogError("No CareerManager was found to load the career.");
        }
    }
}
