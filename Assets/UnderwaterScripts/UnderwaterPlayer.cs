using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterPlayer : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }
}
