using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Cursor Variables")]
    
    public Texture2D cursorTexture;
    private Vector2 cursorHotspot;


    [Header("Main Menu Variables")]

    [SerializeField] GameObject firstPanel;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject mainMenuImage;
    
    [SerializeField] GameObject startFadeInPanel;

    
    
    
    
    
    [Header("The Story  Bit")]

    public bool storyBitStarter = false;
    private bool firstBitBool = false;
    private bool secondBitBool = false;
    private bool thirdBitBool = false;

    [SerializeField] GameObject pressAnyKey;
    [SerializeField] GameObject firstBitText;
    [SerializeField] GameObject secondBitText;
    [SerializeField] AudioClip walkingOutside;
    [SerializeField] GameObject thirdBitText;

    [SerializeField] AudioClip doorShutClip;

    [SerializeField] GameObject streetSoundSource;
    private bool streetSoundSourceBool;
    [SerializeField] GameObject officeSoundSource;
    private bool officeSoundSourceBool;

    

    

    [Header("The Contract")]

    [SerializeField] GameObject theContract;
    [SerializeField] TMP_InputField yourName;
    [SerializeField] GameObject yourNameHere;
    
    //transition
    [SerializeField] GameObject tournamentManager;

    [SerializeField] AudioClip signingSFX;
    [SerializeField] GameObject exitFadeInPanel;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        
        StartCoroutine("firstPanelFunc");


    }

    private void Update()
    {
       if(streetSoundSourceBool)
        {
            streetSoundSource.GetComponent<AudioSource>().volume = Mathf.MoveTowards(streetSoundSource.GetComponent<AudioSource>().volume, 0.2f, 0.1f * Time.deltaTime);
        }
       else if(!streetSoundSourceBool && streetSoundSource.GetComponent<AudioSource>().volume != 0)
        {
            streetSoundSource.GetComponent<AudioSource>().volume = Mathf.MoveTowards(streetSoundSource.GetComponent<AudioSource>().volume, 0, 0.15f * Time.deltaTime);
        }

       if(officeSoundSourceBool)
        {
            officeSoundSource.GetComponent<AudioSource>().volume = Mathf.MoveTowards(officeSoundSource.GetComponent<AudioSource>().volume, 0.5f, 0.1f * Time.deltaTime);
        }
       else if(!officeSoundSourceBool && officeSoundSource.GetComponent<AudioSource>().volume != 0)
        {
            officeSoundSource.GetComponent<AudioSource>().volume = Mathf.MoveTowards(officeSoundSource.GetComponent<AudioSource>().volume, 0.3f, 0.1f * Time.deltaTime);
        }

        
        if (storyBitStarter)
        {
            if (firstBitBool && Input.anyKeyDown)
            {
                firstBitBool = false;
                StartCoroutine("firstStoryBitFunc");
                
                
            }
            else if (secondBitBool && Input.anyKeyDown)
            {
                secondBitBool = false;
                StartCoroutine("secondStoryBitFunc");
            }
            else if (thirdBitBool && Input.anyKeyDown)
            {
                
                thirdBitBool = false;
                StartCoroutine("thirdStoryBitFunc");

            }
        }
        else return;

    }

    IEnumerator firstPanelFunc()
    {
        firstPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        firstPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        firstPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.ForceSoftware);

    }

    public void playButtonFunc()
    {
        StartCoroutine("mainMenuBitsAndSound");
        
    }

    public void exitButtonFunc()
    {
        Application.Quit();
    }

    public void signButton()
    {
        if (yourName.text.Length < 1)
        {
            StartCoroutine("yourNameHereSir");
        }

        else
        {
            StartCoroutine("weGoIn");
        }
    }

    //main menu to story bit 

    IEnumerator mainMenuBitsAndSound()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startFadeInPanel.SetActive(true);
        startFadeInPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        mainMenuImage.SetActive(false);
        mainMenu.SetActive(false);
        firstBitText.SetActive(true);
        pressAnyKey.SetActive(true);
        // street sounds goes in and the texts
        streetSoundSourceBool = true;
        streetSoundSource.GetComponent<AudioSource>().Play(0);
        firstBitText.GetComponent<Animator>().SetTrigger("FadeIn");
        pressAnyKey.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        storyBitStarter = true;
        firstBitBool = true;
    }

    IEnumerator firstStoryBitFunc()
    {
        firstBitText.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        firstBitText.SetActive(false);
        secondBitText.SetActive(true);
        secondBitText.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        secondBitBool = true;

    }

    IEnumerator secondStoryBitFunc()
    {
        GetComponent<AudioSource>().PlayOneShot(walkingOutside);
        secondBitText.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        thirdBitText.SetActive(true);
        thirdBitText.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        thirdBitBool = true;

    }

    IEnumerator thirdStoryBitFunc()
    {
        officeSoundSourceBool = true;
        streetSoundSourceBool = false;
        officeSoundSource.GetComponent<AudioSource>().Play(0);
        GetComponent<AudioSource>().PlayOneShot(doorShutClip, 0.5f);
        thirdBitText.GetComponent<Animator>().SetTrigger("FadeOut");
        pressAnyKey.GetComponent<Animator>().SetTrigger("FadeOut");
        theContract.SetActive(true);
        yield return new WaitForSeconds(3);
        startFadeInPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        startFadeInPanel.SetActive(false);
        

    }



    //the contract part
    IEnumerator setTheContractUp()
    {
        yield return new WaitForSeconds(1.5f);
        startFadeInPanel.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        startFadeInPanel.SetActive(false);
    }

    IEnumerator yourNameHereSir()
    {
        yourNameHere.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        yourNameHere.SetActive(false);
    }
    
    IEnumerator weGoIn()
    {
        GetComponent<AudioSource>().PlayOneShot(signingSFX);
        yield return new WaitForSeconds(2);
        exitFadeInPanel.SetActive(true);
        ValueManager.playerName = yourName.text.ToString();
        exitFadeInPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        yield return new WaitForSeconds(2);
        tournamentManager.GetComponent<TournamentManager>().nextScene();
    }

    
    

}
