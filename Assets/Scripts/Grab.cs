using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    [Header("Gameplay values")]
    [SerializeField]
    float m_grabDistance = 50.0f;
    [SerializeField]
    float m_pointToReach = 30.0f;
    [SerializeField]
    float m_pullForce = 1;

    [Header("Effects")]
    [SerializeField]
    ParticleSystem m_particleSystem = null;
    [SerializeField]
    AudioSource m_forceFX = null;

    [HideInInspector]
    public bool m_canGrab = false;
    [HideInInspector]
    public bool m_destroyPlanetCondition = false;

    // Use this for initialization
    void Start ()
    {
        ParticleSystem.EmissionModule emission = m_particleSystem.emission;
        emission.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 fwd = transform.forward;
        ParticleSystem.EmissionModule emission = m_particleSystem.emission;
        ParticleSystem.MainModule particleSettings = m_particleSystem.main;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, m_grabDistance) && hit.transform.tag == "Planet")
        {
            m_canGrab = true;

            if (hit.transform.childCount == 0)
            {
                m_destroyPlanetCondition = true;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (!m_forceFX.isPlaying)
                        m_forceFX.Play();

                    particleSettings.startColor = Color.blue;

                    emission.enabled = true;

                    hit.transform.GetComponent<Rigidbody>().AddForce(-fwd * m_pullForce * Time.fixedDeltaTime);
                }

                if (Input.GetKey(KeyCode.Mouse1))
                {
                    if (!m_forceFX.isPlaying)
                        m_forceFX.Play();

                    particleSettings.startColor = Color.red;

                    emission.enabled = true;

                    hit.transform.GetComponent<Rigidbody>().AddForce(fwd * m_pullForce * Time.fixedDeltaTime);
                }
            } 
        }
        else
        {
            m_canGrab = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Mouse0) || !m_canGrab)
        {
            m_forceFX.Stop();
            emission.enabled = false;
        }
    }
}
