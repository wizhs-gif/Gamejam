using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElevatorButton : MonoBehaviour
{
    // ��Ӧ��¥��
    public int floor;
    // ��������
    public ElevatorController elevator;
    // ��ť�Ƿ񱻰���
    private bool isPressed = false;
    private Button button;
    

    void Start()
    {
        button = GetComponent<Button>();
    }

    void OnMouseDown()
    {
        if (!isPressed)
        {
            isPressed = true;

            elevator.RequestFloor(floor);
        }
    }
}
