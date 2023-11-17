using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Animator.SetBool("isWalking", true);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        /*else if (Input.GetKey(KeyCode.A))
        {
            Animator.SetBool("isWalking", true);
            transform.Rotate(-new Vector3(0, 90, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Animator.SetBool("isWalking", true);
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
        }

        else
        {
            Animator.SetBool("isWalking", false);
        }*/
    }
}
