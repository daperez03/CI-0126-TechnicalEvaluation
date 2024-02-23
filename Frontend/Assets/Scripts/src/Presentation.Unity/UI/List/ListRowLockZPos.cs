using UnityEngine;

public class LockZPosition : MonoBehaviour
{
    public float lockedZPosition = 0f; 

    void Update()
    {
        // Get the current position
        Vector3 currentPosition = transform.localPosition;

        // Check if the z position is different from the locked position
        if (currentPosition.z != lockedZPosition)
        {
            Debug.Log($"Setting the z position to the locked one. Before: {currentPosition.z}");
            currentPosition.z = lockedZPosition;
            Debug.Log($"Setting the z position to the locked one. After: {currentPosition.z}");
            transform.position = currentPosition;
        }

        new WaitForSeconds(5);
    }
}
