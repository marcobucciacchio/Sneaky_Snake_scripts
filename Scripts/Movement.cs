using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Camera cam;
    float rotationSpeed = 10f;
    public float speed = 300f;

    float horizontal;
    float vertical;

    float horizontalRaw;
    float verticalRaw;

    Vector3 targetRotation;
    Rigidbody rb;
    Animator anim;
    Renderer rend;

    bool sneaking = false;
    bool hiding = false;
    bool nearStash = false;
    bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckInput();
    }

    private void Update()
    {
        // toggle sneaking mode
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sneaking = !sneaking;
        }
/*
        // hide the player in stashes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (nearStash)
            {
                hiding = !hiding;
                Debug.Log("player is hiding: " + hiding.ToString());
                CheckInputHiding();
            }
        }*/

    }

    private void CheckInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        horizontalRaw = Input.GetAxisRaw("Horizontal");
        verticalRaw = Input.GetAxisRaw("Vertical");

        Vector3 input = cam.transform.right * horizontal + cam.transform.forward * vertical;
        input.y = 0;
        Vector3 inputRaw = new Vector3(horizontalRaw, 0, verticalRaw);

        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        if (inputRaw.sqrMagnitude > 1f)
        {
            inputRaw.Normalize();
        }

        if (inputRaw != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input).eulerAngles;
        }

        rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation.x, Mathf.Round(targetRotation.y / 45) * 45, targetRotation.z), rotationSpeed * Time.deltaTime);

        // Movement
        Vector3 vel = input * speed * Time.deltaTime;
        rb.velocity = vel;
/*
        // makes sure the player doesn't move while hiding
        if (hiding)
        {
            rb.velocity = Vector3.zero;
        }*/

        bool moving = vel.x > 0 || vel.z > 0 || vel.x < 0 || vel.z < 0;

        // Animation

        // normal walking
        if (moving && !sneaking)
        {
            anim.SetBool("isWalking", true);

            anim.SetBool("isSneaking", false);
            anim.SetBool("isCrouching", false);
        }

        // crouching
        else if (!moving && sneaking)
        {
            anim.SetBool("isCrouching", true);

            anim.SetBool("isWalking", false);
            anim.SetBool("isSneaking", false);
        }

        // sneaking
        else if (moving && sneaking)
        {
            anim.SetBool("isSneaking", true);

            anim.SetBool("isWalking", false);
            anim.SetBool("isCrouching", false);
        }

        // idle
        else if (vel.x == 0 && vel.z == 0)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isSneaking", false);
            anim.SetBool("isCrouching", false);
        }


    }

/*    private void CheckInputHiding()
    {
        if (hiding)
        {
            HidePlayer();
        }
        else
        {
            ShowPlayer();
        }
    }

    private void ShowPlayer()
    {
        Debug.Log("player is not hiding anymore.");
        anim.Play("Idle");
        rend.enabled = true;
    }

    private void HidePlayer()
    {
        Debug.Log("player is hiding.");
        anim.Play("Jump");

        // TODO: disable player's rendering after the animation finishes
        //rend.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered a cube");

        if (other.gameObject.tag == "Stash")
        {
            Debug.Log("Near a stash");
            nearStash = true;
            Debug.Log("near stash: " + nearStash.ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited a cube");
        if (other.gameObject.tag == "Stash")
        {
            Debug.Log("not near a stash anymore");
            nearStash = false;
            Debug.Log("near stash: " + nearStash.ToString());
        }
    }*/
}
