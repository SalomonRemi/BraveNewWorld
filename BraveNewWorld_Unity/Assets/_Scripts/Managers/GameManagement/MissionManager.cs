using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using DigitalRuby.SoundManagerNamespace;

public class MissionManager : MonoBehaviour {

	public static MissionManager instance = null;

    [Header("Introduction Settings")]

    public float timeInElevator;
	public float fadeTime;

    public Animator elevatorDoorAnim;
    public Animator hallDoorAnim;
    public Animator doorRoomAnim;
	public Animator blockView;
    public GameObject tutoMoveObject;
	public GameObject tutoRotateObject;
    public GameObject babiesPrefab;
	public GameObject elevator;
    public GameObject liftLoopObj;
    public Transform instantiateTransform1;
    public Transform instantiateTransform2;

    [HideInInspector] public bool goToNextStep;
    [HideInInspector] public bool isInElevator;
    [HideInInspector] public bool isInHall;
    [HideInInspector] public bool enterInRoom;
    [HideInInspector] public bool closeDoor;
	[HideInInspector] public bool isAtDesk;

    private bool doScrolling;
    private float counter;

    [Header("Missions Settings")]

    public Animator levier;
    public Animator commandPanel;

	public digiCode digicode;
	public Keypad keypad;

    [HideInInspector] public bool finishedLevel = false;
    [HideInInspector] public bool finishedStep01 = false;
    [HideInInspector] public bool keyPadCorrect = false;
    [HideInInspector] public bool mission1indication = false;
    [HideInInspector] public float doorAmmount = 0;

    [HideInInspector] public bool inExePuzzle;
    [HideInInspector] public bool canStartExePuzzle;

    public TextMeshPro recapText;
	public TextMeshPro oscarOrderText;
    public flipSwitch flipper;
    public TextMeshPro digiTxt;

	[Header("Other Settings")]

	public Transform debugTransform;
	public GameObject agent;

	[HideInInspector] public bool digiFinishPuzzle = false;
	[HideInInspector] public bool inLastPuzzle;
	[HideInInspector] public bool searchJack;

	[HideInInspector] public bool hideDigicode;

    [HideInInspector] public int numberOfGoodDoor = 0;
    [HideInInspector] public string orderText;

    private int alerts = 0;
    private int puzzleNum = 0;
	private bool doorFeedbackState;

	private MenuManager mm;
    private ExePuzzle ep;

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
        isInElevator = true;
        isInHall = true;
        enterInRoom = false;
        closeDoor = false;

		mm = FindObjectOfType<MenuManager>();
		player = GameObject.FindGameObjectWithTag ("Player");
        ep = GetComponent<ExePuzzle>();

        StartCoroutine(startIntroduction());
        StartCoroutine(scrollingBabies());

		agent.SetActive (false);

        recapText.text = "Ne bougez plus, votre bonheur est en danger. Nous venons vous chercher.";
	}


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            StopAllCoroutines();

            AudioManager.instance.StopMusic();

            StartCoroutine(startMission());

			player.transform.position = debugTransform.position;
        }

        if(doScrolling)
        {
            counter += Time.deltaTime;
            if(counter >= 1.5f)
            {
				Instantiate(babiesPrefab, instantiateTransform1.position, Quaternion.Euler(new Vector3(0,-90,0)));
				Instantiate(babiesPrefab, instantiateTransform2.position, Quaternion.Euler(new Vector3(0,90,0)));

                counter = 0;
            }
        }
    }



