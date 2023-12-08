using System;
using UnityEngine;

namespace game.movement.camera
{
    public class RotatePlayer : MonoBehaviour
    {

        public Transform player;
        private float mouseSens = 2f;
        private float cameraVerticalRotation = 0f;

        public GameObject target;
        public void Update()
        {
            var inputX = Input.GetAxis("Mouse X") * mouseSens;
            var inputY = Input.GetAxis("Mouse Y") * mouseSens;

            cameraVerticalRotation -= inputY;
            cameraVerticalRotation = Math.Clamp(cameraVerticalRotation, -90, 90);
            transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
            
            player.Rotate(Vector3.up * inputX);
        }
    }
}