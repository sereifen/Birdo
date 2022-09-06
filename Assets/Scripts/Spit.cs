using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    public float Speed;
    public Vector3 Direction;
    public float LimitMaxX = 48;
    public float LimitMinX = -48;
    public float Timeout = 50;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameObject.tag = "Spit";
        Timeout = 50;
    }

    // Update is called once per frame
    void Update()
    {
        var magnitude = Mathf.Clamp01(Direction.magnitude) * Speed;
        Direction.Normalize();

        var velocity = Direction * magnitude;

        controller.Move(velocity * Time.deltaTime);
        Timeout -= Time.deltaTime;

        if (transform.position.x >= LimitMaxX || transform.position.x <= LimitMinX || Timeout <= 0.0f)
        {
            Destroy(this.gameObject);
            enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Platform")
        {
            Destroy(this.gameObject);
            enabled = false;
        }
    }
}
