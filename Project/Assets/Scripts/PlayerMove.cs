using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float UpDownSpeed = 2.5f;
    public int jumpCount = 0;
    public int maxJumpCount = 2;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //점프
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            if (jumpCount == 0 || jumpCount == 1)
            {
                if (jumpCount == 0)
                {
                    anim.SetBool("isJumping", true);
                }
                else if (jumpCount == 1 && anim.GetBool("isFalling"))
                {
                    anim.SetBool("DoubleJump", true);
                    anim.SetBool("isFalling", false);
                }
                else
                {
                    anim.SetBool("DoubleJump", true);
                    anim.SetBool("isJumping", false);
                }
                rigid.velocity = new Vector2(rigid.velocity.x, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jumpCount++;
            }
        }
        if (rigid.velocity.y < 0) //하강속도 증가
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (UpDownSpeed - 1) * Time.deltaTime;
        }
        if (rigid.velocity.y > 0) //상승속도 증가
        {
            rigid.velocity += Vector2.up * Physics2D.gravity.y * (UpDownSpeed - 1) * Time.deltaTime;
        }

        //정지 속도
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //방향 전환
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //애니메이션
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    void FixedUpdate()
    {
        if (rigid.velocity.y < 0 && jumpCount > 0)
        {
            anim.SetBool("DoubleJump", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }

        //키로 움직이기
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //최대 속도
        if (rigid.velocity.x > maxSpeed) //오른쪽 최대 속도
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) //왼쪽 최대 속도
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //착지 인식
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isFalling", false);
                    jumpCount = 0;
                }
            }
        }
    }
}
