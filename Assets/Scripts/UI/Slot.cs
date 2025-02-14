using UnityEngine;

public class Slot : MonoBehaviour
{

    // Slot Empty
    private bool empty = true;

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
        return empty;
    }

    public void ToggleEmpty() {
        empty = !empty;
    }

    #endregion
}
