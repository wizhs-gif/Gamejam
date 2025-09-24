using UnityEngine;

public class RescuePerson : MonoBehaviour
{
    // ����Ƿ��ѵ��
    private bool isClicked = false;
    // ��¼���ʱ��
    private float clickTime;
    // �����ȴ���ת��ʱ�䣨10 �룩
    public float waitTime = 10f;
    // ��ת�Ƕȣ���ʱ�� 90 �ȣ�
    private Quaternion targetRotation;

    void Start()
    {
        // ��ʼ��Ŀ����ת�Ƕ�Ϊ��ǰ�Ƕ���ʱ����ת 90 ��
        targetRotation = transform.rotation * Quaternion.Euler(0, 0, 90);
    }

    void OnMouseDown()
    {
        // ��δ�����ʱ����¼���ʱ�䲢���Ϊ�ѵ��
        if (!isClicked)
        {
            isClicked = true;
            clickTime = Time.time;
            Debug.Log("�ѵ�������ˣ�10 �����ʱ����ת 90 ��");
        }
    }

    void Update()
    {
        // ����ѵ����δ�����ת������Ƿ�ﵽ�ȴ�ʱ��
        if (isClicked && transform.rotation != targetRotation)
        {
            if (Time.time - clickTime >= waitTime)
            {
                // ������ת
                transform.rotation = targetRotation;
                Debug.Log("����������ʱ����ת 90 ��");
            }
        }
    }
}