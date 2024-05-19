using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;

    private bool isInvincible = false;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (isInvincible) return;

        if (collision.gameObject.tag == "Enemy")
        {
            // 플레이어가 떨어지고 있는지 확인
            if (rigid.velocity.y < 0)
            {
                // 플레이어의 발 위치가 적의 머리보다 높은지 확인
                float playerBottom = transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 4);
                float enemyTop = collision.transform.position.y + (collision.collider.bounds.size.y / 4);

                if (playerBottom > enemyTop)
                {
                    OnAttack(collision.transform);
                }
                else
                {
                    OnDamaged(collision.transform.position);
                }
            }
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Item") 
        {
            bool Apple = collision.gameObject.name.Contains("Apple");
            bool Banana = collision.gameObject.name.Contains("Banana");
            bool Cherry = collision.gameObject.name.Contains("Cherry");
            bool Kiwi = collision.gameObject.name.Contains("Kiwi");
            bool Melon = collision.gameObject.name.Contains("Melon");
            bool Orange = collision.gameObject.name.Contains("Orange");
            bool Pineapple = collision.gameObject.name.Contains("Pineapple");
            bool Strawberry = collision.gameObject.name.Contains("Strawberry");
            
            //점수
            if (Apple) gameManager.stagePoint += 50;
            else if (Banana) gameManager.stagePoint += 100;
            else if (Cherry) gameManager.stagePoint += 150;
            else if (Kiwi) gameManager.stagePoint += 200;
            else if (Melon) gameManager.stagePoint += 250;
            else if (Orange) gameManager.stagePoint += 300;
            else if (Pineapple) gameManager.stagePoint += 350;
            else if (Strawberry) gameManager.stagePoint += 400;
        }
            

        else if (collision.gameObject.tag == "CheckPoint")
        {
            //다음 스테이지
            gameManager.NextStage();
        }
    }

    void OnAttack(Transform enemy)
    {
        //점수
        gameManager.stagePoint +=100;

        //적 죽이기
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }

    //플레이어 데미지
    void OnDamaged(Vector2 targetPos)
    {
        //체력 감소
        gameManager.HealthDown();

        //투명도
        spriteRenderer.color = new Color(1,1,1,0.4f);

        //충돌 반응
        int dirc = transform.position.x - targetPos.x > 0? 1 : -1;
        rigid.AddForce(new Vector2(dirc * 3, 5), ForceMode2D.Impulse);

        //애니메이션
        anim.SetTrigger("doDamaged");

        isInvincible = true;
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        isInvincible = false;
    }

    public void OnDie()
    {
        anim.SetTrigger("doDie");
        Invoke("Die", 0.3f);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}
