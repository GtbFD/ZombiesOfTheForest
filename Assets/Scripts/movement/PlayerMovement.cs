using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [FormerlySerializedAs("Animator")] 
    public Animator animator;

    private GameObject player;

    void Start()
    {

    }
    

    void Update()
    {
        Forward();
        Backward();
        Leftward();
        Rightward();
    }

    public void Forward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isForward", true);
            transform.Translate(Vector3.forward * Time.deltaTime);
            /*playerPosition.x = transform.position.x;
            /playerPosition.y = transform.position.y;*/
        }
        else
        {
            animator.SetBool("isForward", false);
        }
    }

    public void Backward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isBackward", true);
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isBackward", false);
        }
    }

    public void Leftward()
    {
        /*if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalking", true);
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }*/
    }

    public void Rightward()
    {
        /*if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }*/
    }
}
