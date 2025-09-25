using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("UI 设置")]
    public Button interactButton;       // 交互按钮
    public Slider progressBar;          // 可选进度条

    [Header("门设置")]
    public BoxCollider2D triggerZone;        // 触发区域（直接拖Collider进来）
    public float interactTime = 2f;     // 开门读条时间        // 门保持打开时间

    private bool isPlayerNear = false;
    private bool isInteracting = false;

    void Start()
    {
        if (interactButton != null)
        {
            interactButton.gameObject.SetActive(false);
            interactButton.onClick.AddListener(StartInteraction);
        }

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (triggerZone == null || isInteracting) return;

        // 检测玩家是否在触发区域
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(
            triggerZone.bounds.center,
            triggerZone.bounds.size,
            0f); // 2D不需要旋转，默认0°

        bool playerFound = false;
        foreach (var col in hitColliders)
        {
            if (col.CompareTag("Player"))
            {
                playerFound = true;
                break;
            }
        }

        if (playerFound && !isPlayerNear)
        {
            isPlayerNear = true;
            interactButton?.gameObject.SetActive(true);
        }
        else if (!playerFound && isPlayerNear)
        {
            isPlayerNear = false;
            interactButton?.gameObject.SetActive(false);
        }
    }

    public void StartInteraction()
    {
        if (isPlayerNear && !isInteracting)
            StartCoroutine(InteractAndOpenDoor());
    }

    private IEnumerator InteractAndOpenDoor()
    {
        isInteracting = true;
        interactButton?.gameObject.SetActive(false);

        // 显示进度条
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = 0f;
        }

        float timer = 0f;
        while (timer < interactTime)
        {
            timer += Time.deltaTime;
            if (progressBar != null)
                progressBar.value = timer / interactTime;
            yield return null;
        }

        // 隐藏进度条
        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        // 开门逻辑
        Debug.Log("门已打开！");
        gameObject.SetActive(false); // 这里用禁用模拟开门效果

        //yield return new WaitForSeconds(openTime);

        // 需要门自动恢复的话，取消注释
        // gameObject.SetActive(true);

        isInteracting = false;
    }

    void OnDrawGizmosSelected()
    {
        if (triggerZone != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(triggerZone.bounds.center, triggerZone.bounds.size);
        }
    }
}
