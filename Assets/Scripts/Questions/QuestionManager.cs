using System.Collections.Generic;
using TMPro;
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

    private string randomKanji = "一七万三上下中九二五人今休何先入八六円出分前北十千午半南友右名四国土外大天女子学小山川左年後日時書月木本来東校母毎気水火父生男白百聞行西見話語読車金長間雨電食高";

    private void LoadQuestion() {
        // Choose a "random" question
        UpdateProbabilites();

        // Load the question
        PickQuestion();
        UpdateImage();

        // Load the answer(s) in a random location

        // Load some extra answers
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
        List<int> optionIndexes = new List<int>() {1,2,3,4,5,6};

        for (int i = 0; i < currentQuestion.kanji.Length; i++) {
            int random = Random.Range(0, optionIndexes.Count);

            Transform option = optionButtons.transform.GetChild(optionIndexes[random]).GetChild(0);
            optionIndexes.Remove(random);

            option.gameObject.GetComponent<TextMeshProUGUI>().text = currentQuestion.kanji[i];
        }

        for (int i = 0; i < optionIndexes.Count; i++) {
            Transform option = optionButtons.transform.GetChild(optionIndexes[i]).GetChild(0);
            optionIndexes.Remove(i);

            option.gameObject.GetComponent<TextMeshProUGUI>().text = randomKanji[Random.Range(0, randomKanji.Length)].ToString();
        }

    }
}
