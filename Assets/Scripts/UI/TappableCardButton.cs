using UnityEngine;

/// <summary>
/// This is attached to the cards that the player can tap to move them.
/// </summary>
public class TappableCardButton : MonoBehaviour
{
    // Card Dealt?
    bool dealt = false;

    // TapHandler
    [SerializeField] TapHandler tapHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tapHandler = null) {
            tapHandler = FindAnyObjectByType<TapHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when the user taps a card button
    public void Tap() {
        // Checks Dealt
        if (!dealt) {
            // Go to first open slot
            
        }
        else {
            // Return to home slot
        }
    }
}
