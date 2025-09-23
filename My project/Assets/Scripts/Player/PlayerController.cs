using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveX, 0);

        rb.velocity = movement * moveSpeed;

        // ✅ 动画控制
        animator.SetBool("IsRun", Mathf.Abs(moveX) > 0.01f);

        // ✅ 翻转角色
        if (moveX > 0)
            spriteRenderer.flipX = false; // 面向右
        else if (moveX < 0)
            spriteRenderer.flipX = true;  // 面向左
        
    }
    
}