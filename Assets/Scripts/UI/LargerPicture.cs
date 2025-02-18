using UnityEngine;
using UnityEngine.UI;

public class LargerPicture : MonoBehaviour
{

    [SerializeField] GameObject largerPictureMenuPrefab;

    public void OpenMenu() {
        // Open the details menu
        var largerPictureMenu = Instantiate(largerPictureMenuPrefab, FindFirstObjectByType<Canvas>().transform);

        // Populate the image
        var largeImage = largerPictureMenu.transform.GetChild(0).GetChild(0).GetChild(1);

        largeImage.GetComponent<Image>().sprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
    }
}
