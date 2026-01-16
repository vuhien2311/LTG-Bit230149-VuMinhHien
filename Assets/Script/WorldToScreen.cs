using UnityEngine;

public class WorldToScreen : MonoBehaviour
{
    void Update()
    {
        Vector3 screenPos =
            Camera.main.WorldToScreenPoint(transform.position);

        Debug.Log(screenPos);
    }
}