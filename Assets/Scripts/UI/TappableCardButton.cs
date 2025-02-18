using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is attached to the cards that the player can tap to move them.
/// </summary>
public class TappableCardButton : MonoBehaviour
{
    // Card Dealt?
    bool dealt = false;
    Vector2 target = Vector2.zero;

    bool tappable = true;

    // Original Location
    Vector2 origin;
    bool arrived = true;

    float speed = 200f;

    Slot currentSlot = null;

    string currentValue;

    Sprite currentSprite;
    
    // TapHandler
    [SerializeField] TapHandler tapHandler;

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
        if (!tappable) return;

        // Checks Dealt
        if (!dealt) {
            // Go to first open slot
            currentSlot = tapHandler.GetOpenSlot();
            if (currentSlot != null) {
                target = currentSlot.transform.position;
                currentSlot.AddCard(this);
                arrived = false;
                dealt = true;
            }
        }
        else
        {
            // Return to home slot
            ReturnHome(false);
        }
    }

    public void ReturnHome(bool instant) {
        if (instant) {
            transform.position = origin;
            arrived = true;
        }
        else {
            target = origin;
            arrived = false;
        }
        currentSlot.ClearCard();
        dealt = false;
        currentSlot = null;
    }

    public bool CheckDealt() {
        return dealt;
    }

    public void SetValue(string val) {
        currentValue = val;
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentValue;
        SetTappable(true);
        GetComponent<Image>().color = Color.white;
    }

    public void SetValue(Sprite sprite) {
        currentSprite = sprite;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        SetTappable(true);
        GetComponent<Image>().color = Color.white;
    }

    public string GetValue() {
        return currentValue;
    }

    public Sprite GetImageValue(){
        return currentSprite;
    }
    

    public void SetTappable(bool value) {
        tappable = value;
    }
}
