using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    private float moveInput;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        // 获取左右输入（A/D 或 ←/→）
        moveInput = Input.GetAxisRaw("Horizontal");  

        // 翻转角色朝向
        if (spriteRenderer != null && moveInput != 0)
        {
            spriteRenderer.flipX = moveInput < 0;
        }
    }

    void FixedUpdate()
    {
        // 用 Rigidbody2D 移动角色
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

}
