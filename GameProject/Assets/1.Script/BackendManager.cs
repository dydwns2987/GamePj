using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        BackendSetup();
    }

    private void Update()
    {
        if(Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }

    void BackendSetup()
    {
        var bro = Backend.Initialize(true);

        if(bro.IsSuccess())
        {
            Debug.Log($"�ʱ�ȭ ���� : {bro}");
        }
        else
        {
            Debug.LogError($"�ʱ�ȭ ���� : {bro}");
        }
    }
}
