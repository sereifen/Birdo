using UnityEngine;
using System.Collections;

public class Squirrel : MonoBehaviour {

    Animator squirrel;
    private bool Speed1 = true;
    private bool Speed2 = false;
    public float gravity = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    // Use this for initialization
    void Start () {
        squirrel = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(moveDirection * Time.deltaTime);
        moveDirection.y = gravity * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Speed1 = !Speed1;
            Speed2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Speed2 = !Speed2;
            Speed1 = false;
        }
        if (squirrel.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            squirrel.SetBool("jump", false);
            squirrel.SetBool("up", false);
            squirrel.SetBool("down", false);
        }
        if (squirrel.GetCurrentAnimatorStateInfo(0).IsName("stand"))
        {
            squirrel.SetBool("eat", false);
        }
        if (Input.GetKeyDown(KeyCode.W)&& (Speed1 == true))
        {
            squirrel.SetBool("idle", false);
            squirrel.SetBool("walk", true);
        }
        if (Input.GetKeyDown(KeyCode.W) && (Speed2 == true))
        {
            squirrel.SetBool("idle", false);
            squirrel.SetBool("run", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            squirrel.SetBool("walk", false);
            squirrel.SetBool("run", false);
            squirrel.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.A)&&(Speed1==true))
        {
            squirrel.SetBool("left", true);
            squirrel.SetBool("idle", false);
            squirrel.SetBool("walk", false);
        }
        if (Input.GetKeyDown(KeyCode.A) && (Speed2 == true))
        {
            squirrel.SetBool("runleft", true);
            squirrel.SetBool("run", false);
            squirrel.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            squirrel.SetBool("runleft", false);
            squirrel.SetBool("left", false);
            squirrel.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.D) && (Speed1 == true))
        {
            squirrel.SetBool("right", true);
            squirrel.SetBool("idle", false);
            squirrel.SetBool("walk", false);
        }
        if (Input.GetKeyDown(KeyCode.D) && (Speed2 == true))
        {
            squirrel.SetBool("runright", true);
            squirrel.SetBool("run", false);
            squirrel.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            squirrel.SetBool("runright", false);
            squirrel.SetBool("right", false);
            squirrel.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            squirrel.SetBool("jump", true);
            squirrel.SetBool("idle", false);
        }
        if (Input.GetKeyDown("up"))
        {
            squirrel.SetBool("up", true);
            squirrel.SetBool("idle", false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            squirrel.SetBool("stand", false);
            squirrel.SetBool("eat", true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            squirrel.SetBool("eat", false);
            squirrel.SetBool("stand", true);
        }
        if (Input.GetKeyDown("down"))
        {
            squirrel.SetBool("down", true);
            squirrel.SetBool("stand", false);
        }
    }

}
