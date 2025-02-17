using UnityEngine;

public class DetailsCloser : MonoBehaviour
{
    public void CloseDetails() {
        Destroy(gameObject.transform.parent.parent.parent.parent.gameObject);
    }
}
