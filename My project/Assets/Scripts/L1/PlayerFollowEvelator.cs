using UnityEngine;

public class PlayerFollowElevator : MonoBehaviour
{
    // ��������
    public Transform elevator;
    // �����ʼ����ڵ��ݵ�ƫ����
    private Vector3 offset;

    void Start()
    {
        // ��ȡ��������ڵ��ݵĳ�ʼƫ����
        offset = transform.position - elevator.position;
    }

    void Update()
    {
        // ������λ�ø�����ݣ����ֳ�ʼƫ����
        transform.position = elevator.position + offset;
    }
}