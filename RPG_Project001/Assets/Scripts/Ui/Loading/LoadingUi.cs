using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUi : MonoBehaviour
{
    static string nextScene;  //다음 씬으로 넘어 갈 Scene 이름의 값을 저장하는 변수 

    [SerializeField] private Image progressBar;

    private void Start()
    {
        //로딩 process를 진행해서 해당 프로세스가 완료되면 다음 scene으로 이동한다.
        StartCoroutine(LoadSceneProcess());
        
    }
    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;

        SceneManager.LoadScene("Loading Scene");
    }

    IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(0.3f); //코드작성 이유
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        
        operation.allowSceneActivation = false; //씬이 끝날 때 자동으로 다음 씬으로 이동할 것인가? => true : 자동으로 이동, false : 이동 안함
                                                //로딩 중에 최소한의 대기 시간을 부여함
        float timer = 0f;

        while (!operation.isDone)
        {
            yield return null; //프레임 마다 아래 내용을 반환 
            
            if (operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;
            }
            else
            {
            
                timer += Time.unscaledDeltaTime; //Time.Scale을 변경할 수 있음
            
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);   //90& -> 100% 사이의 이미지 증가 선형보간으로 표시(90 -> 100,, 천천히 90-> 100가게 표현)
            
                if (progressBar.fillAmount >= 1f)
                {
                    yield return new WaitForSeconds(1f);
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
