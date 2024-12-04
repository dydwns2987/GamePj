using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [System.Serializable]
    private class OnClickEvent : UnityEvent { }

    // Text UI�� Ŭ������ �� ȣ���ϰ� ���� �޼ҵ� ���
    [SerializeField]
    private OnClickEvent onClickEvent;

    // ������ �ٲ��, ��ġ�� �Ǵ� TextMeshProGUI
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyle.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyle.Normal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }
}

