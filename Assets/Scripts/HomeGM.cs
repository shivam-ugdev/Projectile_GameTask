using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeGM : MonoBehaviour
{
    public GameObject hTPP;

    private void Start()
    {
        hTPP.SetActive(false);
    }
    public void FreeThrow()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void PrecisionMode()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;

    }

    public void HomePage()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;

    }

    public void HowToPlayOpen()
    {
        hTPP.SetActive(true);
    }
    public void HowToPlayClose()
    {
        hTPP.SetActive(false);
    }
}
