using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Animator anim; // 애니메이터 컴포넌트

    void Start()
    {
        anim = GetComponent<Animator>();

        // 지정된 간격으로 애니메이션을 반복 실행
        InvokeRepeating("PlayAnimation", 0, 3);
    }

    void PlayAnimation()
    {
        anim.SetTrigger("doOn");
    }
}
