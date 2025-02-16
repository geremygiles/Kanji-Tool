using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{

    SceneManager sceneManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManager = FindFirstObjectByType<SceneManager>();
        GetComponent<Button>().onClick.AddListener(Quit);
    }

    private void Quit() {
        sceneManager.Quit();
    }

}
