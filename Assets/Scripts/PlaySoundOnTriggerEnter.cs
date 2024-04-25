using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTriggerEnter : PlaySoundWithCooldown {

  private void OnTriggerEnter2D(Collider2D other) {
    Play();
  }
}
