using UnityEngine;

public class Rockhead : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;

    Vector2 originalPosition; // 원래 위치

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalPosition = rigid.position; // 시작 위치를 저장
    }

    void FixedUpdate()
    {
        // 지형 확인
        Vector2 downVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawRay(downVec, Vector2.down*10f, new Color(0, 1, 0));

        // 플레이어 레이어 마스크 설정
        int playerLayerMask = LayerMask.GetMask("Player");

        // Raycast를 사용하여 플레이어 감지
        RaycastHit2D rayHit = Physics2D.Raycast(downVec, Vector2.down, 10f, playerLayerMask);

        if (rayHit.collider != null && rayHit.collider.CompareTag("Player"))
        {
            Fall();
        }
    }

    void Fall()
    {
        // Rockhead가 떨어지도록 설정
        rigid.bodyType = RigidbodyType2D.Dynamic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥과 충돌했는지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            anim.SetTrigger("isGrounded");
            Invoke("ReturnToOriginalPosition", 2f);
        }
    }

    void ReturnToOriginalPosition()
    {
        // 원래 위치로 돌아가기
        rigid.bodyType = RigidbodyType2D.Kinematic;
        rigid.position = originalPosition;
    }
}
