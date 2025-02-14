using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This is attached to the cards that the player can tap to move them.
/// </summary>
public class TappableCardButton : MonoBehaviour
{
    // Card Dealt?
    bool dealt = false;
    Vector2 target = Vector2.zero;
    bool arrived = true;
    float speed = 100f;
    
    // TapHandler
    [SerializeField] TapHandler tapHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (tapHandler = null) {
        //    tapHandler = FindAnyObjectByType<TapHandler>();
        //}

        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived) {
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step * 10);
            if (Vector3.Distance(transform.position, target) < 0.001f) {
                // Stop moving
                transform.position = target;
                arrived = true;
            }
        }
    }

    // Called when the user taps a card button
    public void Tap() {
        // Checks Dealt
        if (!dealt) {
            // Go to first open slot
            target = tapHandler.GetOpenSlotPositon();
            arrived = false;
        }
        else {
            // Return to home slot
        }
    }
}
