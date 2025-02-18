using TMPro;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField] GameObject detailsMenuPrefab;

    public Question question;

    public int questionIndex;

    public void OpenMenu() {
        // Open the details menu
        var detailsMenu = Instantiate(detailsMenuPrefab, FindFirstObjectByType<Canvas>().transform);

        // Populate the title
        var detailsTitle = detailsMenu.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);

        string parsedWord = "";

        foreach (string item in question.kanji) {
            parsedWord += item;
        }

        detailsTitle.GetComponent<TextMeshProUGUI>().text = parsedWord;

        // Populate the text
        var detailsText = detailsMenu.transform.GetChild(0).GetChild(0).GetChild(1);

        var contents = "読み方：" + question.fullPronounciation + "\n\n" + "意味：" + question.meaning + "\n\n" + "Reviews Remaining: " + (10 - question.level) + "\n\n" + "Current Level: " + question.level;

        detailsText.GetComponent<TextMeshProUGUI>().text = contents;

        // Set up reset button
        var resetButton = detailsMenu.transform.GetChild(0).GetChild(0).GetChild(2);

        resetButton.GetComponent<ResetButton>().questionIndex = questionIndex;
    }
}
