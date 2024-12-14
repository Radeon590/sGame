using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fighting.Hp;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<HpHandler> characters;
    [SerializeField] private string gameOverSceneName;

    private bool gameOverTriggered = false;

    private void Start()
    {
        // Подписываемся на событие смерти каждого персонажа
        foreach (var character in characters)
        {
            if (character != null)
            {
                character.OnDead += OnCharacterDead;
            }
        }
    }

    private void OnCharacterDead()
    {
        if (gameOverTriggered) return;
        characters.RemoveAll(character => character == null);

        foreach (var character in characters)
        {
            if (character != null && !character.IsDead)
            {
                return;
            }
        }
        
        gameOverTriggered = true;
        LoadGameOverScene();
    }


    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}
