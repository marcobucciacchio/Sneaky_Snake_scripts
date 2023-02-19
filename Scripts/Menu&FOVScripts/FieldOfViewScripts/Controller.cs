using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    /*   public float moveSpeed = 6f;
       Rigidbody rigidbody;
       Camera viewCamera;
       Vector3 velocity;
       // Start is called before the first frame update
       void Start()
       {
           rigidbody = GetComponent<Rigidbody>();
           viewCamera = Camera.main;
       }

       // Update is called once per frame
       void Update()
       {
           Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
           transform.LookAt(mousePos + Vector3.up * transform.position.y);
           velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

       }

       private void FixedUpdate()
       {
           rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
       }*/

    [SerializeField] float Speed = 3.5f;


    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.position += direction * Speed * Time.deltaTime;
        }
    }


}

