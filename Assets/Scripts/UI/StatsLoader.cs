using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsLoader : MonoBehaviour
{
    [SerializeField] QuestionManager qmPrefab;
    [SerializeField] GameObject itemPrefab;

    QuestionManager questionManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questionManager = Instantiate(qmPrefab);

        LoadData();
        LoadData();
        LoadData();
    }

    private void LoadData() {
        foreach (Question question in FindFirstObjectByType<GameData>().questions) {
            var item = Instantiate(itemPrefab, transform);

            // Set Kanji
            var kanjiText = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            kanjiText.text = "";

            foreach (string character in question.kanji) {
                kanjiText.text += character;
            }

            // Set Level
            var levelText = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            levelText.text = "Reviews Remaining: " + (10 - question.level);
        }
    }
}
