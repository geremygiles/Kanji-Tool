using UnityEngine;
using UnityEngine.UI;

public class StatsButton : MonoBehaviour
{
    SceneManager sceneManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManager = FindFirstObjectByType<SceneManager>();
        GetComponent<Button>().onClick.AddListener(Stats);
    }

    private void Stats() {
        sceneManager.Stats();
    }
}
