using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public int highsc = 0;
    public PlayerAction player;
    public GameObject[] Stages;
    public GameObject Menu;
    public GameObject gameover;

    public Image[] UIhealth;
    public Text game;
    public Text UIPoint;
    public Text UIStage;
    public Text URpoint;
    public Text Highscore;

    void Start()
    {
        // 게임 시작 시 highsc 값을 불러옴
        highsc = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    void Update()
    {
        // 현재 점수 업데이트
        UIPoint.text = (totalPoint + stagePoint).ToString();
        URpoint.text = "획득점수 : " + (totalPoint + stagePoint).ToString();

        if (Input.GetButtonDown("Cancel"))
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
        }

        int currentScore = totalPoint + stagePoint;
        if (currentScore > highsc)
        {
            highsc = currentScore;
            Highscore.color = new Color(1, 0, 0);
            Highscore.text = highsc.ToString() + " (신기록)";

            // 신기록 갱신 시 highsc 값을 저장
            PlayerPrefs.SetInt("HighScore", highsc);
            PlayerPrefs.Save();
        }
        else
        {
            Highscore.color = new Color(1, 1, 1);
            Highscore.text = highsc.ToString();
        }
    }

    // 버튼 클릭했을 때 게임 중지
    public void OnPauseButtonClicked()
    {
        Menu.SetActive(true);
        Time.timeScale = 0;
    }

    // 버튼 클릭했을 때 게임 종료
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    // 버튼 클릭했을 때 게임 재개
    public void OnResumeButtonClicked()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }

    // 버튼 클릭했을 때 게임 재시작
    public void OnRestartButtonClicked()
    {
        // 현재 highsc 값을 저장
        PlayerPrefs.SetInt("HighScore", highsc);
        PlayerPrefs.Save();

        // 장면을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // 점수 초기화
        totalPoint = 0;
        stagePoint = 0;

        Time.timeScale = 1;
        gameover.SetActive(false);
    }

    public void NextStage()
    {
        // 스테이지 변환
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "Stage" + (stageIndex + 1);
        }
        else // 게임 클리어
        {
            // 플레이어 정지
            Time.timeScale = 0;
            // 결과창
            Stages[stageIndex].SetActive(false);
            gameover.SetActive(true);
            game.text = "게임 클리어!";
        }
        // 점수 계산
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 0)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
            if (health == 0) // 죽을 때 효과
            {
                player.OnDie();
                Invoke("ResultUI", 0.5f);
            }
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-10, -4.5f, -1);
        player.VelocityZero();
    }

    void ResultUI()
    {
        gameover.SetActive(true);
        game.text = "게임 오버";
    }

    private void UpdateHighScoreText()
    {
        Highscore.text = highsc.ToString();
        Highscore.color = new Color(1, 1, 1);
    }
}