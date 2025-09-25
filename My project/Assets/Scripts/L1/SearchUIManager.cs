using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchUIManager : MonoBehaviour
{
    public static SearchUIManager Instance;
    public Text messageText;    // 屏幕中央的提示文字
    public float displayTime = 2f;

    void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine(message));
    }

    IEnumerator ShowMessageRoutine(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        messageText.gameObject.SetActive(false);
    }
}
