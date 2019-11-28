using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]

    public string[] sentences;

    public struct Character
    {
        public string name;
        public string text;
        public Sprite image;
        public Character(string name, string text, Sprite image)
        {
            this.name = name;
            this.text = text;
            this.image = image;
        }
    }
}
