using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //애니메이션
            anim.SetTrigger("doCollect");
            Invoke("OnCollectAnimationEnd", 0.25f);
        }
    }

    // 오브젝트 삭제
    public void OnCollectAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
