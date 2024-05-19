using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        Think();
    }

    void FixedUpdate()
    {
        //이동
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형 확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null) Turn();
        Vector2 RLVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawRay(RLVec, new Vector3(nextMove, 0, 0), new Color(1, 0, 0));
        RaycastHit2D frontObstacleHit = Physics2D.Raycast(RLVec, new Vector2(nextMove, 0), 0.5f, LayerMask.GetMask("Platform"));
        if (frontObstacleHit.collider != null) Turn();
    }

    //재귀 함수
    void Think()
    {
        //다음 행동
        nextMove = Random.Range(-1, 2);

        //애니메이션
        anim.SetInteger("WalkSpeed", nextMove);

        //적 스프라이트 방향
        if(nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //재귀
        float nextThinkTime = Random.Range(2f, 2f);
        Invoke("Think", 5);
    }

    //충돌 후 방향 전환
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }

    //적 데미지
    public void OnDamaged()
    {
        //투명도
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //FlipY
        spriteRenderer.flipY = true;
        //콜라이더 비활성화
        capsuleCollider.enabled = false;
        //죽인 뒤 이펙트
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //적 파괴
        Invoke("DeActive", 5);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
