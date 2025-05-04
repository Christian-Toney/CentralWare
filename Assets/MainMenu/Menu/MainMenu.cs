using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {

  public static Player player;
  public static ProgressReport progressReport;

  void Start() {

    Screen.SetResolution(1920, 1080, true);

    if (progressReport == null) {

      progressReport = GameObject.FindGameObjectWithTag("ProgressReport").GetComponent<ProgressReport>();
    
    }

    if (player == null) {

      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    if (TryGetComponent<UIDocument>(out var uiDocument)) {
      // Get the root visual element
      VisualElement root = uiDocument.rootVisualElement;

      // Find the button by name and add a click event listener
      Button startButton = root.Q<Button>("PlayButton");
      startButton.RegisterCallback<ClickEvent>(ev => {
        
        player.SetLifeCount(3);
        player.SetScore(0);
        progressReport.LoadRandomMinigame();

      });

      Button exitButton = root.Q<Button>("ExitButton");
      exitButton.RegisterCallback<ClickEvent>(ev => {

        Application.Quit();

      });
    }

  }
}
