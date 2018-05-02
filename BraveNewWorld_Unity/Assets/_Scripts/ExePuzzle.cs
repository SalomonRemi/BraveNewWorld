using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExePuzzle : MonoBehaviour {

    public digiCode digicode;
    public Keypad keypad;

    [HideInInspector] public bool puzzleDone;
    [HideInInspector] public bool puzzleOk;
    [HideInInspector] public bool nextStep;

    private int actualPuzzle;
    private bool inSearch;

    private float searchCount;


    private void Update()
    {
        if (inSearch)
        {
            searchCount += Time.unscaledDeltaTime;

            if (searchCount > 10)
            {
                OutOfTimeFeedback();
            }
        }
        else searchCount = 0;
    }


    public void StartPuzzle(int puzzleID)
    {
        actualPuzzle = puzzleID;

        if (puzzleID == 3) StartCoroutine(Puzzle3(5f));
        else if (puzzleID == 6) StartCoroutine(Puzzle6(5f, 10f));
        else if (puzzleID == 8) StartCoroutine(Puzzle8(5f, 10f));
    }

    public void StopPuzzle()
    {
        inSearch = false;

        StopAllCoroutines();
        StartCoroutine(RestartPuzzle());
    }

    public void ResetPuzzle()
    {
        puzzleOk = false;
        nextStep = false;
        inSearch = false;
        searchCount = 0;

        keypad.resetKeypad();
    }

    public void OutOfTimeFeedback()
    {
        ResetPuzzle();
        keypad.ComfirmInput();
    }


    public IEnumerator Puzzle3(float waitTime)
    {
        Dialogue dialogue1 = new Dialogue();
        dialogue1.sentences.Add("Ouvrez le dépôt des embryons.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);
        StartCoroutine(MissionManager.instance.DisplayOrder(.2f));
        MissionManager.instance.orderText = "Ouvrez le dépôt des embryons.";

        inSearch = true;
        while (!nextStep)
        {
            if(keypad.keyPressed.Count > 0)
            {
                if (keypad.keyPressed[0] == 3)
                {
                    puzzleOk = true;
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue2 = new Dialogue();
        dialogue2.sentences.Add("Attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);
        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(.2f);

        Dialogue dialogue3 = new Dialogue();
        dialogue3.sentences.Add("Ouvrez la salle de fécondation ET le self.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

        inSearch = true;
        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if ((keypad.keyPressed[0] == 2 || keypad.keyPressed[0] == 7) && keypad.keyPressed.Count > 1)
                {
                    if(keypad.keyPressed[1] == 2 || keypad.keyPressed[1] == 7)
                    {
                        puzzleOk = true;
                    }
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue4 = new Dialogue();
        dialogue4.sentences.Add("Ouvrez la salle de tri.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue4);

        inSearch = true;
        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if (keypad.keyPressed[0] == 5)
                {
                    puzzleOk = true;
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue5 = new Dialogue();
        dialogue5.sentences.Add("Attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue5);
        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(.2f);

        Dialogue dialogue6 = new Dialogue();
        dialogue6.sentences.Add("Ouvrez la salle de décantation.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue6);

        inSearch = true;
        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if (keypad.keyPressed[0] == 6)
                {
                    puzzleOk = true;
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue7 = new Dialogue();
        dialogue7.sentences.Add("Attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue7);
        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(.2f);

        puzzleDone = true;
    }


    IEnumerator Puzzle6(float waitTime, float timeMax)
    {
        yield return null;
    }


    IEnumerator Puzzle8(float waitTime, float timeMax)
    {
        yield return null;
    }


    public IEnumerator RestartPuzzle()
    {
        if (actualPuzzle == 3)
        {
            Dialogue dialogue = new Dialogue();
            dialogue.sentences.Add("Vous n’êtes pas assez réactif Wilson, soyez un peu plus attentif. C’est votre premier jour, ça ira pour cette fois. Recommençons depuis le début.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue);
        }
        else if (actualPuzzle == 6)
        {
            Dialogue dialogue1 = new Dialogue();
            dialogue1.sentences.Add("*soupirs*, Vous n’y êtes pas Wilson, un peu de vivacité bon sang, les enfants attendent ! J'espère ne pas vous avoir surestimé après tout. Bref, recommençons calmement.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);
        }
        else if (actualPuzzle == 8)
        {

        }

        yield return new WaitForSeconds(11f);

        StartPuzzle(actualPuzzle);
    }
}