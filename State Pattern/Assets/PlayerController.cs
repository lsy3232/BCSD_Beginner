using UnityEngine;

// 플레이어가 할 수 있는 행동
public enum PlayerState { Idle = 0, Walk, Run, Attack }

public class PlayerController : MonoBehaviour
{
    private PlayerState playerState;

    private void Awake() 
    {
        // 초기상태 지정
        ChangeState(PlayerState.Idle);
    }

    private void Update() 
    {
        if ( Input.GetKeyDown("1") ) ChangeState(PlayerState.Idle);
        else if (Input.GetKeyDown("2")) ChangeState(PlayerState.Walk);
        else if (Input.GetKeyDown("3")) ChangeState(PlayerState.Run);
        else if (Input.GetKeyDown("4")) ChangeState(PlayerState.Attack);

        UpdateState();
    }
    
    private void UpdateState()
    {
        if ( playerState == PlayerState.Idle ) Debug.Log("제자리");
        else if (playerState == PlayerState.Walk) Debug.Log("걷기");
        else if (playerState == PlayerState.Run) Debug.Log("뛰기");
        else if (playerState == PlayerState.Attack) Debug.Log("공격");
    }

    private void ChangeState(PlayerState newState)
    {
        playerState = newState;
    }
}
