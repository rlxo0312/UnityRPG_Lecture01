using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUi : MonoBehaviour
{
    static string nextScene;  //���� ������ �Ѿ� �� Scene �̸��� ���� �����ϴ� ���� 

    [SerializeField] private Image progressBar;

    private void Start()
    {
        //�ε� process�� �����ؼ� �ش� ���μ����� �Ϸ�Ǹ� ���� scene���� �̵��Ѵ�.
        StartCoroutine(LoadSceneProcess());
        
    }
    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;

        SceneManager.LoadScene("Loading Scene");
    }

    IEnumerator LoadSceneProcess()
    {
        yield return new WaitForSeconds(0.3f); //�ڵ��ۼ� ����
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        
        operation.allowSceneActivation = false; //���� ���� �� �ڵ����� ���� ������ �̵��� ���ΰ�? => true : �ڵ����� �̵�, false : �̵� ����
                                                //�ε� �߿� �ּ����� ��� �ð��� �ο���
        float timer = 0f;

        while (!operation.isDone)
        {
            yield return null; //������ ���� �Ʒ� ������ ��ȯ 
            
            if (operation.progress < 0.9f)
            {
                progressBar.fillAmount = operation.progress;
            }
            else
            {
            
                timer += Time.unscaledDeltaTime; //Time.Scale�� ������ �� ����
            
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);   //90& -> 100% ������ �̹��� ���� ������������ ǥ��(90 -> 100,, õõ�� 90-> 100���� ǥ��)
            
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
