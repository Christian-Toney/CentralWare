using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public abstract class Minigame : MonoBehaviour {

  public string task;
  public bool isComplete = false;
  public Player player;
  public UIDocument minigameTaskUIDocument;
  protected ProgressReport progressReport;
  protected readonly DateTime startTime = DateTime.Now;
  protected DateTime endTime;

  protected void Start() {

    try {

      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      progressReport = GameObject.FindGameObjectWithTag("ProgressReport").GetComponent<ProgressReport>();

      VisualElement root = minigameTaskUIDocument.rootVisualElement;
      VisualElement taskLabel = root.Q<VisualElement>("Task");
      taskLabel.Q<Label>("Task").text = task;
      StartCoroutine(AnimateTask(taskLabel));

    } catch (NullReferenceException) {

      SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }

  }

  private IEnumerator AnimateTask(VisualElement taskLabel) {

    // Shrink the task label to normal scale.
    float scaleDuration = 0.1f / player.speed;
    float scaleElapsedTime = 0.0f;
    Vector3 originalScale = taskLabel.transform.scale;
    while (scaleElapsedTime < scaleDuration) {

      scaleElapsedTime += Time.deltaTime;
      float t = Mathf.Clamp01(scaleElapsedTime / scaleDuration);
      taskLabel.transform.scale = Vector3.Lerp(originalScale, new Vector3(1, 1, 1), t);
      yield return null;

    }

    yield return new WaitForSeconds(1.0f / player.speed);

    // Fade out.
    float duration = 0.5f / player.speed;
    float elapsedTime = 0.0f;

    while (elapsedTime < duration) {

      elapsedTime += Time.deltaTime;
      float t = Mathf.Clamp01(elapsedTime / duration);
      taskLabel.Q<Label>("Task").style.opacity = Mathf.Lerp(1.0f, 0.0f, t);
      yield return null;

    }

  }

  protected abstract IEnumerator StartMinigame();

}
