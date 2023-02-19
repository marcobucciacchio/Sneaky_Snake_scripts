using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float smoothMoveTime = 0.1f;
    public float turnSpeed = 8;
    public float speedUpTimer = 5f;

    float angle;
    float smoothInputMag;
    float smoothMoveVelocity;
    Vector3 velocity;

    Rigidbody rigidbody;
    bool disabled;

    public Animator animator;

    public GameObject[] enemylist;
    public GameObject puddle;


    private void Start()
    {
        enemylist = GameObject.FindGameObjectsWithTag("enemy");
        rigidbody = GetComponent<Rigidbody>();
        Guard.OnGuardHasSpottedPlayer += Disable;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);  
        }
        Vector3 direction = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0) * new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = direction.magnitude;
        smoothInputMag = Mathf.SmoothDamp(smoothInputMag, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        transform.eulerAngles = Vector3.up * angle;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime * smoothInputMag, Space.World);
            velocity = transform.forward * speed * smoothInputMag;
            animator.SetBool("isWalking", true);
        }
        else
        {
            transform.Translate(transform.forward * speed * Time.deltaTime * smoothInputMag, Space.World);
            velocity = transform.forward * speed * smoothInputMag;
            animator.SetBool("isWalking", false);
        }

        puddle = findPuddle();

        if(findPuddle() != null)
        {
            slowDownEnemy();
        }
        else
        {
            speedUpEnemy();
        }
        if(speed > 7f)
        {
            speedUpTimer -= Time.deltaTime;
        }
        if(speedUpTimer <= 0)
        {
            slowDownPlayer();
            speedUpTimer = 5f;
        }

    }

    private void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }

    void Disable()
    {
        disabled = true;
    }

    private void OnDestroy()
    {
        Guard.OnGuardHasSpottedPlayer -= Disable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            Disable();
        }
    }

    void slowDownEnemy()
    {
        foreach (GameObject enemy in enemylist)
        {
            enemy.GetComponent<Arrive>().maxVelocity = 0f;
        }
    }
    void speedUpEnemy()
    {
        foreach (GameObject enemy in enemylist)
        {
            enemy.GetComponent<Arrive>().maxVelocity = 2f;
        }
    }
    public void speedUpPlayer()
    {
        speed = 10f;
    }
    public void slowDownPlayer()
    {
        speed = 7f;
    }

    public void timing()
    {
        speedUpTimer -= Time.deltaTime;
    }
    GameObject findPuddle()
    {
        puddle =  GameObject.FindGameObjectWithTag("puddle");
        return puddle;
    }

/*    GameObject findSpeed()
    {
        speedup = GameObject.FindGameObjectWithTag("speed");
        return speedup;
    }*/
}
