using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;
    public PlayerResources playerResources;
    public Button btn;

    void Awake()
    {
        // 确保只有一个 GameManager 存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 场景切换不销毁
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 初始化玩家资源（可以改成从存档读取）
        playerResources = new PlayerResources(startGold: 500, startMaterial: 0);
    }
    void Start()
    {
        btn.onClick.AddListener(BackButton);
    }

    void BackButton()
    {
        SceneManager.LoadScene("Map");
    }
}
