using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Objects references")]
    [SerializeField]
    Animator m_cameraAnimator = null;
    [SerializeField]
    Animator m_HUDAnimator = null;
    [SerializeField]
    ShipController m_playerController = null;
    [SerializeField]
    Rigidbody m_playerRigidBody = null;
    [SerializeField]
    Grab m_playerGrab = null;
    [SerializeField]
    Transform m_listOfPlanets = null;

    [Header("UI references")]
    [SerializeField]
    ShipStartMenu m_shipStartMenu = null;
    [SerializeField]
    Canvas m_startMenu = null;
    [SerializeField]
    GameObject m_menuButtons = null;
    [SerializeField]
    GameObject m_tutorialPanel = null;
    [SerializeField]
    GameObject m_tutorialBackButton = null;
    [SerializeField]
    GameObject m_title = null;
    [SerializeField]
    GameObject m_HUD = null;
    [SerializeField]
    GameObject m_gameOverText = null;
    [SerializeField]
    GameObject m_gameWonText = null;
    [SerializeField]
    Text m_velocityIndicator = null;
    [SerializeField]
    Text m_grabbingToolStatus = null;
    [SerializeField]
    Text m_planetsLeftIndicator = null;
    [SerializeField]
    Text m_timeInSecondsText = null;
    [SerializeField]
    Text m_finalScore = null;
    [SerializeField]
    GameObject m_destroyPlanetIndicator = null;

    private Transform m_playerStartPosition = null;
    private bool m_timerIsActivated = false;
    private float m_time = 0.0f;
    public int m_timeInSeconds = 0;

    private void Start()
    {
        m_playerStartPosition = m_playerController.transform;
    }

    private void Update()
    {
        if (m_timerIsActivated)
        {
            m_time += Time.deltaTime;
            m_timeInSeconds = (int) m_time;
            m_timeInSecondsText.text = m_timeInSeconds.ToString() + " s";
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !m_startMenu.enabled)
        {
            ReloadScene();
        }

        if (m_listOfPlanets.childCount <= 0)
        {
            RestartGame(true);
        }

        if (m_playerRigidBody != null)
            m_velocityIndicator.text = ((int)m_playerRigidBody.velocity.magnitude).ToString();

        m_planetsLeftIndicator.text = m_listOfPlanets.childCount.ToString();

        if (m_playerController != null)
        {
            if (m_playerGrab.m_canGrab)
            {
                if (m_playerGrab.m_destroyPlanetCondition)
                {
                    m_destroyPlanetIndicator.SetActive(false);
                    m_grabbingToolStatus.text = "Active";
                    m_grabbingToolStatus.color = Color.green;
                }
                else
                {
                    m_destroyPlanetIndicator.SetActive(true);
                }
            }
            else
            {
                m_destroyPlanetIndicator.SetActive(false);
                m_grabbingToolStatus.text = "Not Active";
                m_grabbingToolStatus.color = Color.red;
            }
        }
    }

    public void StartGame()
    {
        Camera.main.transform.parent = m_playerController.transform;
        m_cameraAnimator.SetBool("hasStarted", true);
        m_shipStartMenu.m_disable = true;
        m_title.SetActive(false);
        m_menuButtons.SetActive(false);
        m_velocityIndicator.enabled = true;
        m_playerController.enabled = true;
        m_playerGrab.enabled = true;
        m_HUD.SetActive(true);
        m_HUDAnimator.SetBool("hasStarted", true);
        Cursor.lockState = CursorLockMode.Locked;
        m_timerIsActivated = true;
    }

    public void RestartGame(bool hasWon)
    {
        m_HUD.SetActive(false);

        m_timerIsActivated = false;

        if (hasWon)
        {
            m_gameWonText.SetActive(true);
            m_finalScore.text = m_timeInSeconds.ToString() + " seconds";
        }
        else
        {
            m_gameOverText.SetActive(true);
        }
        StartCoroutine(WaitAndRestart());
    }

    public void ShowTutorial()
    {
        m_menuButtons.SetActive(false);
        m_tutorialPanel.SetActive(true);
        m_tutorialBackButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        m_menuButtons.SetActive(true);
        m_tutorialPanel.SetActive(false);
        m_tutorialBackButton.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
