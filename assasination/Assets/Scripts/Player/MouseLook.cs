using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float m_MouseSensitivity = 100f;

    [SerializeField] private Transform m_PlayerBody;

    [SerializeField] Slider m_MouseSensBar;
    [SerializeField] Text m_MouseSensVal;

    [SerializeField] private GameObject m_UIItems;
    [SerializeField] private GameObject m_UIBar;
    private float m_xRotation;

    public bool m_isScoping;
    private void Awake()
    {
        
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(m_isScoping == false)
        {
            m_MouseSensitivity = m_MouseSensBar.value;
        }

        m_MouseSensVal.text = m_MouseSensitivity.ToString("F0");
        float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);

        m_PlayerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                StartGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        m_UIItems.SetActive(false);
        m_UIBar.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    private void StartGame()
    {
        Time.timeScale = 1;
        m_UIItems.SetActive(true);
        m_UIBar.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
