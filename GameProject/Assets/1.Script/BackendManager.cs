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
            Debug.Log($"초기화 성공 : {bro}");
        }
        else
        {
            Debug.LogError($"초기화 실패 : {bro}");
        }
    }
}
