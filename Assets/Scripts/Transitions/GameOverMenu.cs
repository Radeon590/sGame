using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private string playScene;
    [SerializeField] private string mainMenuScene;
    [SerializeField] private AudioClip buttonSound;

    private bool isProcessing = false;

    public void RestartGame()
    {
        if (!isProcessing)
        {
            isProcessing = true;
            float delay = PlayButtonSound();
            StartCoroutine(LoadSceneAfterDelay(playScene, delay));
        }
    }

    public void GoToMainMenu()
    {
        if (!isProcessing)
        {
            isProcessing = true;
            float delay = PlayButtonSound();
            StartCoroutine(LoadSceneAfterDelay(mainMenuScene, delay));
        }
    }

    private float PlayButtonSound()
    {
        if (buttonSound != null)
        {
            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position);
            return buttonSound.length;
        }
        return 0f;
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Сцена не указана!");
            isProcessing = false;
        }
    }
}
