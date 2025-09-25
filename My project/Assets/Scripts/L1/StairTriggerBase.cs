using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class StairTriggerBase : MonoBehaviour
{
    [Header("UI 控件")]
    public Button actionButton;
    public Slider progressBar;
    public float climbTime = 2f; // 读条时间

    [Header("角色与目标点")]
    public Transform player; // 拖角色
    public Transform target; // 拖到目标位置（上楼点/下楼点）

    protected PlayerController playerMovement;
    protected bool isPlayerNearby = false;

    protected virtual void Start()
    {
        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        if (actionButton != null)
            actionButton.gameObject.SetActive(false);
            actionButton.onClick.AddListener(() => StartCoroutine(ClimbStairsCoroutine()));
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (player == null) player = other.transform;
            playerMovement = player.GetComponent<PlayerController>();
            if (actionButton != null) actionButton.gameObject.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (actionButton != null) actionButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator ClimbStairsCoroutine()
    {
        if (playerMovement != null)
            playerMovement.canMove = false;

        if (progressBar != null)
        {
            progressBar.value = 0;
            progressBar.gameObject.SetActive(true);
        }

        float elapsed = 0f;
        while (elapsed < climbTime)
        {
            elapsed += Time.deltaTime;
            if (progressBar != null)
                progressBar.value = elapsed / climbTime;
            yield return null;
        }

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        // 调用子类逻辑
        OnClimbFinished();

        if (playerMovement != null)
            playerMovement.canMove = true;
    }

    /// <summary>
    /// 由子类实现：决定瞬移到哪里
    /// </summary>
    protected abstract void OnClimbFinished();
}
