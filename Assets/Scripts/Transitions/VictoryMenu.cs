using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    [SerializeField] private AudioClip buttonSound;
    private bool isExiting = false;
    private bool isProcessing = false;
    private float PlayButtonSound()
    {
        if (buttonSound != null)
        {
            AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position);
            return buttonSound.length;
        }
        return 0f;
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
    public void ExitGame()
    {
        if (isExiting) return;
        isExiting = true;

        float delay = PlayButtonSound();

        StartCoroutine(ExitAfterDelay(delay));
    }
    private System.Collections.IEnumerator ExitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
