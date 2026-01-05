using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Death")]
    public AudioSource deathAudio;
    [SerializeField] private GameObject deathPanel;
    public float restartDelay = 3f;

    [Header("UI")]
    public GameObject winPanel;

    private bool isDead = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void KillPlayer(GameObject player)
    {
        if (isDead) return;
        isDead = true;

        if (deathAudio != null)
            deathAudio.Play();

        player.SetActive(false);
        deathPanel.SetActive(true);

        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
