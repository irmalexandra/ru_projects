using System;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class CameraTarget : MonoBehaviour
    {
        public List<Transform> targets;

        

        void LateUpdate()
        {
            float averageX = 0;
            foreach (Transform target in targets)
            {
                
                averageX += target.position.x;
            }

            averageX /= targets.Count;

            transform.position = new Vector3(averageX, transform.position.y, transform.position.z);
        }
    }
}
