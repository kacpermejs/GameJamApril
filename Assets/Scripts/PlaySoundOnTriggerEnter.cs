using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTriggerEnter : PlaySoundOnBase {

  private void OnTriggerEnter2D(Collider2D other) {
    Play();
  }
}
