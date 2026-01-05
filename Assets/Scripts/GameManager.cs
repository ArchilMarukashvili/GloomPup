using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private GameObject deathPanel;
    




[Header("UI")]
public GameObject winPanel;





    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void KillPlayer(GameObject player)
    {
        player.SetActive(false);
        deathPanel.SetActive(true);

        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
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

    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
    );
}


public void QuitGame()
{
    Application.Quit();
}



}
