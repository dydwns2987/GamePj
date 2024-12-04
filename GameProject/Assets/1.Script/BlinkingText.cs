using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public float interval = 1f; // 깜박임 간격
    private Text text; // 텍스트 컴포넌트

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        while (true)
        {
            text.enabled = !text.enabled; // 텍스트 가시성 토글
            yield return new WaitForSeconds(interval); // 지정한 간격만큼 대기
        }
    }
}