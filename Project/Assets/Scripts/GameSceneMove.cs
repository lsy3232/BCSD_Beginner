using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneMove : MonoBehaviour
{
    public GameObject method;
    public Text Title;

    //씬 전환
    public void GameSceneCtrl()
    {
        SceneManager.LoadScene("Game");
    }

    //버튼 클릭했을 때 게임 종료
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    //버튼 클릭했을 때 게임 방법
    public void OnMethodButtonClicked()
    {
        method.SetActive(true);
        Title.text = "";
    }

    //버튼 클릭했을 때 게임 방법
    public void OnCloseButtonClicked()
    {
        method.SetActive(false);
        Title.text = "구리의 모험";
    }
}
