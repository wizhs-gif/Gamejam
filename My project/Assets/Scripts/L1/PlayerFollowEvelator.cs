using UnityEngine;

public class PlayerFollowElevator : MonoBehaviour
{
    // 电梯引用
    public Transform elevator;
    // 人物初始相对于电梯的偏移量
    private Vector3 offset;

    void Start()
    {
        // 获取人物相对于电梯的初始偏移量
        offset = transform.position - elevator.position;
    }

    void Update()
    {
        // 让人物位置跟随电梯，保持初始偏移量
        transform.position = elevator.position + offset;
    }
}