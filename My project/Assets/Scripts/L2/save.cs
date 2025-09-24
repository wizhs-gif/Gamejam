using UnityEngine;

public class RescuePerson : MonoBehaviour
{
    // 标记是否已点击
    private bool isClicked = false;
    // 记录点击时间
    private float clickTime;
    // 点击后等待旋转的时间（10 秒）
    public float waitTime = 10f;
    // 旋转角度（逆时针 90 度）
    private Quaternion targetRotation;

    void Start()
    {
        // 初始化目标旋转角度为当前角度逆时针旋转 90 度
        targetRotation = transform.rotation * Quaternion.Euler(0, 0, 90);
    }

    void OnMouseDown()
    {
        // 当未点击过时，记录点击时间并标记为已点击
        if (!isClicked)
        {
            isClicked = true;
            clickTime = Time.time;
            Debug.Log("已点击被救人，10 秒后将逆时针旋转 90 度");
        }
    }

    void Update()
    {
        // 如果已点击且未完成旋转，检查是否达到等待时间
        if (isClicked && transform.rotation != targetRotation)
        {
            if (Time.time - clickTime >= waitTime)
            {
                // 进行旋转
                transform.rotation = targetRotation;
                Debug.Log("被救人已逆时针旋转 90 度");
            }
        }
    }
}