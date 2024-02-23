using TechnicalEvaluation.Presentation.Unity;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(string destinationSceneName)
    {
        if (destinationSceneName == null)
        {
            Debug.LogError("BackButton's destination scene name is null.");
            return;
        }
        UIManager.Instance.ChangeToUIScene(destinationSceneName);
    }
}
