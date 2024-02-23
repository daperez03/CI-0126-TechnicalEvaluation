using Frontend.Presentation.Unity;
using UnityEngine;

public class ListRowGenerator : MonoBehaviour
{
    public static GameObject Create(string description)
    {
        GameObject listItemPrefab = Resources.Load<GameObject>("Prefabs/UpdatedListRow");
        if (listItemPrefab is null)
        {
            Debug.LogError("ListItem prefab was not found.");
            return null;
        }

        GameObject listItem = Instantiate(listItemPrefab);
        if (listItem is null)
        {
            Debug.LogError("An error occurred when instantiating a list item.");
            return null;
        }
        Vector3 currentPosition = listItem.transform.position;
        currentPosition.z = 0f;
        listItem.transform.localPosition = currentPosition;

        ListRow listItemComponent = listItem.GetComponent<ListRow>();
        if (listItemComponent is null)
        {
            Debug.LogError("The list item doesn't have the component ListItem.");
            return null;
        }
        listItemComponent.AssignCareerName(description);

        
        listItem.transform.localPosition = currentPosition;

        return listItem;
    }
}
