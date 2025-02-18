using Unity.Multiplayer.Center.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class Question : ScriptableObject
{
    public Sprite image;

    public string[] kanji;

    public int level = 0;

    public int triesRemaining = 2;

    public string[] pronounciation;

    public string fullPronounciation;

    public string meaning;

}
