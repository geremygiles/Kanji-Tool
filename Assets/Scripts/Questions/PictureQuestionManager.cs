using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PictureQuestionManager : MonoBehaviour
{
    // Question Data
    GameData gameData;

    [SerializeField] Question currentQuestion;

    [SerializeField] List<int> probabilites;

    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] TextMeshProUGUI upperText;

    [SerializeField] TextMeshProUGUI pronounciationText;

    [SerializeField] public GameObject optionButtons;

    private TappableCardButton correctCard;

    [SerializeField] GameObject answerSlot;

    [SerializeField] Button checkButton;

    [SerializeField] Button idkButton;

    [SerializeField] Button nextButton;

    AudioSource audioSource;

    [SerializeField] AudioClip[] clips;

    TapHandler tapHandler;

    bool questionComplete = false;

    [SerializeField] private Sprite[] allPics;

    List<Sprite> usedImages;

    int switchRandom = 1;

    void Awake()
    {
        gameData = FindFirstObjectByType<GameData>();
        tapHandler = GetComponent<TapHandler>();
    }
    void Start()
    {
        // Stopping if on Stats screen
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Stats") return;
        audioSource = FindFirstObjectByType<AudioSource>();
        LoadQuestion();
    }

    private void LoadQuestion() {
        // Choose a "random" question
        UpdateProbabilites();

        if (probabilites.Count > 0 && switchRandom == 1) {
            // Load the question
        PickQuestion();
        UpdateText();
        //UpdateSlots();

        // Load the answer(s) in a random location, with the other options filled with random kanji.
        UpdateOptions();

        questionComplete = false;
        ClearUpperText();
        UpdateBottomButtons();
        }
    }

    private void ClearUpperText() {
        upperText.text = "";
        pronounciationText.text = "";
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
            Debug.Log("Ending...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("End");
        }
    }

    private void UpdateText() {
        var width = 100;
        questionText.text = "";
        foreach (string kanjiCharacter in currentQuestion.kanji) {
            questionText.text += kanjiCharacter;
            width += 200;
        }
        questionText.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 300);
    }

    private void UpdateOptions() {
        List<int> optionIndexes = new List<int>() {0,1,2,3};
        int random = Random.Range(0, optionIndexes.Count);

        Transform option = optionButtons.transform.GetChild(optionIndexes[random]);
        optionIndexes.RemoveAt(random);

        correctCard = option.GetComponent<TappableCardButton>();
        option.GetComponent<TappableCardButton>().SetValue(currentQuestion.image);

        usedImages = new List<Sprite>();

        for (int i = 0; i < optionIndexes.Count; i++) {
            option = optionButtons.transform.GetChild(optionIndexes[i]);

            Sprite randomImage = null;
            bool retry = true;
            while (retry) {
                retry = false;
                Sprite test = allPics[Random.Range(0, allPics.Length)];
                if (test == currentQuestion.image) {
                    retry = true;
                }

                foreach (Sprite sprite in usedImages) {
                    if (test == sprite) {
                        retry = true;
                    }
                }

                randomImage = test;
            }
            
            option.GetComponent<TappableCardButton>().SetValue(randomImage);
            usedImages.Add(randomImage);
        }
    }

    private void UpdateBottomButtons() {
        Debug.Log("Updating...");
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
        pronounciationText.text = currentQuestion.fullPronounciation;
    }

    public void CheckAnswer() {
        Slot slot = tapHandler.GetSlot();

        // Check Empty
        if (slot.GetCard() == null) {
            return;
        }

        bool answerCorrect = true;

        // Assign colors and disable

        TappableCardButton card = slot.GetCard();
        if (card.GetImageValue() == currentQuestion.image) {
            Debug.Log("correct~!");
            card.GetComponent<Image>().color = new Color32(77,173,76,255);
            card.SetTappable(false);
            // Clip 0 is correct
            audioSource.PlayOneShot(clips[0]);
        }
        else {
            Debug.Log("WRONG!"); 
            card.GetComponent<Image>().color = new Color32(212, 53, 53, 255);
            // Clip 2 is incorrect
            audioSource.PlayOneShot(clips[2]);

            if (currentQuestion.triesRemaining > 1) {
                upperText.text = "もう一度...";
                currentQuestion.level++;
                answerCorrect = false;
            }
            else {
                upperText.text = currentQuestion.meaning;

                pronounciationText.text += currentQuestion.fullPronounciation;
                // This only happens at full fail

                correctCard.GetComponent<Image>().color = new Color32(77,173,76,255);

                tapHandler.ClearSlot();
            
                answerCorrect = false;
                card.SetTappable(false);
                questionComplete = true;

                UpdateBottomButtons();
            }
        }
        
        if (answerCorrect) {
            questionComplete = true;
            UpdateBottomButtons();
            upperText.text = "よくできた！";
            AddFurigana();
        }
        else {
            // This happens both tries

            currentQuestion.triesRemaining--;
            questionComplete = false;
            
        }

        UpdateQuestionLevel(answerCorrect);
        
    }

    public void DeleteSlots() {
        Debug.Log("Don't Call this");
    }

    public void Next() {
        audioSource.PlayOneShot(clips[3]);

        int switchRandom = Random.Range(0,2);

        Debug.Log(switchRandom);

        if (switchRandom == 0) {
            Debug.Log("Switching to Pic to Kanji Question");
            UnityEngine.SceneManagement.SceneManager.LoadScene("PicToKanji");
        }

        else if (switchRandom == 1) {
            Debug.Log("Staying...");
            tapHandler.ClearSlot();
            LoadQuestion();
        }
    }
}
