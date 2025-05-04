using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressReport : MonoBehaviour
{

  Player player;
  public string[] minigameSceneNameList = {};
  private static ProgressReport currentProgressReport;

  void Awake()
  {
    
    // Keep the progress report across scenes and destroy any duplicate instances.
    if (currentProgressReport != null) 
    {
      Destroy(gameObject);
      return;
    }

    currentProgressReport = this;
    DontDestroyOnLoad(this);

  }

  void Start()
  {

    GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
    player = playerGameObject.GetComponent<Player>();
    player.SetLifeCount(3);

    LoadRandomMinigame();

  }

  public void MinigameEnded(Minigame minigame)
  {

    if (!minigame.isComplete) {

      Debug.Log("Player failed the level. Removing a life...");
      player.SetLifeCount(player.GetLifeCount() - 1);

    }

    LoadRandomMinigame();

  }

  void LoadRandomMinigame()
  {

    if (player.GetLifeCount() > 0) {

      Debug.Log("The player can continue. Adding to their score...");
      int newScore = player.GetScore() + 1;
      player.SetScore(newScore);

      // Check if the player's speed should be increased.
      if (newScore % 5 == 0) {
      
        player.speed += 0.5f;
        Debug.Log("Player's speed increased to: " + player.speed);
      
      }

      // Choose a random minigame.
      System.Random random = new();
      int randomIndex = random.Next(minigameSceneNameList.Length);
      string randomMinigameSceneName = minigameSceneNameList[randomIndex];
      Debug.Log("Minigame selected: " + randomMinigameSceneName);

      // Load the scene.
      SceneManager.LoadScene(randomMinigameSceneName, LoadSceneMode.Single);

    } else {

      Debug.Log("Player has lost all of their lives. Ending the game...");

    }

  }
}
