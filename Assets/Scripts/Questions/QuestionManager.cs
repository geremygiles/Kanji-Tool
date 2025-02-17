using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    // Question Data
    GameData gameData;

    [SerializeField] Question currentQuestion;

    [SerializeField] List<int> probabilites;

    [SerializeField] Image questionImage;

    [SerializeField] TextMeshProUGUI upperText;

    [SerializeField] public GameObject optionButtons;

    [SerializeField] GameObject answerSlots;

    [SerializeField] Slot slotPrefab;

    [SerializeField] Button checkButton;

    [SerializeField] Button idkButton;

    [SerializeField] Button nextButton;

    TapHandler tapHandler;

    bool questionComplete = false;

    private string randomKanji = "一七万三上下中九二五人今休何先入八六円出分前北十千午半南友右名四国土外大天女子学小山川左年後日時書月木本来東校母毎気水火父生男白百聞行西見話語読車金長間雨電食高";


    void Awake()
    {
        gameData = FindFirstObjectByType<GameData>();
        tapHandler = GetComponent<TapHandler>();
    }
    void Start()
    {
        // Stopping if on Stats screen
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Stats") return;
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

        questionComplete = false;
        ClearUpperText();
        UpdateBottomButtons();
    }

    private void ClearUpperText() {
        upperText.text = "";
    }

    private void PickQuestion() {
        int random = probabilites[Random.Range(0, probabilites.Count)];

        currentQuestion = gameData.questions[random];

        currentQuestion.triesRemaining = 2;
    }

    private void UpdateProbabilites() {
        probabilites.Clear();

        for (int index = 0; index < gameData.questions.Count; index++) {
            int questionProbability = 10 - gameData.questions[index].level;
            while (questionProbability > 0) {
                probabilites.Add(index);
                questionProbability--;
            }
        }

        if (probabilites.Count <= 0) {
            FindFirstObjectByType<SceneManager>().Quit();
        }
    }

    private void UpdateImage() {
        questionImage.sprite = currentQuestion.image;
    }

    private void UpdateOptions() {
        List<int> optionIndexes = new List<int>() {0,1,2,3,4,5};

        for (int i = 0; i < currentQuestion.kanji.Length; i++) {
            int random = Random.Range(0, optionIndexes.Count);

            Transform option = optionButtons.transform.GetChild(optionIndexes[random]);
            optionIndexes.RemoveAt(random);

            option.GetComponent<TappableCardButton>().SetValue(currentQuestion.kanji[i]);
        }

        List<string> usedCharacters = new();

        for (int i = 0; i < optionIndexes.Count; i++) {
            Transform option = optionButtons.transform.GetChild(optionIndexes[i]);

            string randomKanjiChar = "";
            bool retry = true;
            while (retry) {
                retry = false;
                string test = randomKanji[Random.Range(0, randomKanji.Length)].ToString();

                foreach (string kanji in currentQuestion.kanji) {
                    if (test == kanji) {
                        retry = true;
                    }
                }

                foreach (string kanji in usedCharacters) {
                    if (test == kanji) {
                        retry = true;
                    }
                }

                randomKanjiChar = test;
            }
            
            option.GetComponent<TappableCardButton>().SetValue(randomKanjiChar);
            usedCharacters.Add(randomKanjiChar);
        }
    }

    private void UpdateSlots() {
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

    private void UpdateBottomButtons() {
        if (questionComplete) {
            checkButton.gameObject.SetActive(false);
            idkButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
        else {
            nextButton.gameObject.SetActive(false);
            checkButton.gameObject.SetActive(true);
            idkButton.gameObject.SetActive(true);
        }
    }

    private void UpdateQuestionLevel(bool increase) {
        if (increase) {
            currentQuestion.level++;
        }
        else {
            currentQuestion.level--;
        }
    }

    private void AddFurigana() {
        Slot[] slots = tapHandler.GetSlots();
        for (int i = 0; i < slots.Length; i++) {
            slots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentQuestion.pronounciation[i];
        }
    }

    public void CheckAnswer() {
        Slot[] slots = tapHandler.GetSlots();

        // Check Empty
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].GetCard() == null) {
                return;
            }
        }

        bool answerCorrect = true;

        // Assign colors and disable
        for (int i = 0; i < slots.Length; i++) {
            TappableCardButton card = slots[i].GetCard();
            if (card.GetValue() == currentQuestion.kanji[i]) {
                Debug.Log("correct~!");
                card.GetComponent<Image>().color = new Color32(77,173,76,255);
                card.SetTappable(false);
            }
            else if (currentQuestion.kanji.Contains(card.GetValue())) {
                Debug.Log("Wrong Location...");
                card.GetComponent<Image>().color = new Color32(240,242,78,255);

                if (currentQuestion.triesRemaining > 1) {
                    upperText.text = "もう一度...";
                    currentQuestion.level++;
                    answerCorrect = false;
                }

                else {
                    upperText.text = "";
                    foreach (string kanji in currentQuestion.kanji) {
                        upperText.text += kanji;
                    }

                    upperText.text += "\n";

                    foreach (string section in currentQuestion.pronounciation) {
                        upperText.text += section;
                    }
                    
                
                    answerCorrect = false;
                    card.SetTappable(false);
                    questionComplete = true;

                    UpdateBottomButtons();
                    
                }
            }
            else {
                Debug.Log("WRONG!"); 
                card.GetComponent<Image>().color = new Color32(212, 53, 53, 255);

                if (currentQuestion.triesRemaining > 1) {
                    upperText.text = "もう一度...";
                    currentQuestion.level++;
                    answerCorrect = false;
                }
                else {
                    upperText.text = "";
                    foreach (string kanji in currentQuestion.kanji) {
                        upperText.text += kanji;
                    }

                    upperText.text += "\n";

                    foreach (string section in currentQuestion.pronounciation) {
                        upperText.text += section;
                    }
                    
                
                    answerCorrect = false;
                    card.SetTappable(false);
                    questionComplete = true;

                    UpdateBottomButtons();
                }
            }
        }
        
        if (answerCorrect) {
            questionComplete = true;
            UpdateBottomButtons();
            upperText.text = "よくできた！";
            AddFurigana();
        }
        else {
            currentQuestion.triesRemaining--;
            questionComplete = false;
        }

        UpdateQuestionLevel(answerCorrect);
        
    }

    public void DeleteSlots() {
        for (int i = answerSlots.transform.childCount - 1; i >= 0; i--) {
            Destroy(answerSlots.transform.GetChild(i).gameObject);
        }
    }

    public void Next() {
        tapHandler.ClearSlots();
        LoadQuestion();
    }
}
