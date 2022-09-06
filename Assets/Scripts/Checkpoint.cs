using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Parameters.Dificulty CheckPointType;
    public AudioSource SoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        if (Parameters.GameMode > CheckPointType)
        {
            Destroy(this.gameObject);
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SoundEffect.Play();
            Destroy(this.gameObject);
            enabled = false;
        }
    }
}
