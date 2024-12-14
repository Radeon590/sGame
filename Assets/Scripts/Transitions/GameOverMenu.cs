using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Переменные для указания сцен через инспектор
    [SerializeField] private string playScene;  // Сцена для новой игры
    [SerializeField] private string mainMenuScene; // Сцена главного меню

    // Функция для загрузки сцены с игрой
    public void RestartGame()
    {
        if (!string.IsNullOrEmpty(playScene))
        {
            SceneManager.LoadScene(playScene);
        }
        else
        {
            Debug.LogWarning("Сцена с игрой не указана!");
        }
    }

    // Функция для загрузки сцены главного меню
    public void GoToMainMenu()
    {
        if (!string.IsNullOrEmpty(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
        else
        {
            Debug.LogWarning("Сцена главного меню не указана!");
        }
    }
}
