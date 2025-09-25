using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // 跟随目标（通常是玩家）
    public float smoothSpeed = 0.125f; // 平滑跟随速度
    public Vector3 offset; // 摄像机相对角色的偏移

    [Header("摄像机限制范围")]
    public Vector2 minPosition; // 左下角
    public Vector2 maxPosition; // 右上角

    void LateUpdate()
    {
        if (target == null) return;

        // 目标位置 + 偏移
        Vector3 desiredPosition = target.position + offset;

        // 限制范围
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        // 平滑移动
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
