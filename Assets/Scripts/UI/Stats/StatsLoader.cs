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
    }

    private void LoadData() {
        var questions = FindFirstObjectByType<GameData>().questions;

        for (int i = 0; i < questions.Count; i++) {
            var item = Instantiate(itemPrefab, transform);

            item.GetComponent<SlotItem>().question = questions[i];
            item.GetComponent<SlotItem>().questionIndex = i;

            // Set Kanji
            var kanjiText = item.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            kanjiText.text = "";

            foreach (string character in questions[i].kanji) {
                kanjiText.text += character;
            }

            // Set Level
            var levelText = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            levelText.text = "Reviews Remaining: " + (10 - questions[i].level);
            
        }
    }

    public void ReloadData() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            Destroy(transform.GetChild(i).gameObject);
        }

        LoadData();
    }
}
