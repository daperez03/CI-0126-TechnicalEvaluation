using TMPro;
using UnityEngine;

namespace Frontend.Presentation.Unity
{
    public class ContentRow : MonoBehaviour
    {
        public TextMeshProUGUI typeText;
        public TextMeshProUGUI descriptionText;

        public void AssignValues(string typeText, string descriptionText)
        {
            this.typeText.text = typeText;
            this.descriptionText.text = descriptionText;
        }
    }
}
