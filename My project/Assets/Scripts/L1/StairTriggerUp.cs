using UnityEngine;

public class StairTriggerUp : StairTriggerBase
{
    protected override void OnClimbFinished()
    {
        if (player != null && target != null)
        {
            player.position = target.position;
            Debug.Log("上楼完成，已瞬移到目标位置");
        }
    }
}
