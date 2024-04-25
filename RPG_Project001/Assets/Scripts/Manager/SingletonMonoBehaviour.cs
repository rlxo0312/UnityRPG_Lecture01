using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤 패턴을 구현에서 반복을 없애주기 위한 클래스
/// 인스턴스를 생성하고 게임 실행 중에 파괴되자 않도록 보장 
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
    private static T instance; //실제 인스턴스
    public static T Instance => instance; //위 instance 접근가능 

    public static T GetOrCreateInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType(typeof(T)) as T; //get, 하이어라키 창에 검색했을 때없을때 get
            if(instance == null)
            {
                GameObject newGameObject = new GameObject(typeof(T).Name, typeof(T));

                instance = newGameObject.AddComponent<T>();
            }
            return instance;
        }
        return instance;
    }
    protected virtual void Awake()
    {
        instance = this as T;

        if (Application.isPlaying) //자동으로 true 판정
        {
            DontDestroyOnLoad(gameObject);
        }

    }

}
