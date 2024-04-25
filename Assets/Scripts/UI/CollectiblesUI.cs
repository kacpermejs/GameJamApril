using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollectiblesUI : MonoBehaviour
{
  private VisualElement root;
  private Label collectedLabel;
  private Label totalLabel;

  private void Start() {
    root = GetComponent<UIDocument>().rootVisualElement;

    var parentBar = root.Q<VisualElement>("Collectibles").Q<VisualElement>("container");

    collectedLabel = parentBar.Q<Label>("Collected");
    totalLabel = parentBar.Q<Label>("Total");
  }

  private void Update() {
    totalLabel.text = Collection.instance.numUniqueCollectibles.ToString();
    collectedLabel.text = Collection.instance.numCollected.ToString();
  }
}
