using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fighting.Hp;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<HpHandler> enemies;
    [SerializeField] private List<HpHandler> allies;
    [SerializeField] private string gameOverSceneName;
    [SerializeField] private string victorySceneName;

    private bool gameOverTriggered = false;

    private void Start()
    {
        // Подписываемся на событие смерти каждого врага
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.OnDead += OnEnemyDead;
            }
        }

        // Подписываемся на событие смерти каждого союзника
        foreach (var ally in allies)
        {
            if (ally != null)
            {
                ally.OnDead += OnAllyDead;
            }
        }
    }

    private void OnEnemyDead()
    {
        if (gameOverTriggered) return;

        // Убираем мертвых врагов из списка
        enemies.RemoveAll(enemy => enemy == null || enemy.IsDead);

        Debug.Log($"Enemies remaining: {enemies.Count}");

        // Если все враги мертвы, запускаем сцену победы
        if (enemies.Count == 0)
        {
            gameOverTriggered = true;
            Debug.Log("All enemies are dead. Loading victory scene...");
            LoadVictoryScene();
        }
    }

    private void OnAllyDead()
    {
        if (gameOverTriggered) return;

        // Убираем мертвых союзников из списка
        allies.RemoveAll(ally => ally == null || ally.IsDead);

        Debug.Log($"Allies remaining: {allies.Count}");

        // Если все союзники мертвы, запускаем сцену поражения
        if (allies.Count == 0)
        {
            gameOverTriggered = true;
            Debug.Log("All allies are dead. Loading game over scene...");
            LoadGameOverScene();
        }
    }

    private void LoadGameOverScene()
    {
        Debug.Log("Game Over Scene Loading...");
        SceneManager.LoadScene(gameOverSceneName);
    }

    private void LoadVictoryScene()
    {
        Debug.Log("Victory Scene Loading...");
        SceneManager.LoadScene(victorySceneName);
    }
}
