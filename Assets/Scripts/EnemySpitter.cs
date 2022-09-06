using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitter : MonoBehaviour
{
    public Spit Spit;
    public float MaxDelaySpits;
    public Vector3 DirectionSpit;
    public float SpitSpeed;

    private float delayspits;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        delayspits -= Time.deltaTime;
        if (delayspits <= 0)
        {
                delayspits = MaxDelaySpits;
            try
            {
                Spit newspit = Instantiate(Spit, new Vector3(transform.position.x, transform.position.y) + (DirectionSpit * 0.6f), Quaternion.identity);
                newspit.Direction = DirectionSpit;
                newspit.Speed = SpitSpeed;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }
}
