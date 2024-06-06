using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public float movePower = 5f; // �÷��̾��� �̵� �ӵ�
    public GameObject gameOverUI; //���� ���� �� ��Ÿ���� UI
    public float jumpForce = 10f; // �÷��̾��� ���� ��
    private bool isGrounded; // �÷��̾ ���� ��� �ִ���    
    private Rigidbody2D rigid;

    public Text timeText; // UI�� �ð��� ��Ÿ�� Text ���
    private float elapsedTime = 0f; // ��� �ð�



    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��� �ð� ������Ʈ
        elapsedTime += Time.deltaTime;

        // UI ������Ʈ
        UpdateTimeUI();
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetButton("Jump") && isGrounded) // �÷��̾ ���� ���� ���� ����
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
        // ��� �ð��� �ؽ�Ʈ�� ��ȯ�Ͽ� UI�� ǥ��
        timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString();
    }
}
