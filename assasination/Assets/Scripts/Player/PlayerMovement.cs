using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private CharacterController m_Control;
    private Vector3 m_Velocity;

    [Header("movement variables")]
    [SerializeField] private float m_Movespeed;
    [SerializeField] private float m_Gravity = 9.81f;
    [SerializeField] private float m_JumpHeight;

    [Header("Groundcheck")]
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private float m_GroundDistance;
    [SerializeField] private LayerMask m_GroundMask;
    [SerializeField] private bool m_IsGrounded;


    [Header("Crouch")]
    [SerializeField] private Vector3 CurrentHeight;


    [Header("Wall Climb vars")]
    [SerializeField] private LayerMask m_ClimbAbleWallmask;
    [SerializeField] private float CLimbSpeed;

    [Header("sneak vars")]
    [SerializeField] private float m_SoundRange;
    [SerializeField] private Collider[] m_EnemiesInRange;
    [SerializeField] private LayerMask m_EnemyLayer;

    [Header("health vars")]
    [SerializeField] private float m_MaxHealth;
    [SerializeField] private float m_CurrentHealth;

    [SerializeField] private Slider m_HealthBar;

    private void Start()
    {
        CurrentHeight = new Vector3(1, 1, 1);

        m_CurrentHealth = m_MaxHealth;
        m_HealthBar.maxValue = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        m_HealthBar.value = m_CurrentHealth;
        m_IsGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask);

        if (m_IsGrounded && m_Velocity.y < 0)
        {
            m_Velocity.y = -2;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        m_Control.Move(direction * m_Movespeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && m_IsGrounded)
        {
            m_Velocity.y = Mathf.Sqrt(m_JumpHeight * -2f * m_Gravity);
            Debug.Log("ping");
        }
        RaycastHit hit;
        if (Physics.Raycast(m_GroundCheck.position, transform.forward, out hit, 1f, m_ClimbAbleWallmask) && !m_IsGrounded && Input.GetKey(KeyCode.W))
        {
            m_Velocity.y = CLimbSpeed;
        }
        else
        {
            m_Velocity.y += -m_Gravity * Time.deltaTime;
        }

        m_Control.Move(m_Velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }

        transform.localScale = CurrentHeight;

        m_EnemiesInRange = Physics.OverlapSphere(transform.position, m_SoundRange, m_EnemyLayer);

        if (m_EnemiesInRange.Length != 0)
        {
            for (int i = 0; i < m_EnemiesInRange.Length; i++)
            {
                m_EnemiesInRange[i].GetComponent<enemyState>().hunting = true;
            }
        }
    }

    private void StartCrouch()
    {
        CurrentHeight.y = 0.5f;
        m_SoundRange /= 2;
        m_Movespeed /= 2;
    } 
    private void StopCrouch()
    {
        CurrentHeight.y = 1f;
        m_SoundRange *= 2;
        m_Movespeed *= 2;
    }
    public void TakeDamage(float _damage)
    {
        m_CurrentHealth -= _damage;

        if (m_CurrentHealth < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }
}
