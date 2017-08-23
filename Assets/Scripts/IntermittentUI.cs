using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermittentUI : MonoBehaviour
{
    private Image m_image = null;

    private void Start()
    {
        m_image = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update ()
    {
        Color color = m_image.color;
        color.a = Random.Range(0.7f, 1.0f);
        m_image.color = color;
	}
}