#region Introduction
    public IEnumerator startIntroduction()
    {
        yield return new WaitForSeconds(fadeTime);

        Dialogue dialogue1 = new Dialogue();
		dialogue1.sentences.Add("Bonjour Wilson, je suis désolé de cette mutation imprévue. Nous avions besoin d'un remplaçant capable et disponible, ce que vous êtes je n'en doute pas.");
		dialogue1.sentences.Add("Oscar Sostiene, votre prédécesseur, a disparu ce matin et nous sommes actuellement à sa recherche. Nous craignons que certaines personnes en aient voulu à son bonheur.");
		dialogue1.sentences.Add("La plupart de ses affaires sont toujours dans le bureau, n'en soyez pas dérangé une équipe passera d'ici peu.");
		dialogue1.sentences.Add("Enfin, ce n'est pas votre problème, vous êtes gradé désormais, chose assez rare pour quelqu'un de votre classe, soyez-en fier !");
		FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);

        //AudioManager.instance.PlayMusic("introDialog01");

        yield return new WaitForSeconds(timeInElevator - 3f);


        AudioManager.instance.PlaySound("liftEnd");

        AudioSource sound = liftLoopObj.GetComponent<AudioSource>();
        sound.volume = 0;

		Destroy(tutoRotateObject);

        yield return new WaitForSeconds(3f);


        elevatorDoorAnim.SetBool("Open", true);
        AudioManager.instance.PlaySound("elevatorDoor");

        yield return new WaitForSeconds(.5f);

        tutoMoveObject.SetActive(true);

        while (!goToNextStep)
        {
            if(!isInElevator)
            {
                goToNextStep = true;
            }
            yield return null;
        }

        Destroy(tutoMoveObject);

        yield return new WaitForSeconds(3f);

		Dialogue dialogue2 = new Dialogue();
		dialogue2.sentences.Add("Ne vous laissez pas impressionner par cette théâtralité, vous vous y habituerez vite.");
		dialogue2.sentences.Add("Soyez le bienvenue Wilson, vous êtes ici chez vous.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);

        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("introDialog02");


        yield return new WaitForSeconds(10f);

		Dialogue dialogueOrdre = new Dialogue();
		dialogueOrdre.sentences.Add("Allez y je vous prie.");

        hallDoorAnim.SetBool("Open", true);

        yield return new WaitForSeconds(1f);


        while (goToNextStep)
        {
            if (!isInHall)
            {
                goToNextStep = false;
            }
            yield return null;
        }

		//intro3
		Dialogue dialogue3 = new Dialogue();
		dialogue3.sentences.Add("Vous êtes des nôtres désormais Wilson, dans une relation de confiance. Si jamais vous trouvez des informations sur Oscar, faites m'en part,");
		dialogue3.sentences.Add("il est sûrement en danger. Écoutez moi et tout ira pour le mieux !");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("introDialog03");

        yield return new WaitForSeconds(5f);

        while (!goToNextStep)
        {
            if (enterInRoom)
            {
                goToNextStep = true;
            }
            yield return null;
        }

        while (goToNextStep)
        {
            if (closeDoor)
            {
                goToNextStep = false;
            }
            yield return null;
        }

        doorRoomAnim.SetBool("Open", false);

        yield return new WaitForSeconds(.5f);

		//intro4
		Dialogue dialogue4 = new Dialogue();
		dialogue4.sentences.Add("Suite à l’incident de ce matin, les portes restent fermées durant les heures de travail.");
		dialogue4.sentences.Add("Ne vous en faites pas je vous accompagnerai le temps de vous apprendre le métier.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue4);

        yield return new WaitForSeconds(7f);

		while (!goToNextStep)
		{
			if (isAtDesk)
			{
				goToNextStep = true;
			}
			yield return null;
		}

        StartCoroutine(startMission());
    }
#endregion 


    public IEnumerator startMission()
    {
        Dialogue dialogue = new Dialogue ();

        yield return new WaitForSeconds(0.2f);

        //AudioManager.instance.PlayMusic("startDialogue");

		dialogue.sentences.Add ("Bien, vous voilà installé. Devant vous se trouvent le manuel d’utilisation et le tableau de bord, ils seront vos meilleurs amis aujourd’hui.");
		dialogue.sentences.Add ("Il est temps de rentrer dans le vif du sujet :");
		dialogue.sentences.Add ("Des deltas sont bloqués dans la salle de décantation et ils ont besoin de se rendre dans le dépôt des embryons. Ouvrez leur les portes !");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(10f));
        orderText = "Ouvrez les portes de la salle de décantation et du dépôt des embryons.";

        yield return new WaitForSeconds(1f);

        StartCoroutine(mission1());

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

		StartCoroutine (MapFeedback (3,6));

		while (!doorFeedbackState)
		{
			yield return null;
		}

        doorNums.Clear();

        yield return new WaitForSeconds(2f);

		StartCoroutine (mission2 ());
        resestMission();

        //StopCoroutine("randomTalk");
        yield return null;
    }


	public IEnumerator mission2()
	{
        puzzleNum = 2;
		doorNums.Add (1);
		doorNums.Add (3);
		doorNums.Add (5);
		numberOfGoodDoor = 3;
		doorAmmount = 7;

		//StartCoroutine(randomTalk(puzzleNum));

		Dialogue dialogue = new Dialogue ();
		dialogue.sentences.Add ("Hum, Oscar reste introuvable, nos recherches nous ont permis de conclure qu'il était en liaison avec un individu particulier. Restez sur vos gardes.");
		dialogue.sentences.Add ("Bien ! Vous semblez apprendre vite, Wilson, continuons sur notre lancée.");
		dialogue.sentences.Add ("Mr. Jay vient de se rendre compte que les batteries sont usées, vous rendez-vous compte ?");
		dialogue.sentences.Add ("Localisez ce matériel défectueux, puis ouvrez l’accès, j’enverrais des employés s’en débarrasser.");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(11f));
        orderText = "Localisez les batteries dans le manuel puis déverrouillez un accès pour se débarrasser de ce matériel défecteux.";

        yield return new WaitForSeconds(2f);

        while (!finishedLevel)
		{
			if (keyPadCorrect)
			{
				finishedLevel = true;
			}
			yield return null;
		}

		StartCoroutine (MapFeedback (1,5));

		while (!doorFeedbackState)
		{
			yield return null;
		}

		//StopCoroutine("randomTalk");
		doorNums.Clear();
		resestMission();
        StartCoroutine(mission3());

        yield return null;
	}


    public IEnumerator mission3()
    {
        puzzleNum = 3;
        inExePuzzle = true;

        Dialogue dialogue = new Dialogue();
        dialogue.sentences.Add("Bien Wilson, les batteries seront placées dans le broyeur sous peu. J’ai une tâche plus urgente à vous confier.");
        dialogue.sentences.Add("Une équipe d’investigation est entrée dans le centre à la recherche d’Oscar et vous allez devoir leur permettre l'accès aux différentes salles au fur et à mesure de l’opération.");
        dialogue.sentences.Add("Les enquêteurs ne font pas partie des employés de l’usine, vous ne pourrez pas les localiser.");
        dialogue.sentences.Add("Je vais donc vous épauler durant le processus, écoutez moi attentivement.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        while (!canStartExePuzzle)
        {
            yield return null;
        }

        ep.StartPuzzle(puzzleNum);

        while (!ep.puzzleDone)
        {
            yield return null;
        }

        keypad.ComfirmInput(); // APPELLE COMFIRMINPUT POUR FEEDBAKC FLASH ET SON

        yield return new WaitForSeconds(3f);

        doorNums.Clear();
        resestMission();
        StartCoroutine(mission4());

        yield return null;
    }


    public IEnumerator mission4()
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
        dialogue.sentences.Add ("Merci Wilson. Mh, toujours aucune nouvelle d’Oscar, je commence à croire qu’il s’est tout simplement volatilisé. Nous reviendrons à ce cas plus tard.");
        dialogue.sentences.Add ("Très bien, je sens que vous avez compris ce que l’entreprise attend de vous.\n Vous n’êtes pas comme votre prédécesseur.");
		dialogue.sentences.Add ("Bref, on vient de me faire parvenir qu’une livraison à été facturée \n et stockée en salle de fécondation, il y a 5 jours.");
		dialogue.sentences.Add ("Avec tout ça, personne n’a eu le temps de s’en occuper. \n Trouvez sa destination et assurez son transfert.");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(11f));
        orderText = "Trouvez la facture arrivée il y a 5 jours. La livraison est arrivée en salle de fécondation, permettez lui l'accès jusqu'à la salle concernée.";

        yield return new WaitForSeconds(2f);

		while (!finishedLevel)
		{
            if (keyPadCorrect)
            {
				finishedLevel = true;
            }
            yield return null;
		}

		StartCoroutine (MapFeedback (2,6));

		while (!doorFeedbackState)
		{
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
		hideDigicode = true;
		searchJack = true;

		puzzleNum = 4;

        GameManager.instance.flashKeypad = false;

		//StartCoroutine(randomTalk(puzzleNum));
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("3dialogue");

        Dialogue dialogue = new Dialogue();
		dialogue.sentences.Add("Oscar n'a jamais quitté l'usine, les caméras ne mentent pas. Ses comportements douteux laissent à penser qu'il prépare quelque chose.");
		dialogue.sentences.Add("Je doute qu'une information pareille soit dissimulée ici mais si jamais vous trouvez dans vos documents l'identifiant");
		dialogue.sentences.Add("de la personne qui tire les ficelles de cette affaire, transmettez les moi.");
		dialogue.sentences.Add("Revenons à un réel un peu plus radieux si vous le voulez bien !");
		dialogue.sentences.Add("Un de nos ouvriers, Jack, fête ses 30 ans au sein du centre, cela se fête n’est-ce pas ?");
		dialogue.sentences.Add("Ses collègues, avec notre accord, lui ont réservé une surprise dans le self.");
		dialogue.sentences.Add("Je vous donne accès au digicode, identifiez le puis ouvrez lui les portes. Je ne sais plus où il se trouve, référez vous à son planning.");
        
		FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(10f));
        orderText = "Trouvez le numéro de matricule de Jack, et rentrez le dans sur le digicode.";

        yield return new WaitForSeconds(9f);

        commandPanel.SetBool("isDigicodeAvailable", true);

		oscarOrderText.text = "Trouvez l'identifiant du responsable de la disparition d'Oscar.";
        
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
		doorNums.Add(6);
		doorNums.Add(7);
		numberOfGoodDoor = 4;
		doorAmmount = 7;

		StartCoroutine(DisplayOrder(1f));
		orderText = "Localisez Jack et ouvrez lui un accès au self. Son planning se trouve dans le manuel.";

		Dialogue dialogue2 = new Dialogue();
		dialogue2.sentences.Add("Bien vous l'avez localisé, ouvrez lui les portes vers le self.");

		FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);

        yield return new WaitForSeconds(1f);

        while (!finishedLevel)
		{
			if (keyPadCorrect)
			{
				finishedLevel = true;
			}
			yield return null;
		}

		StartCoroutine (MapFeedback (4,7));

		while (!doorFeedbackState)
		{
			yield return null;
		}

		//StopCoroutine("randomTalk");
        doorNums.Clear();
        StartCoroutine(mission6());
        resestMission();

        yield return null;
    }


    public IEnumerator mission6()
    {
        while (!ep.puzzleDone)
        {
            yield return null;
        }

        doorNums.Clear();
        StartCoroutine(mission7());
        resestMission();

        yield return null;
    }


    public IEnumerator mission7()
    {
        puzzleNum = 5;
        inLastPuzzle = true;

        GameManager.instance.flashKeypad = true;

        //StartCoroutine(randomTalk(puzzleNum));
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("4dialogue");

        Dialogue dialogue = new Dialogue();
        dialogue.sentences.Add("Vous savez Wilson, c’est en travaillant comme vous le faites \n que les employés se voient attribués une promotion et…");
        dialogue.sentences.Add("Attendez, la salle de tri est ouverte, c’est étrange… \n Personne n’est censé y avoir accès à cette heure-ci.");
        dialogue.sentences.Add("Je vais essayer de me renseigner si aucune fraude ou détérioration n’a eu lieu…");
        dialogue.sentences.Add("Essayez de trouver qui s’y trouve, vous devriez commencer \n par consulter le relevé d'incidents.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(12f));
        orderText = "Trouvez qui se trouve dans la salle tri à l'aide du relevé d'incidents puis entrez son identifiant sur le digicode.";

        yield return new WaitForSeconds(2f);

        while (!finishedStep01)
        {
            if (digiFinishPuzzle)
            {
                finishedStep01 = true;
            }
            yield return null;
        }

        Dialogue dialogue2 = new Dialogue();
        dialogue2.sentences.Add("Mhhh je vois. Mademoiselle Fanny Woods donc.\nElle a posé beaucoup de problème à l’entreprise auparavant.");
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
        dialogue3.sentences.Add("Parfait Wilson! Nous lui trouverons rapidement un remplaçant.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

		AudioManager.instance.PlaySound("weirdSoundsTri");
        doorNums.Clear();
        StartCoroutine(mission8());
        resestMission();
    }


    public IEnumerator mission8()
    {
        while (!ep.puzzleDone)
        {
            yield return null;
        }

        doorNums.Clear();
        resestMission();

        yield return null;
    }



    public void resestMission()
    {
        keyPadCorrect = false;
        finishedStep01 = false;
        finishedLevel = false;
		digiFinishPuzzle = false;
        doorFeedbackState = false;

        ep.puzzleDone = false;
        inExePuzzle = false;
        canStartExePuzzle = false;

        foreach (GameObject btn in digicode.keyButtons)
		{
			btn.GetComponent<Renderer>().material.color = Color.grey;
		}

		digicode.keycode = 0;
		digicode.enabledAmmount = 0;
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


	private void DoGoodDoorAnim(List<int> puzzleDoorNumber, bool doorState)
	{
		for (int i = 0; i < puzzleDoorNumber.Count; i++)
		{
			int door = puzzleDoorNumber[i] - 1;
			keypad.keyButtons [door].GetComponent<keyBtn> ().doorAnimator.SetBool ("Open", doorState);
		}
	}


	public IEnumerator MapFeedback(int startRoom, int endRoom)
	{
		bool done = false;
		bool state = true;
		DoGoodDoorAnim (doorNums, state);

		agent.transform.position = agent.GetComponent<AgentRegisteredPos>().pos[startRoom - 1].position;
		agent.SetActive (true);

		Transform finalLocation = agent.GetComponent<AgentRegisteredPos>().pos[endRoom - 1]; 

		NavMeshAgent mNavMeshAgent = agent.GetComponent<NavMeshAgent> ();

		mNavMeshAgent.SetDestination (finalLocation.position);


		while (!done) //TANT QUE LE AGENT N'A PAS ATTEINT DESTINATION
		{
			if (!mNavMeshAgent.pathPending)
			{
				if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
				{
					if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
					{
						done = true;
					}
				}
			}
			yield return null;
		}
			
		agent.SetActive (false);
		state = false;
		DoGoodDoorAnim (doorNums, state);
		doorFeedbackState = true;
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


    public IEnumerator DisplayOrder(float waitBeforDisplay)
    {
        recapText.text = "";

        yield return new WaitForSeconds(waitBeforDisplay);

        recapText.text = orderText;
    }


    private IEnumerator scrollingBabies()
    {
        doScrolling = true;

		yield return new WaitForSeconds(fadeTime + (timeInElevator * 0.60f));

		blockView.SetBool ("goDown", true); 
		tutoRotateObject.SetActive(true);

		yield return new WaitForSeconds(fadeTime + timeInElevator * 0.1f);

        doScrolling = false;
		Destroy (elevator);
    }
}