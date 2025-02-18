using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
public static SceneManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
        }
        else {
            Destroy(gameObject);
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Public Methods

    public void Quit() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void End() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("End");
    }

    public void StartGame() {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("PicToKanji");
        UnityEngine.SceneManagement.SceneManager.LoadScene("KanjitoPic");
    }

    public void Stats() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stats");
    }

    #endregion
}
