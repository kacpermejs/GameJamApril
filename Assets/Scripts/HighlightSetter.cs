using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSetter : MonoBehaviour
{
  public SpriteRenderer highlightRenderer;

  public bool isHighlighted;

  private void Start() {
    //set the same sprite
    highlightRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
    SetHightlight(isHighlighted);
  }

  public void SetHightlight(bool doHighlight) {
    isHighlighted = doHighlight;

    highlightRenderer.enabled = doHighlight;
  }
}
