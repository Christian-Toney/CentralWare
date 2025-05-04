using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ProgressReport : MonoBehaviour {
  public string[] minigameSceneNameList = { };
  private static ProgressReport currentProgressReport;
  public Player player;
  public AudioSource fanfareSound;
  public UIDocument gameOverDocument;

  void Awake() {

    // Keep the progress report across scenes and destroy any duplicate instances.
    if (currentProgressReport != null) {
      Destroy(this);
      return;
    }

    currentProgressReport = this;
    DontDestroyOnLoad(this);

  }

  public void MinigameEnded(Minigame minigame) {

    if (!minigame.isComplete) {

      Debug.Log("Player failed the level. Removing a life...");
      player.SetLifeCount(player.GetLifeCount() - 1);

    }

    LoadRandomMinigame();

  }

  public void LoadRandomMinigame() {

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

    }
    else {

      Debug.Log("Player has lost all of their lives. Ending the game...");
      fanfareSound.Play();

      // Load the game over screen.
      StartCoroutine(LoadGameOverScreen());

    }

  }

  IEnumerator LoadGameOverScreen() {

    // Fade in the game over screen.
    gameOverDocument.enabled = true;
    VisualElement rootVisualElement = gameOverDocument.rootVisualElement;
    VisualElement window = rootVisualElement.Q("Window");

    float duration = 1.0f;
    float elapsedTime = 0.0f;

    while (elapsedTime < duration) {
      elapsedTime += Time.deltaTime;
      float t = Mathf.Clamp01(elapsedTime / duration);
      window.style.opacity = t;
      yield return null;
    }

    Label scoreLabel = rootVisualElement.Q<Label>("Score");
    scoreLabel.text = $"Your final score: {player.GetScore()}";
    Button mainMenuButton = rootVisualElement.Q<Button>("MainMenuButton");
    mainMenuButton.RegisterCallback<ClickEvent>(OnMenuButtonClick);

    VisualElement content = rootVisualElement.Q("Content");
    elapsedTime = 0.0f;
    duration = 0.5f;

    while (elapsedTime < duration) {
      elapsedTime += Time.deltaTime;
      float t = Mathf.Clamp01(elapsedTime / duration);
      content.style.opacity = t;
      yield return null;
    }

  }

  void OnMenuButtonClick(ClickEvent evt) {

    VisualElement rootVisualElement = gameOverDocument.rootVisualElement;
    VisualElement window = rootVisualElement.Q("Window");
    Button mainMenuButton = rootVisualElement.Q<Button>("MainMenuButton");
    mainMenuButton.UnregisterCallback<ClickEvent>(OnMenuButtonClick);

    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    gameOverDocument.enabled = false;

    VisualElement content = rootVisualElement.Q("Content");
    content.style.opacity = 0.0f;
    window.style.opacity = 0.0f;
    fanfareSound.Stop();

  }

}
