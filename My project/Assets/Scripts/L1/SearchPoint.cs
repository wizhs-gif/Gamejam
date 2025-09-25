using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class SearchPoint : MonoBehaviour
{
    [Header("UI 设置")]
    public Button searchButton;
    public Slider progressBar;

    [Header("搜索设置")]
    public Item[] possibleItems;   // 可能刷出的物品
    public float searchTime = 2f;    // 搜索耗时

    private bool isPlayerNear = false;
    private bool isSearching = false;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true; // 确保是触发器
    }

    void Start()
    {
        if (searchButton != null)
        {
            searchButton.gameObject.SetActive(false);
            searchButton.onClick.AddListener(StartSearch);
        }

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isSearching) return;

        // 检测玩家是否进入触发区域
        Collider2D player = Physics2D.OverlapBox(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            LayerMask.GetMask("Player")
        );

        bool playerFound = player != null;

        if (playerFound && !isPlayerNear)
        {
            isPlayerNear = true;
            searchButton?.gameObject.SetActive(true);
        }
        else if (!playerFound && isPlayerNear)
        {
            isPlayerNear = false;
            searchButton?.gameObject.SetActive(false);
        }
    }

    void StartSearch()
    {
        if (isPlayerNear && !isSearching)
            StartCoroutine(SearchRoutine());
    }

    IEnumerator SearchRoutine()
    {
        isSearching = true;
        searchButton.gameObject.SetActive(false);

        // 显示进度条
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = 0f;
        }

        float timer = 0f;
        while (timer < searchTime)
        {
            timer += Time.deltaTime;
            if (progressBar != null)
                progressBar.value = timer / searchTime;
            yield return null;
        }

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        // 随机生成一个物品
        Item foundItem = possibleItems.Length > 0
            ? possibleItems[Random.Range(0, possibleItems.Length)]
            : null;

        if (foundItem != null)
        {
            Debug.Log($"你找到了 {foundItem.itemName}");

            // 屏幕显示提示
            SearchUIManager.Instance.ShowMessage($"你找到了 {foundItem.itemName}");

            // 存入全局仓库
            BagManager.Instance.AddItem(foundItem);
        }

        // 搜索完成后销毁该搜索点，防止重复搜索
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // 在Scene视图中显示触发范围
        if (boxCollider == null) boxCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}
