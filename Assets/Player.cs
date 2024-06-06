using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public float movePower = 5f; // 플레이어의 이동 속도
    public GameObject gameOverUI; //게임 오버 시 나타나는 UI
    public float jumpForce = 10f; // 플레이어의 점프 힘
    private bool isGrounded; // 플레이어가 땅에 닿아 있는지    
    private Rigidbody2D rigid;

    public Text timeText; // UI에 시간을 나타낼 Text 요소
    private float elapsedTime = 0f; // 경과 시간



    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 경과 시간 업데이트
        elapsedTime += Time.deltaTime;

        // UI 업데이트
        UpdateTimeUI();
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetButton("Jump") && isGrounded) // 플레이어가 땅에 있을 때만 점프
        {
            Jump();
        }

    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(moveInput * movePower, rigid.velocity.y);
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
        }

        if (collision.gameObject.CompareTag("FallingObject"))
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void UpdateTimeUI()
    {
        // 경과 시간을 텍스트로 변환하여 UI에 표시
        timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString();
    }
}
