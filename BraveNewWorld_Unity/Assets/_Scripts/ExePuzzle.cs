using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class ExePuzzle : MonoBehaviour {

    public float waitTime;
    public float maxTime;

    public digiCode digicode;
    public Keypad keypad;

    [HideInInspector] public bool puzzleDone;
    [HideInInspector] public bool puzzleOk;
    [HideInInspector] public bool nextStep;

    [HideInInspector] public int stepID;

    private int actualPuzzle;
    private bool inSearch;
    private float searchCount;


    private void Update()
    {
        if (inSearch)
        {
            searchCount += Time.unscaledDeltaTime;

            if (searchCount > maxTime)
            {
                OutOfTimeFeedback();
            }
        }
        else searchCount = 0;
    }


    public void StartPuzzle(int puzzleID)
    {
        actualPuzzle = puzzleID;

        if (puzzleID == 3) StartCoroutine(Puzzle3());
        else if (puzzleID == 6) StartCoroutine(Puzzle6());
        else if (puzzleID == 8) StartCoroutine(Puzzle8());
    }

    public void StopPuzzle()
    {
        inSearch = false;

        StopAllCoroutines();

        StartCoroutine(keypad.flashKeys(Color.red, true));
        StartCoroutine(digicode.flashKeys(Color.red, true));
        AudioManager.instance.PlaySound("digiError");

        StartCoroutine(RestartPuzzle());
    }

    public void ResetPuzzle()
    {
        puzzleOk = false;
        nextStep = false;
        inSearch = false;
        searchCount = 0;

        keypad.resetKeypad();
        digicode.resetKeypad();
    }

    public void OutOfTimeFeedback()
    {
        ResetPuzzle();
        keypad.ComfirmInput();
    }


    IEnumerator Puzzle3()
    {
        Dialogue dialogue1 = new Dialogue();
        dialogue1.sentences.Add("Ouvrez le dépôt des embryons.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle3_1");

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

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderWait1");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        Dialogue dialogue3 = new Dialogue();
        dialogue3.sentences.Add("Ouvrez la salle de fécondation ET le self.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle3_2");

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

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle3_3");

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

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderWait2");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        Dialogue dialogue6 = new Dialogue();
        dialogue6.sentences.Add("Ouvrez la salle de décantation.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue6);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle3_4");

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

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderWait1");

        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        puzzleDone = true;
    }


    IEnumerator Puzzle6()
    {
        inSearch = true;
        stepID = 1;

        Dialogue dialogue1 = new Dialogue();
        dialogue1.sentences.Add("Tapez 95 sur le Digicode.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_1");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);


        inSearch = true;

        Dialogue dialogue2 = new Dialogue();
        dialogue2.sentences.Add("Ouvrez la salle de fécondation.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_2");

        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if (keypad.keyPressed[0] == 2)
                {
                    puzzleOk = true;
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue3 = new Dialogue();
        dialogue3.sentences.Add("Attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_3");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        inSearch = true;

        Dialogue dialogue4 = new Dialogue();
        dialogue4.sentences.Add("Ouvrez la salle de mise en flacon et de décantation.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue4);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_4");

        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if ((keypad.keyPressed[0] == 4 || keypad.keyPressed[0] == 6) && keypad.keyPressed.Count > 1)
                {
                    if (keypad.keyPressed[1] == 4 || keypad.keyPressed[1] == 6)
                    {
                        puzzleOk = true;
                    }
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 2;

        Dialogue dialogue5 = new Dialogue();
        dialogue5.sentences.Add("Tapez 18 sur le Digicode.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue5);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_5");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);


        inSearch = true;
        stepID = 3;

        Dialogue dialogue6 = new Dialogue();
        dialogue6.sentences.Add("Tapez 19.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue6);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_6");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue7 = new Dialogue();
        dialogue7.sentences.Add("Attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue7);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_7");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);


        inSearch = true;
        stepID = 1;

        Dialogue dialogue8 = new Dialogue();
        dialogue8.sentences.Add("Tapez 95 à nouveau.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue8);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_8");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);


        inSearch = true;

        Dialogue dialogue9 = new Dialogue();
        dialogue9.sentences.Add("Ouvrez la salle de conditionnement.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue9);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_9");

        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if (keypad.keyPressed[0] == 1)
                {
                    puzzleOk = true;
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue10 = new Dialogue();
        dialogue10.sentences.Add("Attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue10);

        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle6_10");

        yield return new WaitForSeconds(waitTime);

        puzzleDone = true;

        yield return null;
    }


    IEnumerator Puzzle8()
    {
        Dialogue dialogue1 = new Dialogue();
        dialogue1.sentences.Add("Attendez un petit peu.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_1");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        inSearch = true;

        Dialogue dialogue2 = new Dialogue();
        dialogue2.sentences.Add("Ouvrez la salle de conditionnement et de décantation.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);
        AudioManager.instance.StopMusic();

        AudioManager.instance.PlayMusic("orderPuzzle8_2");

        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if ((keypad.keyPressed[0] == 1 || keypad.keyPressed[0] == 6) && keypad.keyPressed.Count > 1)
                {
                    if (keypad.keyPressed[1] == 1 || keypad.keyPressed[1] == 6)
                    {
                        puzzleOk = true;
                    }
                }
            }
            yield return null;
        }

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 4;

        Dialogue dialogue3 = new Dialogue();
        dialogue3.sentences.Add("Tapez 260 sur le Digicode.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);
        AudioManager.instance.StopMusic();

        AudioManager.instance.PlayMusic("orderPuzzle8_3");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 5;

        Dialogue dialogue4 = new Dialogue();
        dialogue4.sentences.Add("Non, tapez 270 sur le digicode.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue4);
        AudioManager.instance.StopMusic();

        AudioManager.instance.PlayMusic("orderPuzzle8_4");


        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 6;

        Dialogue dialogue5 = new Dialogue();
        dialogue5.sentences.Add("Toujours pas, essayez 280.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue5);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_5");


        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue6 = new Dialogue();
        dialogue6.sentences.Add("Ah, attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue6);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_6");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        inSearch = true;

        Dialogue dialogue7 = new Dialogue();
        dialogue7.sentences.Add("Ouvrez la salle de fécondation et de mise en flacon.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue7);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_7");

        while (!nextStep)
        {
            if (keypad.keyPressed.Count > 0)
            {
                if ((keypad.keyPressed[0] == 4 || keypad.keyPressed[0] == 2) && keypad.keyPressed.Count > 1)
                {
                    if (keypad.keyPressed[1] == 4 || keypad.keyPressed[1] == 2)
                    {
                        puzzleOk = true;
                    }
                }
            }
            yield return null;
        }

        ResetPuzzle();

        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 7;

        Dialogue dialogue8 = new Dialogue();
        dialogue8.sentences.Add("Tapez 450 sur le digicode.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue8);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_8");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;

        Dialogue dialogue9 = new Dialogue();
        dialogue9.sentences.Add("D'accord, ouvrez la salle de tri.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue9);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_9");

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

        inSearch = true;
        stepID = 8;

        Dialogue dialogue10 = new Dialogue();
        dialogue10.sentences.Add("Wilson, tapez 860 sur le digicode.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue10);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_10");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 9;

        Dialogue dialogue11 = new Dialogue();
        dialogue11.sentences.Add("Toujours pas, tapez 870.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue11);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_11");


        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 10;

        Dialogue dialogue12 = new Dialogue();
        dialogue12.sentences.Add("Non... essayez 850.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue12);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_12");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        Dialogue dialogue13 = new Dialogue();
        dialogue13.sentences.Add("Ah, attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue13);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_13");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        inSearch = true;

        Dialogue dialogue14 = new Dialogue();
        dialogue14.sentences.Add("Très bien ouvrez encore la salle de tri.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue14);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_14");

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

        Dialogue dialogue15 = new Dialogue();
        dialogue15.sentences.Add("Attendez, attendez.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue15);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_15");
        yield return new WaitForSeconds(waitTime);

        yield return new WaitForSeconds(.2f);

        inSearch = true;
        stepID = 11;

        Dialogue dialogue16 = new Dialogue();
        dialogue16.sentences.Add("Wilson, tapez 461.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue16);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayMusic("orderPuzzle8_16");

        while (!nextStep)
        {
            yield return null;
        }
        puzzleOk = true;

        ResetPuzzle();
        yield return new WaitForSeconds(.2f);

        puzzleDone = true;
    }


    public IEnumerator RestartPuzzle()
    {
        if (actualPuzzle == 3)
        {
            Dialogue dialogue = new Dialogue();
            dialogue.sentences.Add("Vous n’êtes pas assez réactif Wilson, soyez un peu plus attentif. C’est votre premier jour, ça ira pour cette fois. Recommençons depuis le début.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayMusic("hint3");
        }
        else if (actualPuzzle == 6)
        {
            Dialogue dialogue1 = new Dialogue();
            dialogue1.sentences.Add("*longs soupirs*. Vous n’y êtes pas Wilson, un peu de vivacité bon sang, les enfants attendent ! J'espère ne pas vous avoir surestimé après tout. Bref, recommençons calmement.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);

            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayMusic("hint6");
        }
        else if (actualPuzzle == 8)
        {
            Dialogue dialogue2 = new Dialogue();
            dialogue2.sentences.Add("Vous devriez commencer à savoir faire Wilson, nous ne serons pas toujours aussi tolérants croyez-le. Soyez plus minutieux !");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayMusic("hint8");
        }

        yield return new WaitForSeconds(10f);

        StartPuzzle(actualPuzzle);
    }
}