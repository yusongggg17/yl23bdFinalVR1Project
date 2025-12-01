using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogues")]
    [TextArea(2, 5)]
    public string[] dialogues;
    public GameObject currentAnimal;
    public int currentDialogue;
    private bool currentComplete = true;

    [Header("XR Canvas & Text")]
    public Canvas dialogueCanvas;        // XR Canvas
    public TMP_Text dialogueText;        // TMP text inside the canvas
    private XRBaseInteractable interactable;
    int countDiaglogue = 0;
    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        interactable.selectEntered.AddListener(OnInteract);

        if (dialogueCanvas != null)
            dialogueCanvas.enabled = false;  // start hidden
    }

    private void Start()
    {

    }

    private void OnInteract(SelectEnterEventArgs args)
    {
        if (countDiaglogue >= dialogues.Length)
        {
            dialogueText.text = "Thank you! I am full!";
        }
        else if (dialogues.Length == 0)
        {
            dialogueText.text = "No dialogue found.";
        }
        else
        {
            if (currentComplete)
            {
                ShowRandomDialogue();
            }
        }
    }

    private void ShowRandomDialogue()
    {

        dialogueText.text = dialogues[countDiaglogue];
        currentDialogue = countDiaglogue;
        countDiaglogue++;

        dialogueCanvas.enabled = true;
        currentComplete = false;
    }

    public void CorrectDialogue()
    {
        dialogueText.text = "Thank you! This is great!";
        currentComplete = true;
    }
    public void WrongDialogue()
    {
        dialogueText.text = "Sorry, I don't want this...";
        currentComplete = false;
    }
    public void RequireAdditionalDialogue()
    {
        dialogueText.text = "Thank you! I'm still waiting on another thing.";
        currentComplete = false;
    }
}