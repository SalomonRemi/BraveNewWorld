using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.SoundManagerNamespace;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

    [Header("Book Settings")]
	public GameObject manualUi;
    public bool closeDocs = false;
    public bool manualVisible = false;
    public bool documentOpen = false;
    public Sprite[] pagesSprites;
    public Image turnLeftSign;
    public Image turnRightSign; 

    [Header("Pause Settings")]
    public bool quitPause = false;
    public bool isPaused = false;
    public GameObject menuScreen;

    [Header("Line Of Sight")]
    public bool canLOS = false;
    public Collider chariotCol;

    private bool sawChariot;

    [Header("Other")]
    public bool flashKeypad = true;
    public bool doorOpen = false;


    private bool added = false;

    GameObject player;


    void Awake()
	{
		if (instance == null)
        {
			instance = this;
		}
        else if (instance != this)
        {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}


	void Start ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void Update ()
    {
		if ((Input.GetKeyDown (KeyCode.Escape) || quitPause) && !documentOpen && !manualVisible)
        {
			if (isPaused) // Si le jeu est en pause, je reprend
            {
				quitPause = false;
				Time.timeScale = 1f;
				isPaused = false;
				menuScreen.SetActive (false);
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				player.GetComponent<FPSController> ().enabled = true;
                AudioManager.instance.ChangePitchByTime();
			}
            else // Si je met le jeu en pause avec ESCAPE, ou que je suis en pause, je me met en pause
            {
				Time.timeScale = 0f;
				isPaused = true;
				menuScreen.SetActive (true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				player.GetComponent<FPSController> ().enabled = false;
                AudioManager.instance.ChangePitchByTime();
            }
		}

		if (!isPaused)
        {
			if (manualVisible) // GERE LES MANUELS
            {
				if (pagesSprites.Length != 0)
                {
					addPages ();
                
					player.GetComponent<FPSController> ().enabled = false;
					manualUi.SetActive (true);

					if ((Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown("Fire2") || closeDocs) && !manualUi.GetComponent<AutoFlip> ().isFlipping)
                    {
						manualVisible = false;
						closeDocs = false;
					}
				}
                else
                {
					pagesSprites = new Sprite[pagesSprites.Length];
					added = false;
					manualUi.SetActive (false);

                    if (!isPaused)
                    {
						player.GetComponent<FPSController> ().enabled = true;
					}
				}

                if(Input.GetKeyDown(KeyCode.Q))
                {
                    manualUi.GetComponent<AutoFlip>().FlipLeftPage();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    manualUi.GetComponent<AutoFlip>().FlipRightPage();
                }

                if (manualUi.GetComponent<AutoFlip>().ControledBook.currentPage >= manualUi.GetComponent<AutoFlip>().ControledBook.TotalPageCount)
                {
                    turnRightSign.enabled = false;
                }
                else turnRightSign.enabled = true;
                if (manualUi.GetComponent<AutoFlip>().ControledBook.currentPage <= 0)
                {
                    turnLeftSign.enabled = false;
                }
                else turnLeftSign.enabled = true;
            }
            else
            {
				pagesSprites = new Sprite[pagesSprites.Length];
				added = false;
				manualUi.SetActive (false);
				if (!isPaused)
                {
					player.GetComponent<FPSController> ().enabled = true;
				}
			}

			if (documentOpen) // GERE LES DOC
            {
				player.GetComponent<FPSController>().enabled = false;

				if (Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown("Fire2") || closeDocs) // FERMER LE DOC
                {
					documentOpen = false;
					closeDocs = false;
				}
			}
		}

        if(canLOS)
        {
            PlayerManagers.instance.LineOfSight();

            if (PlayerManagers.instance.LOS_objectCol == chariotCol && !sawChariot)
            {
                if(Vector3.Distance(player.transform.position, chariotCol.transform.position) < 5f)
                {
                    Dialogue dialogueChariot = new Dialogue();
                    dialogueChariot.sentences.Add("Ce que vous voyez ici sont des conteneurs pour Epsilons. Le service d'entretient passera les chercher demain.");
                    FindObjectOfType<DialogSystem>().StartDialogue(dialogueChariot);
                    sawChariot = true;
                    AudioManager.instance.PlayMusic("losChariot");
                }
            }
        }
	}

	void addPages()
    {
		if (!added)
        {
			added = true;
			manualUi.GetComponent<Book>().bookPages = pagesSprites;
			manualUi.GetComponent<Book>().currentPage = 0;
            manualUi.GetComponent<Book>().UpdateSprites();
        }
	}
}