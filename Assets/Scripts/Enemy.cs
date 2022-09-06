using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxPos;
    public float MinPos;
    public bool GoingToRight;
    public float Speed;
    public bool Vertical;
    public bool ShouldRotate;
    public bool Inmovile;

    private float otherCorden;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameObject.tag = "Enemy";
        otherCorden = Vertical ? transform.position.x : transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Inmovile)
            return;
        var comparison = Vertical ? transform.position.y : transform.position.x;
        if (comparison >= MaxPos)
        {
            GoingToRight = false;
            if (ShouldRotate)
            {
                Quaternion target = Quaternion.Euler(0,180,0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target,  1);
            }
        }
        if (comparison <= MinPos)
        {
            GoingToRight = true;
            if (ShouldRotate)
            {
                Quaternion target = Quaternion.Euler(0,0,0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target,  1);
            }
        }

        Vector3 move = new Vector3(0, 0, 0);
        if (Vertical)
            move.y = GoingToRight ? 1 : -1;
        else
            move.x = GoingToRight ? 1 : -1;

        if (Vertical && transform.position.x != otherCorden)
        {
            move.x = (transform.position.x - otherCorden) * -1;
        }
        else if (!Vertical && transform.position.y != otherCorden)
        {
            move.y = (transform.position.y - otherCorden) * -1;
        }

        var magnitude = Mathf.Clamp01(move.magnitude) * Speed;
        move.Normalize();

        var velocity = move * magnitude;

        controller.Move(velocity * Time.deltaTime);
    }
}
