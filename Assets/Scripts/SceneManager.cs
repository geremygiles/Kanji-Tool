using UnityEngine;

public class SceneManager : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("");
    }

    #endregion
}
