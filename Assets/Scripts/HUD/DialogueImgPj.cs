using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Character
{
    public string NAME;
    public List<Emotion> EMOTIONS = new List<Emotion>();
}

[System.Serializable]
public class Emotion
{
    public string EMOTION;
    public Texture ICON;
}

public class DialogueImgPj : MonoBehaviour
{
    public List<Character> CHARACTERS = new List<Character>();
    public Dictionary<string, List<Emotion>> CHARACTERS_TRUE;

    private void Awake()
    {
        CHARACTERS_TRUE = CHARACTERS.ToDictionary(character => character.NAME, character => character.EMOTIONS);
    }
}
