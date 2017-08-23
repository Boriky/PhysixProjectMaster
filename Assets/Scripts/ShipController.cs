using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    [Header("Objects references")]
    [SerializeField]
    MenuManager m_menuManager = null;
    [SerializeField]
    Rigidbody m_weaponCannon = null;
    [SerializeField]
    Rigidbody m_bulletPrefab = null;
    [SerializeField]
    GameObject m_engineTrails = null;
    [SerializeField]
    GameObject m_bottomTrails = null;
    [SerializeField]
    GameObject m_upTrails = null;
    [SerializeField]
    GameObject m_leftTrails = null;
    [SerializeField]
    GameObject m_rightTrails = null;
    [SerializeField]
    GameObject m_frontTrails = null;

    [Header("Gameplay values")]
    [SerializeField]
    float m_thrusterForce = 10.0f;
    [SerializeField]
    float m_maxVelocity = 100.0f;
    [SerializeField]
    float m_defaultDrag = 0.1f;
    [SerializeField]
    float m_shipDecelerationFactor = 1.02f;
    [SerializeField]
    float m_rayLenght = 10.0f;
    [SerializeField]
    float m_cannonMovementForce = 30.0f;

    [Header("Input values")]
    [SerializeField]
    float m_horizontalSensitivity = 2.0f;
    [SerializeField]
    float m_verticalSensitivity = 2.0f;

    [Header("Effects")]
    [SerializeField]
    AudioSource m_shootFX = null;
    [SerializeField]
    AudioSource m_thrusterFX = null;
    [SerializeField]
    AudioSource m_explosionSFX = null;

    private Rigidbody m_shipRigidbody = null;
    private float m_oldHAxis = 0.0f;
    private float m_oldVAxis = 0.0f;

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
        MouseMovement();
        
        FordwardThrusters();

        BackwardThrusters();

        LowerThrusters();

        UpperThrusters();

        StrafeLeft();

        SrafeRight();

        RollLeft();

        RollRight();

        Shoot();

        CannonTorque();

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space))
        {
            if (!m_thrusterFX.isPlaying)
                m_thrusterFX.Play();

            m_shipRigidbody.drag = 0.1f;
        }
        else
        {
            m_thrusterFX.Stop();

            if (m_shipRigidbody.drag < 10.0f)
            {
                m_shipRigidbody.drag = m_shipRigidbody.drag * m_shipDecelerationFactor;
            }
        }

        m_shipRigidbody.velocity = Vector3.ClampMagnitude(m_shipRigidbody.velocity, m_maxVelocity);
    }

    void MouseMovement()
    {
        float torqueAroundY = m_horizontalSensitivity * Input.GetAxis("Mouse X");
        float torqueAroundX = m_verticalSensitivity * Input.GetAxis("Mouse Y");

        m_shipRigidbody.AddTorque(torqueAroundY * transform.up * m_thrusterForce, ForceMode.Force);
        m_shipRigidbody.AddTorque(torqueAroundX * -transform.right * m_thrusterForce, ForceMode.Force);

        if (torqueAroundY != m_oldHAxis || torqueAroundX != m_oldVAxis)
        {
            m_shipRigidbody.angularDrag = 0.1f;
            m_oldHAxis = torqueAroundY;
            m_oldVAxis = torqueAroundX;
        }
        else
        {
            if (m_shipRigidbody.angularDrag < 10.0f)
            {
                m_shipRigidbody.angularDrag = m_shipRigidbody.angularDrag * m_shipDecelerationFactor;
            }
        }
    }

    void FordwardThrusters()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_shipRigidbody.AddForce(transform.forward * m_thrusterForce, ForceMode.Force);
            m_engineTrails.SetActive(true);
        }
        else
        {
            m_engineTrails.SetActive(false);
        }
    }

    void BackwardThrusters()
    {
        if (Input.GetKey(KeyCode.S))
        {
            m_shipRigidbody.AddForce(-transform.forward * m_thrusterForce, ForceMode.Force);
            m_frontTrails.SetActive(true);
        }
        else
        {
            m_frontTrails.SetActive(false);
        }
    }

    void LowerThrusters()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            m_shipRigidbody.AddForce(transform.up * m_thrusterForce, ForceMode.Force);
            m_bottomTrails.SetActive(true);
        }
        else
        {
            m_bottomTrails.SetActive(false);
        }
    }

    void UpperThrusters()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_shipRigidbody.AddForce(-transform.up * m_thrusterForce, ForceMode.Force);
            m_upTrails.SetActive(true);
        }
        else
        {
            m_upTrails.SetActive(false);
        } 
    }

    void StrafeLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_shipRigidbody.AddForce(-transform.right * m_thrusterForce, ForceMode.Force);
            m_rightTrails.SetActive(true);
        }
        else
        {
            m_rightTrails.SetActive(false);
        }
    }

    void SrafeRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            m_shipRigidbody.AddForce(transform.right * m_thrusterForce, ForceMode.Force);
            m_leftTrails.SetActive(true);
        }
        else
        {
            m_leftTrails.SetActive(false);
        }
    }

    void RollLeft()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            m_shipRigidbody.AddTorque(transform.forward * m_thrusterForce, ForceMode.Force);
        }
    }

    void RollRight()
    {
        if (Input.GetKey(KeyCode.E))
        {
            m_shipRigidbody.AddTorque(-transform.forward * m_thrusterForce, ForceMode.Force);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Rigidbody bullet = Instantiate(m_bulletPrefab, m_weaponCannon.transform.position + 8 * m_weaponCannon.transform.forward, m_weaponCannon.transform.rotation * Quaternion.Euler(90, 0, 0));
            bullet.AddForce(m_weaponCannon.transform.forward * 50 + m_shipRigidbody.velocity, ForceMode.Impulse);
            m_shootFX.Play();
        }
    }

    void CannonTorque()
    {
        float spinWeaponAroundY = Input.GetAxis("Mouse ScrollWheel");

        m_weaponCannon.AddTorque(spinWeaponAroundY * transform.up * m_cannonMovementForce, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            Camera.main.transform.parent = null;

            m_explosionSFX.Play();

            Destroy(gameObject);
            m_menuManager.RestartGame(false);
        }
    }
}