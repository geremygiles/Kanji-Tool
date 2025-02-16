using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject
{
    public Sprite image;

    public string[] kanji;

    public int level = 0;

}
