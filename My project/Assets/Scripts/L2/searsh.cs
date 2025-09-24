using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxInteraction : MonoBehaviour
{
    // �洢�������Ƶ��б��ɸ���ʵ�������ӹ���
    public List<string> tools = new List<string> { "����", "��˿��", "����", "ǯ��" };
    // ��������Ƿ��ѱ����
    private bool isClicked = false;
    // ����Ƿ��Ѿ���ù���
    private bool hasReceivedTool = false;
    // ��¼�����ʱ��
    private float clickTime;
    // �����ȴ���ù��ߵ�ʱ�䣨10 �룩
    public float waitTime = 10f;

    void Start()
    {
        // ��ʼ�������б�ȷ��������һ������
        if (tools.Count == 0)
        {
            tools.Add("Ĭ�Ϲ���");
        }
    }

    void OnMouseDown()
    {
        // ������δ�������δ��ù���ʱ����¼���ʱ�䲢���Ϊ�ѵ��
        if (!isClicked && !hasReceivedTool)
        {
            isClicked = true;
            clickTime = Time.time;
            Debug.Log("�ѵ�����ӣ�10 ��󽫻���������");
        }
    }

    void Update()
    {
        // ��������ѱ������δ��ù��ߣ�����Ƿ�ﵽ�ȴ�ʱ��
        if (isClicked && !hasReceivedTool)
        {
            if (Time.time - clickTime >= waitTime)
            {
                // �ӹ����б������ѡ��һ������
                string randomTool = tools[Random.Range(0, tools.Count)];
                hasReceivedTool = true;
                Debug.Log("���������ߣ�" + randomTool);
                // ���������ӻ�ù��ߺ�������߼�������������ӹ��ߵ�
            }
        }
    }
}