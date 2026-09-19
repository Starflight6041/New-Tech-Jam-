using System;
using UnityEngine;

public class circle_spinnner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    float accel_timer = 0.0f;
    float timer_length = 5.0f;

    // Update is called once per frame
    void Update()
    {
        spin();
        //spin_speed += 10 * Time.deltaTime;
        accel_timer += Time.deltaTime;

        if (accel_timer >= timer_length) {
            print("accel timer timeout: speeding up");
            spin_speed += 5.0f;
            accel_timer = 0.0f;
        }
    }

    public float spin_speed = 15f;

    void spin()
    {
        transform.Rotate(0f, 0f, -spin_speed * Time.deltaTime);
        //if (transform.rotation.z >= 360.0f)
        //{
        //    transform.rotation.z = 0.0f;
        //}
    }
}
