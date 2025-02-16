using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    SceneManager sceneManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManager = FindFirstObjectByType<SceneManager>();
        GetComponent<Button>().onClick.AddListener(Play);
    }

    private void Play() {
        sceneManager.StartGame();
    }
}
