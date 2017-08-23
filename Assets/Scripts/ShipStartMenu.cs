using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStartMenu : MonoBehaviour
{
    [Header("Gameplay values")]
    [SerializeField]
    private float m_thrusterForce = 10.0f;

    [HideInInspector]
    public bool m_disable = false;

    private Rigidbody m_shipRigidbody = null;
    private bool m_repeatCondition = false;

    private void Start()
    {
        m_shipRigidbody = GetComponent<Rigidbody>();
        InvokeRepeating("Wait", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (m_disable)
        {
            CancelInvoke();
        }
    }

    public void Wait()
    {
        if (!m_repeatCondition)
        {
            m_shipRigidbody.velocity = -m_shipRigidbody.velocity;
            m_shipRigidbody.AddTorque(transform.forward * m_thrusterForce, ForceMode.Force);
            m_repeatCondition = true;
        }
        else
        {
            m_shipRigidbody.velocity = -m_shipRigidbody.velocity;
            m_shipRigidbody.AddTorque(-transform.forward * m_thrusterForce, ForceMode.Force);
            m_repeatCondition = false;
        }
    }
}
