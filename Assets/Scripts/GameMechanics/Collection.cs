using UnityEngine;
using System.Collections.Generic;

public class Collection : MonoBehaviour {

  public static Collection instance;

  public HashSet<int> collectedItemIds = new HashSet<int>();
  public List<int> collectedItemIdsList = new List<int>();
  public int numUniqueCollectibles;

  public int numCollected { get => collectedItemIds.Count; }

  private void Awake() {
    if (instance == null) {
      instance = this;
    }
  }

  private void Start() {
    HashSet<int> itemTypes = new HashSet<int>();

    GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

    foreach (GameObject gameObject in collectibles) {
      var collectible = gameObject.GetComponent<Collectible>();
      if (itemTypes.Contains(collectible.item.id)) {
        continue;
      }

      itemTypes.Add(collectible.item.id);
    }

    numUniqueCollectibles = itemTypes.Count;
  }

  public void TryAddToCollection(Item item, out bool success) {
    if (collectedItemIds.Contains(item.id)) {
      success = false;
      return;
    }
    success = true;
    collectedItemIds.Add(item.id);
    collectedItemIdsList.Add(item.id);
    DisableSameItems(item);

  }

  private void DisableSameItems(Item item) {
    int itemId = item.id;

    GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

    foreach (GameObject collectible in collectibles) {
      Collectible itemCollectible = collectible.GetComponent<Collectible>();
      if (itemCollectible && itemCollectible.item.id == itemId) {
        itemCollectible.enabled = false;
      }
    }
  }

}
