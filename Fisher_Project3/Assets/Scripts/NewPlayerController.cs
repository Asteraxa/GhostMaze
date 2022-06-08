using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour
{

    CharacterController charCont;

    public GameObject camera;
    public GameObject offset;
    public GameObject basic;

    private Vector3 moveDirection = Vector3.zero;

    public Rigidbody laser;
    public float shootSpeed;

    public float speed = 10f;
    public float dashSpeed = 2f;

    public Slider chargeBar;
    public int maxCharge = 25;
    private int currentCharge;

    public Slider staminaBar;
    public float usageRate;
    public float regenRate;
    public float maxStam;
    public float minStam = 0;
    private float currentStam;

    public ScoreText scoreScript;
    public static int score;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        charCont = GetComponent<CharacterController>(); //Get the player's character controller.
        staminaBar = staminaBar.GetComponent<Slider>();
        chargeBar = chargeBar.GetComponent<Slider>();
        currentStam = maxStam;
        currentCharge = 0;
        chargeBar.value = 0;
        staminaBar.value = maxStam;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButton("Dash"))
        {
            anim.SetBool("isDashing", false);
            camera.transform.position = basic.transform.position;
        }
        PlayerMove();
        if (Input.GetButtonDown("Shoot") && currentCharge > 0)
        {
            StartCoroutine(ShootAnim());
            PlayerShoot();
            currentCharge = 0;  
        }
        staminaBar.value = currentStam;
        chargeBar.value = currentCharge;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pellet")
        {
            score += 1;
            scoreScript.NewScore();
            Destroy(other.gameObject);
            if (currentCharge < maxCharge)
            {
                currentCharge += 1;
                chargeBar.value = currentCharge;
            }
        }
    }
    void PlayerMove()
    {
        //Get player's input for horizontal and vertical movement.
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", moveZ);
        anim.SetFloat("Direction", moveX);

        if (moveZ != 0 || moveX != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetButton("Dash")) //Check to see if player is dashing and apply the movement speed bonus. 
        {
            if (currentStam <= minStam)
            {
                moveDirection = transform.forward * moveZ + dashSpeed * transform.right * moveX;
            }
            else if (currentStam > minStam)
            {
                camera.transform.position = offset.transform.position;
                anim.SetBool("isDashing", true);
                currentStam -= usageRate;
                moveDirection = dashSpeed * transform.forward * moveZ + dashSpeed * transform.right * moveX;
            }
        }

        else //Set the player's movement direction. 
        {
            if (currentStam < maxStam)
            {
                currentStam += regenRate;
            }
            moveDirection = transform.forward * moveZ + transform.right * moveX;
        }

        moveDirection *= speed; //Multiply by the player's speed value.
        charCont.Move(moveDirection * Time.deltaTime); //Move player. 
    }
    void PlayerShoot()
    {
        Rigidbody clone;
        Vector3 startPosition = transform.position;
        startPosition.y += 5f;

        clone = Instantiate(laser, startPosition + transform.forward, transform.rotation);
        clone.velocity = transform.forward * shootSpeed;
        clone.GetComponent<DestroyShot>().charge = currentCharge;
    }

    IEnumerator ShootAnim()
    {
        anim.SetBool("isShooting", true);
        speed = 0;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("isShooting", false);
        speed = 20f;
    }
}
