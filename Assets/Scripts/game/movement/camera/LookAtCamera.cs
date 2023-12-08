using System;
using UnityEngine;

namespace game.movement.camera
{
    public class LookAtCamera : MonoBehaviour
    {

        public GameObject target;
        public void LateUpdate()
        {
            transform.LookAt(target.transform);
        }
    }
}