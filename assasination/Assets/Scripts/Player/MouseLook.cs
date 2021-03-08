using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float m_MouseSensitivity = 100f;

    [SerializeField] private Transform m_PlayerBody;

    private float m_xRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0, 0);

        m_PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
