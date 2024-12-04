using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScenario : MonoBehaviour
{
    [SerializeField]
    Progress progress;
    [SerializeField]
    private SceneNames nextScene;

    void Awake()
    {
        SystemSetup();    
    }

    void SystemSetup()
    {
        Application.runInBackground = true;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        progress.Play(OnAfterProgress);
    }
    
    void OnAfterProgress()
    {
        Utils.LoadScene(nextScene);
    }
}


