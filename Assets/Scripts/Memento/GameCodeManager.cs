using Assets.Scripts.Memento;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCodeManager : MonoBehaviour
{
    private GameState gState;
    private GameMemento gMemento;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject saveLoadMenu;
    private bool isMenuActive = false;

    private void Start()
    {
        if (saveLoadMenu != null)
        {
            saveLoadMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowSaveLoadMenu();
        }
    }
    public void SaveGame()
    {
        Vector3 playerPosition = player.transform.position;
        float playerHealth = player.GetComponent<PlayerHealth>().health.fillAmount;
        int currentWave = EnemySpawner.Instance?.GetCurrentWave() ?? 1;
        List<Vector3> enemyPositions = EnemySpawner.Instance?.GetEnemyPositions();

        gState = new GameState(playerPosition, playerHealth, currentWave, enemyPositions);
        gMemento = new GameMemento(gState);

        Debug.Log("Игра сохранена");
    }

    public void LoadGame()
    {
        if (gState != null)
        {
            GameState state = gMemento.State;

            player.transform.position = state.PlayerPosition;
            player.GetComponent<PlayerHealth>().SetCurrentHealth(state.PlayerHealth);

            for (int i = 0; i < EnemySpawner.Instance.enemySpawns.Count; i++)
            {
                if (i < state.EnemyPosition.Count)
                {
                    EnemySpawner.Instance.enemySpawns[i].transform.position = state.EnemyPosition[i];
                }
            }

            Debug.Log("Игра загружена.");
        }
        else
        {
            Debug.Log("Нет сохранений игры.");
        }
    }
    public void ShowSaveLoadMenu()
    {
        {
            isMenuActive = !isMenuActive;

            if (saveLoadMenu != null)
            {
                saveLoadMenu.SetActive(isMenuActive);
            }

            Time.timeScale = isMenuActive ? 0f : 1f;
        }
    }
    public void OnSaveButtonClick()
    {
        SaveGame();
    }

    public void OnLoadButtonClick()
    {
        LoadGame();
        saveLoadMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
