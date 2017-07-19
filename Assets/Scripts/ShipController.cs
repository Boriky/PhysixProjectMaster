using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float m_thrusterForce = 10.0f;
    public float m_horizontalSensitivity = 2.0F;
    public float m_verticalSensitivity = 2.0F;

    private Rigidbody m_shipRigidbody = null;

    // Use this for initialization
    void Start ()
    {
        m_shipRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float torqueAroundY = m_horizontalSensitivity * Input.GetAxis("Mouse X");
        float torqueAroundX = m_verticalSensitivity * Input.GetAxis("Mouse Y");

        m_shipRigidbody.AddTorque(torqueAroundY * gameObject.transform.up * m_thrusterForce, ForceMode.Force);
        m_shipRigidbody.AddTorque(torqueAroundX * gameObject.transform.right * m_thrusterForce, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_shipRigidbody.AddTorque(gameObject.transform.forward * m_thrusterForce, ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_shipRigidbody.AddTorque(-gameObject.transform.forward * m_thrusterForce, ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            m_shipRigidbody.AddForce(gameObject.transform.up * m_thrusterForce, ForceMode.Force);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            m_shipRigidbody.AddForce(-gameObject.transform.up * m_thrusterForce, ForceMode.Force);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            m_shipRigidbody.AddForce(-gameObject.transform.right * m_thrusterForce, ForceMode.Force);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            m_shipRigidbody.AddForce(gameObject.transform.right * m_thrusterForce, ForceMode.Force);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_shipRigidbody.AddForce(gameObject.transform.forward * m_thrusterForce, ForceMode.Force);
        }
    }
}