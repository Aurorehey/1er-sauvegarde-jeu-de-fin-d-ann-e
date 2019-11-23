using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialoguesText;

    public Animator animator;

    private Queue<string> sentences;
    
    

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }
    public void StartDialogues (Dialogues dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogues();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialoguesText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialoguesText.text += letter;
            yield return null;
        }
    }
    void EndDialogues()
    {
        animator.SetBool("IsOpen", false);
    }
}
