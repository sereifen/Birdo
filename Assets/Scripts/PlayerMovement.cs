using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator squirrel;
    public float Speed;
    public float JumpSpeed;
    public int MaxJumps;
    public GameObject GameOverText;
    public GameObject ToCheckPointText;
    public float MaxTimeInmovile;    
    public AudioSource JumpSoundEffect;
    public AudioSource DieSoundEffect;

    private CharacterController controller;
    private float ySpeed;
    private int jumps;
    private Vector3 checkPoint;
    private float timeInmovile;
    private bool inRope;
    private bool goingRight;
    private float deffStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        squirrel = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        gameObject.tag = "Player";
        GameOverText.SetActive(false);
        ToCheckPointText.SetActive(false);
        checkPoint = transform.position;
        timeInmovile = 0;
        inRope = false;
        goingRight = true;
        deffStepOffset = controller.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationVariables();
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
        if (timeInmovile > 0.0f)
        {
            timeInmovile -= Time.deltaTime;
            if (timeInmovile <= 0.0f)
            {
                GameOverText.SetActive(false);
                ToCheckPointText.SetActive(false);
            }
            return;
        }
        var horizontal = Input.GetAxis("Horizontal");
        Quaternion target = Quaternion.Euler(0,goingRight?90:-90,0);
        if (horizontal>0.1f)
        {
            target = Quaternion.Euler(0,90,0);
            goingRight = true;
        }
        else if (horizontal<-0.1f)
        {
            target = Quaternion.Euler(0,-90,0);
            goingRight = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  1);
        Vector3 move = new Vector3(horizontal, 0, 0);

        if (inRope)
        {
            var vertical = Input.GetAxis("Vertical");
            move.y = vertical;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-90,0,0),  1);
        }

        var magnitude = Mathf.Clamp01(move.magnitude) * Speed;
        move.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (IsGrounded() || inRope)
        {
            ySpeed = -0.5f;
            jumps = MaxJumps;
            controller.stepOffset = deffStepOffset;
        }
        if (jumps > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                controller.stepOffset = 0;
                jumps--;
                ySpeed = JumpSpeed;
                JumpSoundEffect.Play();
            }
        }

        var velocity = move * magnitude;        
        velocity.y += ySpeed;

        controller.Move(velocity * Time.deltaTime);
        if (transform.position.y + 2 < checkPoint.y)
        {
            MoveToCheckPoint();
            ToCheckPointText.SetActive(true);
        }
    }

    void MoveToCheckPoint()
    {
        controller.enabled = false;
        transform.position = checkPoint;
        controller.enabled = true;
        timeInmovile = MaxTimeInmovile;
    }

    void AnimationVariables()
    {
        if (squirrel.GetCurrentAnimatorStateInfo(0).IsName("idle") && IsGrounded())
        {
            squirrel.SetBool("jump", false);
        } 
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    squirrel.SetBool("jump", true);
        //    squirrel.SetBool("idle", false);
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            squirrel.SetBool("run", true);
            squirrel.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            squirrel.SetBool("run", false);
            squirrel.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            squirrel.SetBool("run", true);
            squirrel.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            squirrel.SetBool("run", false);
            squirrel.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.W) && inRope)
        {
            squirrel.SetBool("idle", false);
            squirrel.SetBool("run", true);
        }
        if (Input.GetKeyUp(KeyCode.W) && inRope)
        {
            squirrel.SetBool("idle", true);
            squirrel.SetBool("run", false);
        }
        if (Input.GetKeyDown("down") && inRope)
        {
            squirrel.SetBool("idle", false);
            squirrel.SetBool("run", true);
        }
        if (Input.GetKeyUp("down") && inRope)
        {
            squirrel.SetBool("idle", true);
            squirrel.SetBool("run", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Spit")
        {
            DieSoundEffect.Play();
            GameOverText.SetActive(true);
            MoveToCheckPoint();
            inRope = false;
        }
        else if (other.tag == "CheckPoint")
        {
            checkPoint = transform.position;
        }
        else if (other.tag == "Rope")
        {
            inRope = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rope")
        {
            inRope = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.02f );
    }
}
