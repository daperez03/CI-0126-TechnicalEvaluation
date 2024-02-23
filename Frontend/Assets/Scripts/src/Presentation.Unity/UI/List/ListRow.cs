using TMPro;
using UnityEngine;

namespace Frontend.Presentation.Unity
{
    public class ListRow : MonoBehaviour
    {
        public TextMeshProUGUI careerNameTextMesh;

        public DetailsButton moreDetailsButton;

        public void AssignCareerName(string careerName)
        {
            careerNameTextMesh.text = careerName;
            moreDetailsButton.careerNameTextMesh = this.careerNameTextMesh;
        }
    }
}
