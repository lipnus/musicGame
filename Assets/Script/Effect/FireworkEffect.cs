using UnityEngine;

public class FireworkEffect : MonoBehaviour {

    public GameObject firework;
    
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("점화!");
        firework.active = true;
    }
}
 