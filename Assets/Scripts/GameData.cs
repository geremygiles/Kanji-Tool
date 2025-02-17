using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    [SerializeField] public List<Question> questions;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        
    }

    public void ResetQuestion(int index) {
        questions[index].level = 0;
        questions[index].triesRemaining = 2;
    }
}
