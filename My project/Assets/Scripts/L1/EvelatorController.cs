using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public float moveSpeed = 2f;
    public float floorHeight = 3f;
    private int currentFloor = 1;
    private int targetFloor = 1;
    private bool isMoving = false;
    private int requestedFloor = -1;

    void Update()
    {
        if (isMoving)
        {
            MoveElevator();
        }
        else
        {
            if (requestedFloor != -1)
            {
                targetFloor = requestedFloor;
                isMoving = true;
                requestedFloor = -1;
            }
        }
    }

    void MoveElevator()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, floorHeight * (targetFloor - 1), transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
            currentFloor = targetFloor;
        }
    }

    public void RequestFloor(int floor)
    {
        requestedFloor = floor;
    }
}
