using UnityEngine;

public class TapHandler : MonoBehaviour
{

    #region UI Objects

    // Slots
    [SerializeField] Slot[] slots;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Private Methods

    /// <summary>
    /// Returns position data for a particular slot
    /// </summary>
    /// <param name="slotIndex">Slot to return</param>
    /// <returns>Vector2 holding x and y pos</returns>
    private Vector2 GetSlotPosition(int slotIndex) {
        Vector2 slotPos = new Vector2(
            slots[slotIndex].transform.position.x,
            slots[slotIndex].transform.position.y);
        return slotPos;
    }

    /// </summary>
    /// <param name="slotIndex">Slot to return</param>
    private Slot GetSlot(int slotIndex) {
        return slots[slotIndex];
    }

    #endregion

    #region Public Methods

    public void PopulateSlots(Slot[] slotList) {
        slots = slotList;
    }

    public void ClearSlots() {
        foreach (TappableCardButton card in GetComponent<QuestionManager>().optionButtons.GetComponentsInChildren<TappableCardButton>()) {
            if (card.CheckDealt()) {
                card.ReturnHome(true);
            }
        }
        GetComponent<QuestionManager>().DeleteSlots();
        slots = null;
    }
    
    public void ClearSlot() {
        foreach (TappableCardButton card in GetComponent<QuestionManager>().optionButtons.GetComponentsInChildren<TappableCardButton>()) {
            if (card.CheckDealt()) {
                card.ReturnHome(true);
            }
        }
    }

    public Slot GetOpenSlot() {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].IsEmpty()) {
                return GetSlot(i);
            }
        }

        return null;
    }

    public Slot[] GetSlots() {
        return slots;
    }

    public Slot GetSlot() {
        return slots[0];
    }

    #endregion    
}
