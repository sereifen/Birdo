using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float DashSpeed;
    public float MaxDashTime;
    public float DashDistance;
    public float DashStoppingSpeed;
    public float CurrentDashTime;
    public float DashDelay;
    public float MaxDashDelay;
    public AudioSource DashSoundEffect;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);
        if (DashDelay > 0.0)
        {
            DashDelay -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (DashDelay <= 0.0)
            {
                DashSoundEffect.Play();
                CurrentDashTime = 0;
                DashDelay = MaxDashDelay;
            }
        }
        if (CurrentDashTime < MaxDashTime)
        {
            move = new Vector3(Input.GetAxis("Horizontal") * DashDistance, 0, 0);
            CurrentDashTime += DashStoppingSpeed;
        }
        else
        {
            move = Vector3.zero;
        }
        controller.Move(move * Time.deltaTime * DashSpeed);
    }
}
