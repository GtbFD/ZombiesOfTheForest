using game.movement.interfaces;
using UnityEngine;

namespace game.movement
{
    public class Backward : IMovement
    {
        public void move(Transform transform)
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))
                                    * 1.5f * Time.deltaTime);
            }
        }
    }
}