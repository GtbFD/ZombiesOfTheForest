using game.movement.interfaces;
using UnityEngine;

namespace game.movement
{
    public class Rightward : IMovement
    {
        public void move(Transform transform)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, (100f * Time.deltaTime), 0);
            }
        }
    }
}