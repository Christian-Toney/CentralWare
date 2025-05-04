using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {

  public Player player;
  public ProgressReport progressReport;

  void Start() {

    if (TryGetComponent<UIDocument>(out var uiDocument)) {
      // Get the root visual element
      VisualElement root = uiDocument.rootVisualElement;

      // Find the button by name and add a click event listener
      Button startButton = root.Q<Button>("PlayButton");
      startButton.RegisterCallback<ClickEvent>(ev => {

        progressReport.enabled = true;

      });

      Button exitButton = root.Q<Button>("ExitButton");
      exitButton.RegisterCallback<ClickEvent>(ev => {

        Application.Quit();

      });
    }

  }
}
