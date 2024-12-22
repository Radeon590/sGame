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
        enemies.RemoveAll(enemy => enemy == null || enemy.IsDead);
        if (enemies.Count == 0)
        {
            gameOverTriggered = true;
            LoadVictoryScene();
        }
    }

    private void OnAllyDead()
    {
        if (gameOverTriggered) return;
        allies.RemoveAll(ally => ally == null || ally.IsDead);


        if (allies.Count == 0)
        {
            gameOverTriggered = true;
            LoadGameOverScene();
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    private void LoadVictoryScene()
    {
        SceneManager.LoadScene(victorySceneName);
    }
}
