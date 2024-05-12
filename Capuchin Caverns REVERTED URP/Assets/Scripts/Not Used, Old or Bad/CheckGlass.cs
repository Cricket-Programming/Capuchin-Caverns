using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGlass : MonoBehaviour
{

    private void Start()
    {
        
    }

}

//     void Start()
//     {
//         // Check if the GameObject has a Renderer component
//         Renderer renderer = GetComponent<Renderer>();

//         if (renderer != null)
//         {
//             // Check if the material is named "Glass"
//             if (renderer.material.name.Contains("Glass"))
//             {
//                 // If it's "Glass", add a BoxCollider
//                 AddBoxCollider();
//             }
//             else if (renderer.material.name.Contains("GlassFake"))
//             {
//                 // If it's "GlassFake", remove the BoxCollider
//                 RemoveBoxCollider();
//             }
//         }
//     }

//     void AddBoxCollider()
//     {
//         // Add a BoxCollider if it doesn't exist
//         if (GetComponent<BoxCollider>() == null)
//         {
//             gameObject.AddComponent<BoxCollider>();
//         }
//     }

//     void RemoveBoxCollider()
//     {
//         // Remove the BoxCollider if it exists
//         BoxCollider boxCollider = GetComponent<BoxCollider>();
//         if (boxCollider != null)
//         {
//             Destroy(boxCollider);
//         }
//     }
// }