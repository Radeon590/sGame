using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Переменная для указания сцены через инспектор
    [SerializeField] private string sceneToLoad;

    // Функция для загрузки сцены
    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Сцена для загрузки не указана!");
        }
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
