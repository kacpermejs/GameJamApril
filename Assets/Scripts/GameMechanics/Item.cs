using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName="Collection/Item")]
public class Item : ScriptableObject {
  public int id;
  public new string name;

  public Sprite sprite;
}
