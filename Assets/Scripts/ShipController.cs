using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float m_thrusterForce = 10.0f;
    public float m_horizontalSensitivity = 2.0f;
    public float m_verticalSensitivity = 2.0f;
    public float m_rayLenght = 10.0f;
    public LayerMask m_mask = 1;
    public Rigidbody m_weaponCannon = null;
    public Rigidbody m_bulletPrefab = null;
    public GameObject m_engineTrails = null;
    public GameObject m_bottomTrails = null;
    public GameObject m_upTrails = null;
    public GameObject m_leftTrails = null;
    public GameObject m_rightTrails = null;
    public GameObject m_frontTrails = null; 

    private Rigidbody m_shipRigidbody = null;

    // Use this for initialization
    void Start ()
    {
        m_shipRigidbody = gameObject.GetComponent<Rigidbody>();
        m_engineTrails.SetActive(false);
        m_bottomTrails.SetActive(false);
        m_upTrails.SetActive(false);
        m_leftTrails.SetActive(false);
        m_rightTrails.SetActive(false);
        m_frontTrails.SetActive(false);
    }

    // Update is called once per frame
	void FixedUpdate ()
    {
        float torqueAroundY = m_horizontalSensitivity * Input.GetAxis("Mouse X");
        float torqueAroundX = m_verticalSensitivity * Input.GetAxis("Mouse Y");
        float spinWeaponAroundY = Input.GetAxis("Mouse ScrollWheel");

        m_shipRigidbody.AddTorque(torqueAroundY * transform.up * m_thrusterForce, ForceMode.Force);
        m_shipRigidbody.AddTorque(torqueAroundX * -transform.right * m_thrusterForce, ForceMode.Force);
        m_weaponCannon.AddTorque(spinWeaponAroundY * transform.up * 50, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.Q))
        {
            m_shipRigidbody.AddTorque(transform.forward * m_thrusterForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.E))
        {
            m_shipRigidbody.AddTorque(-transform.forward * m_thrusterForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            m_shipRigidbody.AddForce(transform.up * m_thrusterForce, ForceMode.Force);
            m_bottomTrails.SetActive(true);
        }
        else
        {
            m_bottomTrails.SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_shipRigidbody.AddForce(-transform.up * m_thrusterForce, ForceMode.Force);
            m_upTrails.SetActive(true);
        }
        else
        {
            m_upTrails.SetActive(false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_shipRigidbody.AddForce(-transform.right * m_thrusterForce, ForceMode.Force);
            m_rightTrails.SetActive(true);
        }
        else
        {
            m_rightTrails.SetActive(false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_shipRigidbody.AddForce(transform.right * m_thrusterForce, ForceMode.Force);
            m_leftTrails.SetActive(true);
        }
        else
        {
            m_leftTrails.SetActive(false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_shipRigidbody.AddForce(transform.forward * m_thrusterForce, ForceMode.Force);
            m_engineTrails.SetActive(true);
        }
        else
        {
            m_engineTrails.SetActive(false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_shipRigidbody.AddForce(-transform.forward * m_thrusterForce, ForceMode.Force);
            m_frontTrails.SetActive(true);
        }
        else
        {
            m_frontTrails.SetActive(false);
        }

        if (Input.GetMouseButtonDown(2))
        {
            Rigidbody bullet = Instantiate(m_bulletPrefab, m_weaponCannon.transform.position + 5 * m_weaponCannon.transform.forward, m_weaponCannon.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.AddForce(m_weaponCannon.transform.forward * 50 + m_shipRigidbody.velocity, ForceMode.Impulse);
        }
    }
}