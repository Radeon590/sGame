using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private GameObject musicObject; // Ссылка на объект с музыкой

    private bool isExiting = false;

    public void PlayGame()
    {
        float delay = PlayButtonSound();

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(LoadSceneAfterDelay(sceneToLoad, delay));
        }
        else
        {
            Debug.LogWarning("Сцена для загрузки не указана!");
        }
    }

    public void ExitGame()
    {
        if (isExiting) return;
        isExiting = true;

        float delay = PlayButtonSound();

        StartCoroutine(ExitAfterDelay(delay));
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

    private System.Collections.IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Удаляем объект с музыкой перед загрузкой новой сцены
        if (musicObject != null)
        {
            Destroy(musicObject);
        }

        SceneManager.LoadScene(sceneName);
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