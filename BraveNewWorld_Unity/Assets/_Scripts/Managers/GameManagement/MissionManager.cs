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
    public Transform instantiateTransform;
    public GameObject instance1;
    public GameObject instance2;
    public GameObject instance3;

    [HideInInspector] public bool goToNextStep;
    [HideInInspector] public bool isInElevator;
    [HideInInspector] public bool isInHall;
    [HideInInspector] public bool enterInRoom;
    [HideInInspector] public bool closeDoor;
	[HideInInspector] public bool isAtDesk;

    private bool doScrolling;
    private float counter;


    [Header("Missions Settings")]

    [Range(210f,310f)] public float timeBeforeHint;
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
            if(counter >= 2.15f)
            {
				Instantiate(babiesPrefab, instantiateTransform.position, Quaternion.Euler(new Vector3(-180,-135,0)));
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

		StartCoroutine(randomTalk(puzzleNum));
        StartCoroutine(GiveHint(puzzleNum));

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

        resestMission();
        StartCoroutine (mission2());

        StopCoroutine("randomTalk");
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

		StartCoroutine(randomTalk(puzzleNum));
        StartCoroutine(GiveHint(puzzleNum));

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

		StopCoroutine("randomTalk");
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

        StartCoroutine(DisplayOrder(11f));
        orderText = "Écoutez les ordres du directeur.";

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
        puzzleNum = 4;
        doorNums.Add(6);
        doorNums.Add(2);
        numberOfGoodDoor = 2;
        doorAmmount = 7;

		StartCoroutine(randomTalk(puzzleNum));
        StartCoroutine(GiveHint(puzzleNum));
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

        StopCoroutine("randomTalk");
        doorNums.Clear();
        StartCoroutine(mission5());
        resestMission();

        yield return null;
    }


	public IEnumerator mission5()
    {
		hideDigicode = true;
		searchJack = true;

		puzzleNum = 5;

        GameManager.instance.flashKeypad = false;

		StartCoroutine(randomTalk(puzzleNum));
        StartCoroutine(GiveHint(puzzleNum));
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

		StopCoroutine("randomTalk");
        doorNums.Clear();
        resestMission();
        StartCoroutine(mission6());

        yield return null;
    }


    public IEnumerator mission6()
    {
        puzzleNum = 6;
        inExePuzzle = true;
        canStartExePuzzle = false; // RESET MANUEL ? 

        Dialogue dialogue = new Dialogue();
        dialogue.sentences.Add("Merci Wilson, merci pour Jack ! Oh mais c’est déjà l’heure du conditionnement, nous sommes en retard !");
        dialogue.sentences.Add("Il va falloir régler ça, écoutez moi attentivement je vous indiquerai la marche à suivre."); 
        dialogue.sentences.Add("Vous êtes devant le panneau de commandes? Bien.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(10f));
        orderText = "Écoutez les ordres du directeur.";

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
        StartCoroutine(mission7());

        yield return null;
    }


    public IEnumerator mission7()
    {
        puzzleNum = 5;
        inLastPuzzle = true;

        GameManager.instance.flashKeypad = true;

        StartCoroutine(randomTalk(puzzleNum));
        StartCoroutine(GiveHint(puzzleNum));
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("4dialogue");

        Dialogue dialogue = new Dialogue();
        dialogue.sentences.Add("Merci, voilà un conditionnement parfaitement réalisé");
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

        StopCoroutine("GiveHint");

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

        StopCoroutine("randomTalk");
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayMusic("5dialogue");

        Dialogue dialogue3 = new Dialogue();
        dialogue3.sentences.Add("Parfait Wilson! Nous lui trouverons rapidement un remplaçant.");
        FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);

		AudioManager.instance.PlaySound("weirdSoundsTri");

        yield return new WaitForSeconds(5f);

        doorNums.Clear();
        resestMission();
        StartCoroutine(mission8());
    }


    public IEnumerator mission8()
    {
        commandPanel.SetBool("isDigicodeAvailable", true);

        puzzleNum = 8;
        inExePuzzle = true;
        canStartExePuzzle = false;  

        Dialogue dialogue = new Dialogue();
        dialogue.sentences.Add("Nous venons d'interroger le responsable de la sécurité, nous nous sommes rendu compte qu’il n’était pas en état d'exercer sa fonction ce matin.");
        dialogue.sentences.Add("Il a négligé beaucoup de possibilité quant à la fuite d’Oscar.");
        dialogue.sentences.Add("Nous allons devoir nous en occuper nous-mêmes ! Il y a nombre de couloirs et de conduits à vérifier.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue);

        StartCoroutine(DisplayOrder(10f));
        orderText = "Écoutez les ordres du directeur.";

        while (!canStartExePuzzle)
        {
            yield return null;
        }

        ep.StartPuzzle(puzzleNum);

        while (!ep.puzzleDone)
        {
            yield return null;
        }

        keypad.ComfirmInput(); // APPELLE COMFIRMINPUT POUR FEEDBACK FLASH ET SON

        yield return new WaitForSeconds(1f);

        Dialogue dialogue1 = new Dialogue();
        dialogue1.sentences.Add("Tiens sacré Oscar c’est donc là que tu es parti ! Rah j’ai peur de ce qui a pu lui arriver...");
        dialogue1.sentences.Add("J’aimerais que vous rendiez un service à l’entreprise, ce qui jouerait, je vous l’avoue, beaucoup sur vos chances de promotions.");
        dialogue1.sentences.Add("Pourriez-vous jeter un oeil derrière cette grille de ventilation au fond de la pièce ?");
        dialogue1.sentences.Add("Le code est normalement 461.");

        FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);

        StartCoroutine(DisplayOrder(15f));
        orderText = "Ouvrez la grille de ventilation, le code est 461.";

        yield return new WaitForSeconds(20f);

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
        int i = Random.Range(1, 7);
        yield return new WaitForSeconds(180f);
        if (missionLevel == puzzleNum)
        {
            switch (i)
            {
                case 1:
                    Debug.Log("Rand 1 ");
                    //AudioManager.instance.PlayMusic("rand1");
                    Dialogue dialogue1 = new Dialogue();
                    dialogue1.sentences.Add("Vous avez sûrement eu vent du séisme au Japon, pas vrai ? Quels sont les chiffres déjà ?");
                    dialogue1.sentences.Add("15 000 morts ? Plus ? Il y aura des répercussions au niveau international, je vous le garantis.");
                    dialogue1.sentences.Add("Enfin au moins ça va nous faire du travail, nous devrions nous réjouir !");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);
                    break;
                case 2:
                    Debug.Log("Rand 2 ");
                    //AudioManager.instance.PlayMusic("rand2");
                    Dialogue dialogue2 = new Dialogue();
                    dialogue2.sentences.Add("Plus je prend de l’âge, plus j'apprécie rester dans mon bureau.");
                    dialogue2.sentences.Add("C’est grand, lumineux et calme, croyez moi Wilson, le parfait endroit pour travailler sereinement.");
                    dialogue2.sentences.Add("Je pourrais passer ma vie dans cette usine !");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);
                    break;
                case 3:
                    Debug.Log("Rand 3 ");
                    //AudioManager.instance.PlayMusic("rand3");
                    Dialogue dialogue3 = new Dialogue();
                    dialogue3.sentences.Add("Vous avez entendu parler de ce réseau de contestataire ?");
                    dialogue3.sentences.Add("Je ne les comprends pas Wilson, peut-être devraient-ils consulter plutôt que de perturber le monde…");
                    dialogue3.sentences.Add("Et ce Kreep, quelles folles idées le pousse à agir ainsi ? Ils doivent en souffrir croyez-moi.");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);
                    break;
                case 4:
                    Debug.Log("Rand 4 ");
                    //AudioManager.instance.PlayMusic("rand2");
                    Dialogue dialogue4 = new Dialogue();
                    dialogue4.sentences.Add("Je me demandais Wilson, pensez vous que les castes inférieures se laissent pousser la barbe pour dissimuler leur visage, ou s’agit-il d’un phénomène de mode ?");
                    dialogue4.sentences.Add("J’ai ma petite idée mais dans l’absolu, il faudrait comparer avec d’autres régions.");
                    dialogue4.sentences.Add("Après tout ils arrivent bien à compter jusqu’à cinq ahah !");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogue4);
                    break;
                case 5:
                    Debug.Log("Rand 5 ");
                    Dialogue dialogue5 = new Dialogue();
                    dialogue5.sentences.Add("Scarlet a encore oublié de déposer les inventaires sur mon bureau. Enfin, sans elle j’aurais eu plus d’une fois la tête sous l’eau.");
                    dialogue5.sentences.Add("J’imagine que je vais passer l’éponge, encore une fois.");
                    dialogue5.sentences.Add("Elle se débrouille quand même plutôt bien pour une Béta, prenez-en de la graine Wilson.");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogue5);
                    //AudioManager.instance.PlayMusic("rand5");
                    break;
                case 6:
                    Debug.Log("Rand 6 ");
                    Dialogue dialogue6 = new Dialogue();
                    dialogue6.sentences.Add("Alors Wilson, ce métier est bien plus complexe qu’il n’en a l’air pas vrai ?");
                    dialogue6.sentences.Add("J’en ai conscience, doutez vous bien que si il s’agissait simplement de rester assis à un bureau et d’ouvrir des portes, un simple delta moins aurait suffit.");
                    dialogue6.sentences.Add("Vous prendrez le rythme, ne vous inquiétez pas !");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogue6);
                    //AudioManager.instance.PlayMusic("rand5");
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
        doScrolling = false;

        instance1.SetActive(true);
        instance2.SetActive(true);
        instance3.SetActive(true);

        yield return new WaitForSeconds(fadeTime + timeInElevator * 0.1f);

		Destroy (elevator);
    }


    private IEnumerator GiveHint(float puzzleID)
    {
        yield return new WaitForSeconds(timeBeforeHint);

        if(puzzleID == 1)
        {
            Dialogue dialogue1 = new Dialogue();
            dialogue1.sentences.Add("Bon, ça va que c’est votre premier jour.");
            dialogue1.sentences.Add("Vous devriez trouver les informations dont vous avez besoin dans le manuel d’utilisation. Courage !");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);
        }
        else if (puzzleID == 2)
        {
            Dialogue dialogue2 = new Dialogue();
            dialogue2.sentences.Add("Faites un effort Wilson ! La liste de matériel se trouve elle aussi dans le manuel.");
            dialogue2.sentences.Add("Et le matériel défectueux et bien faites preuve d’un peu de bon sens !");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue2);
        }
        else if (puzzleID == 4)
        {
            Dialogue dialogue3 = new Dialogue();
            dialogue3.sentences.Add("Bon ! Je vois que vous avez des difficultés, je n’ai pas l’habitude de travailler avec des gens de votre caste après tout… ");
            dialogue3.sentences.Add("Peut-être devriez-vous commencer par trouver la date d’aujourd’hui ?");
            dialogue3.sentences.Add("Vous n’auriez plus qu’à trouver à quoi la livraison correspond dans la liste de matériel du manuel.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue3);
        }
        else if (puzzleID == 5)
        {
            Dialogue dialogue4 = new Dialogue();
            dialogue4.sentences.Add("Je vois que vous avez décidé de ne pas y mettre du vôtre Wilson. Soyez soucieux de votre bonheur et conscient de votre chance d’être ici.");
            dialogue4.sentences.Add("Il vous suffit de trouver l’identifiant de Jack, sa caste devrait vous permettre de savoir où il se trouve. Reprenez-vous !");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue4);
        }
        else if (puzzleID == 7)
        {
            Dialogue dialogue5 = new Dialogue();
            dialogue5.sentences.Add("Je vois, un vol de badge a eu lieu en salle de conditionnement, vous n’avez plus qu’à trouver qui s’y trouvait à ce moment-là.");
            dialogue5.sentences.Add("Soyez plus vif la prochaine fois, je pense que vous souhaitez rester parmi nous.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue5);
        }

        yield break;
    }
}