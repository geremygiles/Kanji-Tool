using TMPro;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    GameData gameData;

    public int questionIndex;

    void Start()
    {
        gameData = FindFirstObjectByType<GameData>();
    }

    public void ResetQuestion()
    {
        gameData.ResetQuestion(questionIndex);

        string parsedPronounciation = "";

        foreach (string item in gameData.questions[questionIndex].pronounciation) {
            parsedPronounciation += item;
        }

        Question question = gameData.questions[questionIndex];

        gameObject.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>().text = "読み方：" + parsedPronounciation + "\n\n" + "意味：" + question.meaning + "\n\n" + "Reviews Remaining: " + (10 - question.level) + "\n\n" + "Current Level: " + question.level;
    
        FindFirstObjectByType<StatsLoader>().ReloadData();
    }

    public void ResetAll() {
        foreach (Question question in gameData.questions) {
            question.level = 0;
            question.triesRemaining = 2;
        }

        FindFirstObjectByType<StatsLoader>().ReloadData();
    }
}
