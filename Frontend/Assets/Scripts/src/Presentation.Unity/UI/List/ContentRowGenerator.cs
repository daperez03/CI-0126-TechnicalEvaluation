using TMPro;
using UnityEngine;

namespace Frontend.Presentation.Unity
{
    public class ContentRowGenerator : MonoBehaviour
    {
        public static GameObject Create(string type, string description)
        {
            GameObject listItemPrefab = Resources.Load<GameObject>("Prefabs/ContentRow");
            if (listItemPrefab is null)
            {
                Debug.LogError("ContentRow prefab was not found.");
                return null;
            }

            GameObject listItem = Instantiate(listItemPrefab);
            if (listItem is null)
            {
                Debug.LogError("An error occurred when instantiating a content item.");
                return null;
            }

            ContentRow listItemComponent = listItem.GetComponent<ContentRow>();
            if (listItemComponent is null)
            {
                Debug.LogError("The list item doesn't have the component ContentRow.");
                return null;
            }
            listItemComponent.AssignValues(type, description);


            return listItem;
        }
    }
}
