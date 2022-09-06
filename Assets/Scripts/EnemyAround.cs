using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAround : MonoBehaviour
{
    public List<Vector3> Positions;
    public float Speed;
    public bool ClockWise;

    private CharacterController controller;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameObject.tag = "Enemy";
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = GetDirection();
        var magnitude = Mathf.Clamp01(direction.magnitude) * Speed;
        direction.Normalize();

        var velocity = direction * magnitude;

        controller.Move(velocity * Time.deltaTime);
        if (ArrivedToPosition())
        {
            Quaternion target = Quaternion.Euler(0,0,90 * (index+1) * (ClockWise?-1:1));
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  1);
            index++;
            if (index >= Positions.Count)
            {
                index = 0;
            }
        }
    }
    bool ArrivedToPosition()
    {
        return Mathf.Abs(transform.position.x - Positions[index].x) <= 0.1 && Mathf.Abs(transform.position.y - Positions[index].y) <= 0.1;
    }

    Vector3 GetDirection()
    {
        return Positions[index]- transform.position;
    }
}
