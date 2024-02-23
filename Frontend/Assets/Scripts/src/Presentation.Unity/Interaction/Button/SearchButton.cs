using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField careerInputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search()
    {
        if (careerInputField is null)
        {
            Debug.LogError("Search career's input field is null.");
        }
        else
        {
            string careerName = careerInputField.text;
            Debug.Log($"Searching for {careerName}...");
            CareerManager careerManager = FindAnyObjectByType<CareerManager>();
            if (careerManager is null)
            {
                Debug.LogError("No CareerManager was found.");
                return;
            }

            careerManager.DisplayCareers(careerName);
        }
    }
}
