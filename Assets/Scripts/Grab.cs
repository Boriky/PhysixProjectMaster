using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public float m_grabDistance = 50.0f;
    public float m_pointToReach = 30.0f;
    public float m_pullForce = 1;
    public ParticleSystem m_particleSystem = null;

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

        if (Input.GetKey(KeyCode.Mouse0) && Physics.Raycast(transform.position, fwd, out hit, m_grabDistance))
        {
            if(hit.transform.tag == "Planet" && hit.transform.childCount == 0)
            {
                particleSettings.startColor = Color.blue;

                emission.enabled = true;

                hit.transform.GetComponent<Rigidbody>().AddForce(-fwd * m_pullForce * Time.fixedDeltaTime);
            }
        }

        if (Input.GetKey(KeyCode.Mouse1) && Physics.Raycast(transform.position, fwd, out hit, m_grabDistance))
        {
            if (hit.transform.tag == "Planet" && hit.transform.childCount == 0)
            {
                particleSettings.startColor = Color.red;

                emission.enabled = true;

                hit.transform.GetComponent<Rigidbody>().AddForce(fwd * m_pullForce * Time.fixedDeltaTime);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            emission.enabled = false;
        }
    }
}




/*print("Planet in sight!");

if (Physics.Raycast(transform.position, fwd, out hit, m_pointToReach))
{
    hit.transform.GetComponent<Rigidbody>().AddForce(fwd * m_pullForce * Time.fixedDeltaTime);
}*/
