using UnityEngine;

public class FindDirectionalLights : MonoBehaviour
{
    void Start()
    {
        // Get all Light components in the scene
        Light[] allLights = FindObjectsOfType<Light>();

        // Loop through each light and check if it's a directional light
        foreach (Light light in allLights)
        {
            if (light.type == LightType.Directional)
            {
                Debug.Log("Found a directional light: " + light.name);
            }
        }
    }
}
