using UnityEngine;

public class PlaySoundOnCollision : PlaySoundWithCooldown {

  private void OnCollisionEnter2D(Collision2D collision) {
    Play();
  }
}
