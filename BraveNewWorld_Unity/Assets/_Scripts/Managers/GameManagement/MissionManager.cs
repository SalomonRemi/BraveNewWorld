using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DigitalRuby.SoundManagerNamespace;

public class MissionManager : MonoBehaviour {

	public static MissionManager instance = null;


    public bool finishedLevel = false;
	public bool finishedStep01 = false;
    public Animator levier;
    public Animator commandPanel;

	public bool keyPadCorrect = false;
	public bool fileDelivered = false;
	public bool mission1indication = false;

	public float doorAmmount = 0;
	public TextMeshPro recapText;

    public flipSwitch flipper;

    public TextMeshPro digiTxt;

	[HideInInspector] public bool digiFinishPuzzle = false;
	[HideInInspector] public bool inLastPuzzle;
	[HideInInspector] public bool searchJack;

	[HideInInspector] public bool hideDigicode;

    [HideInInspector]public int numberOfGoodDoor = 0;

    int alerts = 0;
    int puzzleNum = 0;

    GameObject player;

	List<int> doorNums = new List<int>();


    void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {

			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}


	void Start ()
    {
        hideDigicode = false;

		player = GameObject.FindGameObjectWithTag ("Player");
        StartCoroutine(startGame());
        recapText.text = "Ne bougez plus, votre bonheur est en danger. Nous venons vous chercher.";
	}


    public IEnumerator startGame()
    {
		Dialogue dialogue = new Dialogue ();
		recapText.text = "";

        yield return new WaitForSeconds(0.2f);

        //AudioManager.instance.PlayMusic("startDialogue");

		dialogue.sentences.Add ("Bien, vous voilà installé, il est temps de rentrer dans le vif du sujet :");
		dialogue.sentences.Add ("Des deltas sont bloqués dans la salle de décantation et \n ils ont besoin de se rendre dans le dépôt des embryons. ");
		dialogue.sentences.Add ("Vous voyez ces boutons devant vous ? Ils vous permettent d’ouvrir les portes.");
		dialogue.sentences.Add ("N’oubliez pas de presser la touche “valider” une fois \n les bonnes portes sélectionnées.");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);
        
