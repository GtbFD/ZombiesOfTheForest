using game.movement.interfaces;
using UnityEngine;

namespace game.movement
{
    public class Leftward : IMovement
    {
        public void move(Transform transform)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -(100f * Time.deltaTime), 0);
            }
        }
    }
}