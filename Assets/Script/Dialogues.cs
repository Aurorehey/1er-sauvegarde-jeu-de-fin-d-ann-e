using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string name;

    [TextArea(3,10)]//le nombre de blocs de dialogues possible entre 3 et 10.
    public string[] sentences;

}