        yield return new WaitForSeconds(1f);
        StartCoroutine(mission1());
		yield return new WaitForSeconds(10f);
		recapText.text = "Ouvrez les portes de la salle de décantation et du dépôt des embryons.";
        yield return null;
    }


    public IEnumerator mission1()
    {
        puzzleNum = 1;
        doorNums.Add (3);
		doorNums.Add (6);
        numberOfGoodDoor = 2;
        doorAmmount = 7;

		//StartCoroutine(randomTalk(puzzleNum));

        while (!finishedLevel)
        {
			if (keyPadCorrect) {
				finishedLevel = true;
			}
            yield return null;
        }

        doorNums.Clear();

        yield return new WaitForSeconds (2);

		StartCoroutine (mission2 ());
        resestMission();

        //StopCoroutine("randomTalk");
        yield return null;
    }


	public IEnumerator mission2()
	{
		puzzleNum = 2;
		doorNums.Add (1);
		doorNums.Add (5);
		numberOfGoodDoor = 2;
		doorAmmount = 7;

		recapText.text = "";

		//StartCoroutine(randomTalk(puzzleNum));

		Dialogue dialogue = new Dialogue ();
		dialogue.sentences.Add ("Vous semblez apprendre vite, Wilson. J’ai déjà une autre tâche pour vous.");
		dialogue.sentences.Add ("Mr. Jay vient de se rendre compte que les livres éducatifs \n ont plus de 3 ans, vous rendez-vous compte ?");
		dialogue.sentences.Add ("Localisez ce matériel défectueux, puis ouvrez l’accès,\n j’enverrais des employés s’en débarrasser.");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

		yield return new WaitForSeconds (2);

		recapText.text = "Localisez les livres puis déverrouillez un accès pour se débarrasser de ce matériel défecteux";

		while (!finishedLevel)
		{
			if (keyPadCorrect)
			{
				finishedLevel = true;
			}
			yield return null;
		}

		//StopCoroutine("randomTalk");
		doorNums.Clear();
		StartCoroutine(mission3());
		resestMission();

		yield return null;
	}


	public IEnumerator mission3()
	{
        puzzleNum = 3;
        doorNums.Add(6);
        doorNums.Add(2);
        numberOfGoodDoor = 2;
        doorAmmount = 7;

		//StartCoroutine(randomTalk(puzzleNum));
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("2dialogue");

        Dialogue dialogue = new Dialogue ();
		dialogue.sentences.Add ("Très bien Wilson, je sens que vous avez compris ce que l’entreprise attend de vous.\n Vous n’êtes pas comme votre prédécesseur.");
		dialogue.sentences.Add ("Bref, on vient de me faire parvenir qu’une livraison à été facturée \n et stockée en salle de fécondation, il y a 5 jours.");
		dialogue.sentences.Add (" Avec tout ça, personne n’a eu le temps de s’en occuper. \n Trouvez sa destination et assurez son transfert.");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

		yield return new WaitForSeconds(5f);

		recapText.text = "Trouvez la facture arrivée il y a 5 jours. La livraison est arrivée en salle de fécondation, permettez lui l'accès jusqu'à la salle concernée";

		while (!finishedLevel)
		{
            if (keyPadCorrect)
            {
				finishedLevel = true;
            }
            yield return null;
		}

        //StopCoroutine("randomTalk");
        doorNums.Clear();
        StartCoroutine(mission4());
        resestMission();

        yield return null;
    }


	public IEnumerator mission4()
    {
		hideDigicode = true;
		searchJack = true;

		puzzleNum = 4;

        GameManager.instance.flashKeypad = false;

		//StartCoroutine(randomTalk(puzzleNum));
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("3dialogue");

        Dialogue dialogue = new Dialogue();
		dialogue.sentences.Add("Bien joué Wilson, je ne vais pas vous mentir, votre journée commence fort. Tenez, j’ai une tâche plus légère à vous confier.");
		dialogue.sentences.Add("Un de nos ouvriers, Jack, fête ses 30 ans au sein du centre, cela se fête n’est ce pas ?");
		dialogue.sentences.Add("Ses collègues, avec notre accord, lui ont réservé une surprise dans le self.");
		dialogue.sentences.Add("Je vous donne accès au digicode, identifiez le puis ouvrez lui les portes. Je ne sais plus où il se trouve, référez vous à son emploi du temps.");
        
		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        yield return new WaitForSeconds(5f);

        commandPanel.SetBool("isDigicodeAvailable", true);

        recapText.text = "Trouvez le numéro de matricule de Jack, puis donnez lui accès au self. Son emploi du temps se trouve dans le manuel.";
        
		while (!finishedStep01)
        {
			if (digiFinishPuzzle)
            {
				finishedStep01 = true;
            }
            yield return null;
        }

		doorNums.Add(2);
		doorNums.Add(4);
		doorNums.Add(7);
		numberOfGoodDoor = 3;
		doorAmmount = 7;

		Dialogue dialogue2 = new Dialogue();
		dialogue2.sentences.Add("Bien vous l'avez localisé, ouvrez lui les portes vers le self");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);

		while (!finishedLevel)
		{
			if (keyPadCorrect)
			{
				finishedLevel = true;
			}
			yield return null;
		}

		//StopCoroutine("randomTalk");
        doorNums.Clear();
        StartCoroutine(mission5());
        resestMission();

        yield return null;
    }


    public IEnumerator mission5()
    {
        puzzleNum = 5;
        inLastPuzzle = true;

        GameManager.instance.flashKeypad = true;

        //StartCoroutine(randomTalk(puzzleNum));
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("4dialogue");

        Dialogue dialogue = new Dialogue();
        dialogue.sentences.Add("Vous savez Wilson, c’est en travaillant comme vous le faites \n que les employés se voient attribués une promotion et…");
        dialogue.sentences.Add("Attendez, la salle de tri est ouverte, c’est étrange… \n Personne n’est sensé y avoir accès à cette heure-ci.");
        dialogue.sentences.Add("Je vais essayer de me renseigner si aucune fraude ou détérioration n’a eu lieu…");
        dialogue.sentences.Add("Essayez de trouver qui s’y trouve, vous devriez commencer \n par consulter la fiche des incidents.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

		yield return new WaitForSeconds(5f);
        
		recapText.text = "Trouvez qui se trouve dans la salle tri et régler le problème";

        while (!finishedStep01)
        {
            if (digiFinishPuzzle)
            {
                finishedStep01 = true;
            }
            yield return null;
        }

        Dialogue dialogue2 = new Dialogue();
        dialogue2.sentences.Add("Mhhh je vois. Mademoiselle Fanny Crown donc.\nElle a posé beaucoup de problème à l’entreprise auparavant.");
        dialogue2.sentences.Add("Il est temps de régler définitivement le problème.\n Je vous donne accès au levier pour activer la salle de tri.");
        dialogue2.sentences.Add("Vous savez ce qu’il vous reste à faire.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);

        yield return new WaitForSeconds(5f);

        levier.SetBool("isOpening", true);

        recapText.text = "Baissez le levier";

        while (!finishedLevel)
        {
            if (flipper.switchOn)
            {
                finishedLevel = true;
            }
            yield return null;
        }

        //StopCoroutine("randomTalk");
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("5dialogue");

        Dialogue dialogue3 = new Dialogue();
        dialogue3.sentences.Add("Parfait Wilson! Le conditionnement a parfaitement été réalisé.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

		AudioManager.instance.PlaySound("weirdSoundsTri");
        doorNums.Clear();
        resestMission();
    }


    public void resestMission()
    {
        keyPadCorrect = false;
        finishedStep01 = false;
        finishedLevel = false;
		fileDelivered = false;
		digiFinishPuzzle = false;
	}


	public bool ValidateKeypadCode(List<int> keycode)
	{
		int validateNumber = 0;

		if (keycode.Count == numberOfGoodDoor)
		{
			for(int i = 0; i < doorNums.Count; i++)
			{ 
				for (int x = 0; x < keycode.Count; x++)
				{
					if (doorNums [i] == keycode [x]) 
					{
						validateNumber++;
					}
				}
			}
			if (validateNumber == numberOfGoodDoor)
			{
				return true;
			} 
			else return false;
		} 
		else return false;
	}

    public IEnumerator randomTalk(int missionLevel)
    {
        int i = Random.Range(1, 6);
        yield return new WaitForSeconds(180);
        if (missionLevel == puzzleNum)
        {
            switch (i)
            {
                case 1:
                    AudioManager.instance.PlayMusic("rand1");
                    break;
                case 2:
                    AudioManager.instance.PlayMusic("rand2");
                    break;
                case 3:
                    AudioManager.instance.PlayMusic("rand3");
                    break;
                case 4:
                    AudioManager.instance.PlayMusic("rand2");
                    break;
                case 5:
                    AudioManager.instance.PlayMusic("rand5");
                    break;
            }
        }
        yield return null;
        StartCoroutine(randomTalk(missionLevel));
    }
}