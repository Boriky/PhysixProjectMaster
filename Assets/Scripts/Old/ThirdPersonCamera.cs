using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform m_lookAt;
    public Transform m_camTransform;

    private Camera m_camera;

    private float m_distance = 2.0f;
    private float m_currentX = 0.0f;
    private float m_currentY = 0.0f;
    private float m_sensitivityX = 4.0f;
    private float m_sensitivityY = 4.0f;
	
    // Use this for initialization
	private void Start ()
    {
        m_camTransform = transform;
        m_camera = Camera.main;
	}

    private void Update()
    {
        m_currentX += Input.GetAxis("Mouse X") * m_sensitivityX;
        m_currentY += Input.GetAxis("Mouse Y") * m_sensitivityY;

        m_currentY = Mathf.Clamp(m_currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }
	
	// After the movement of the player we calculate the position of the camera
	void LateUpdate ()
    {
        Vector3 direction = new Vector3(m_distance, 0, 0);
        Quaternion rotation = Quaternion.Euler(m_currentY, m_currentX, 0);
        m_camTransform.position = m_lookAt.position + (rotation * direction);
        m_camTransform.LookAt(m_lookAt.position);
	}
}