using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        if(instance == null) instance=this;
        else if (instance != this) 
        {
            Destroy(gameObject);
            Debug.Log("두 개 이상의 GameManager를 가질 수 없습니다.");
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Call()
    {
        
    }
}
