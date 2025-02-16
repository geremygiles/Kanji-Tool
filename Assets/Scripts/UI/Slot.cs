using UnityEngine;

public class Slot : MonoBehaviour
{

    // Slot Empty
    private bool empty = true;

    private TappableCardButton currentCard = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Public Methods

    public bool IsEmpty() {
        return currentCard == null;
    }

    public void AddCard(TappableCardButton card) {
        currentCard = card;
    }

    public void ClearCard() {
        currentCard = null;
    }

    public TappableCardButton GetCard() {
        return currentCard;
    }

    #endregion
}
