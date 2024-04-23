using UnityEngine;

public class PlaySoundOnCollision : PlaySoundOnBase {

  private void OnCollisionEnter2D(Collision2D collision) {
    Play();
  }
}
