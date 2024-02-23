using UnityEngine;

namespace TechnicalEvaluation.Presentation.Unity
{
    public class DontDestroyScript : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}