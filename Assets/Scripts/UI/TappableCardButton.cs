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

    // Original Location
    Vector2 origin;
    bool arrived = true;
    float speed = 200f;

    Slot currentSlot = null;
    
    // TapHandler
    [SerializeField] TapHandler tapHandler;


    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (tapHandler = null) {
        //    tapHandler = FindAnyObjectByType<TapHandler>();
        //}

        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!arrived) {
            var step = speed;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
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
            currentSlot = tapHandler.GetOpenSlot();
            if (currentSlot != null) {
                target = currentSlot.transform.position;
            arrived = false;
            dealt = true;
            currentSlot.ToggleEmpty();
            }
        }
        else {
            // Return to home slot
            target = origin;
            arrived = false;
            dealt = false;
            currentSlot.ToggleEmpty();
            currentSlot = null;
        }
    }
}
