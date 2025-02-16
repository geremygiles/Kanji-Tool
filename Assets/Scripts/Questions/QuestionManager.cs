using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    // Question Data
    [SerializeField] List<Question> questions;

    [SerializeField] Question currentQuestion;

    [SerializeField] List<int> probabilites;

    [SerializeField] Image questionImage;

    [SerializeField] GameObject optionButtons;

    [SerializeField] GameObject answerSlots;

    [SerializeField] Slot slotPrefab;

    private string randomKanji = "一七万三上下中九二五人今休何先入八六円出分前北十千午半南友右名四国土外大天女子学小山川左年後日時書月木本来東校母毎気水火父生男白百聞行西見話語読車金長間雨電食高";

    void Start()
    {
        LoadQuestion();
    }

    private void LoadQuestion() {
        // Choose a "random" question
        UpdateProbabilites();

        // Load the question
        PickQuestion();
        UpdateImage();
        UpdateSlots();

        // Load the answer(s) in a random location, with the other options filled with random kanji.
        UpdateOptions();
    }

    private void PickQuestion() {
        int random = probabilites[Random.Range(0, probabilites.Count)];

        currentQuestion = questions[random];
    }

    private void UpdateProbabilites() {
        probabilites.Clear();

        for (int index = 0; index < questions.Count; index++) {
            int questionProbability = 10 - questions[index].level;
            while (questionProbability > 0) {
                probabilites.Add(index);
                questionProbability--;
            }
        }
    }

    private void UpdateImage() {
        questionImage.sprite = currentQuestion.image;
    }

    private void UpdateOptions() {
        List<int> optionIndexes = new List<int>() {0,1,2,3,4,5};

        for (int i = 0; i < currentQuestion.kanji.Length; i++) {
            int random = Random.Range(0, optionIndexes.Count);

            Transform option = optionButtons.transform.GetChild(optionIndexes[random]).GetChild(0);
            optionIndexes.RemoveAt(random);

            option.gameObject.GetComponent<TextMeshProUGUI>().text = currentQuestion.kanji[i];
        }

        for (int i = 0; i < optionIndexes.Count; i++) {
            Transform option = optionButtons.transform.GetChild(optionIndexes[i]).GetChild(0);

            string randomKanjiChar = "";
            bool retry = true;
            while (retry) {
                string test = randomKanji[Random.Range(0, randomKanji.Length)].ToString();

                foreach (string kanji in currentQuestion.kanji) {
                    if (test == kanji) {
                        break;
                    }
                }

                retry = false;
                randomKanjiChar = test;
            }
            
            option.gameObject.GetComponent<TextMeshProUGUI>().text = randomKanjiChar;
        }
    }

    private void UpdateSlots() {
        TapHandler tapHandler = GetComponent<TapHandler>();
        switch (currentQuestion.kanji.Length) {
            case 0:
                break;
            case 1:
                Slot[] slotList = {Instantiate(slotPrefab, answerSlots.transform)};
                tapHandler.PopulateSlots(slotList);
                break;
            case 2:
                var slot21 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot21.localPosition = new Vector2(-175, 0);
                var slot22 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot22.localPosition = new Vector2(175, 0);
                Slot[] slotList2 = {slot21.GetComponent<Slot>(), slot22.GetComponent<Slot>()};
                tapHandler.PopulateSlots(slotList2);
                break;
            case 3:
                var slot31 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot31.localPosition = new Vector2(-325, 0);
                var slot32 = Instantiate(slotPrefab, answerSlots.transform);
                var slot33 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot33.localPosition = new Vector2(325, 0);
                Slot[] slotList3 = {slot31.GetComponent<Slot>(), slot32.GetComponent<Slot>(), slot33.GetComponent<Slot>()};
                tapHandler.PopulateSlots(slotList3);
                break;
            case 4:
                var slot41 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot41.localPosition = new Vector2(-525, 0);
                var slot42 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot42.localPosition = new Vector2(-175, 0);
                var slot43 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot43.localPosition = new Vector2(175, 0);
                var slot44 = Instantiate(slotPrefab, answerSlots.transform).GetComponent<RectTransform>();
                slot44.localPosition = new Vector2(525, 0);
                Slot[] slotList4 = {slot41.GetComponent<Slot>(), slot42.GetComponent<Slot>(), slot43.GetComponent<Slot>(), slot44.GetComponent<Slot>()};
                tapHandler.PopulateSlots(slotList4);
                break;
        }
    }
}
