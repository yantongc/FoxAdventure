using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PLAYER_STATE { Stand, Jump };

    private Rigidbody2D rd;

    private Collider2D coll;

    private Animator animator;

    private GameObject groundCheck;

    private bool jumpPress;

    private int jumpCount = 2;

    private PLAYER_STATE playerState;

    public float speed;

    public float jumpForce;

    public LayerMask ground;



    private void OnEnable()
    {
        rd = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.GetChild(0).gameObject;
    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPress = true;
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
        AnimateChange();
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        rd.velocity = new Vector2(horizontal * speed * Time.deltaTime, rd.velocity.y);
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    void PlayerJump()
    {
        bool isGround = Physics2D.OverlapCircle(groundCheck.transform.position, 0.1f, ground);
        if (isGround)
        {
            jumpCount = 2;
            playerState = PLAYER_STATE.Stand;
            if (jumpPress)
            {
                playerState = PLAYER_STATE.Jump;
                HandleJump();
            }
        }
        else
        {
            if (jumpPress && jumpCount > 0 && playerState == PLAYER_STATE.Jump)
            {
                HandleJump();
            }
        }
    }

    private void HandleJump()
    {
        rd.velocity = new Vector2(rd.velocity.x, jumpForce * Time.deltaTime);
        jumpCount--;
        jumpPress = false;
    }

    private void AnimateChange()
    {
        switch (playerState)
        {
            case PLAYER_STATE.Stand:
                animator.SetBool("IsDrowing", false);
                break;
            case PLAYER_STATE.Jump:
                if (rd.velocity.y > 0)
                {
                    animator.SetBool("IsJumping", true);
                }
                else if (rd.velocity.y < 0)
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsDrowing", true);
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (collision.name.IndexOf("Cherry") > -1)
            {
                GameManager.Instance.CurCherryNum++;
            }
            else
            {
                GameManager.Instance.CurGemNum++;
            }
            Destroy(collision.gameObject);

        }
    }
}
