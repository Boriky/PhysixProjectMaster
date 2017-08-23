using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRevolution : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    GameObject m_sun = null;

    [Header("Gameplay values")]
    [SerializeField]
    float m_revolutionVelocity = 5.0f;
    [SerializeField]
    float m_rotationVelocity = 5.0f;

    private Rigidbody m_rb;

	// Use this for initialization
	void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.AddTorque(transform.up * m_rotationVelocity, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Rotates the transform about axis passing through point in world coordinates by angle degrees.
        if (m_rb.velocity.magnitude == 0)
        {
            transform.RotateAround(m_sun.transform.position, Vector3.up, m_revolutionVelocity * Time.deltaTime);
        }
    }
}
