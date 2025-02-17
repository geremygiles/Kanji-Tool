using TMPro;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField] GameObject detailsMenuPrefab;

    public Question question;

    public void OpenMenu() {
        // Open the details menu
        var detailsMenu = Instantiate(detailsMenuPrefab);

        // Populate the text
        var detailsText = detailsMenu.transform.GetChild(0).GetChild(0).GetChild(1);

        string parsedPronounciation = "";

        foreach (string item in question.pronounciation) {
            parsedPronounciation += item;
        }

        var contents = "読み方：" + parsedPronounciation + "\n\n" + "意味：" + question.meaning + "\n\n" + "Reviews Remaining: " + (10 - question.level) + "\n\n" + "Current Level: " + question.level;

        detailsText.GetComponent<TextMeshProUGUI>().text = contents;
    }
}
