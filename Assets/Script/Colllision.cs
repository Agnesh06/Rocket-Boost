using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Colllision : MonoBehaviour
{
    [SerializeField] float LevelDelayinput = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successPartical;
    [SerializeField] ParticleSystem crashPartical;
    AudioSource audioSource;

    bool isControlable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


    }
    void Update()
    {
        //RespondToDebugKeys();
    }

    /*void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            NextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
}*/
    void OnCollisionEnter(Collision collision)
    {
        if(!isControlable || !isCollidable ){ return; }
        switch (collision.gameObject.tag)
        {
            case "Start":
                Debug.Log("Game starts");
                break;
            case "Finish":
                
                SucessSequence();

                break;
            default:
                
                CrashSequence();
                break;

        }
    }
    void SucessSequence()
    {
        isControlable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successPartical.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", LevelDelayinput);
    }
     void CrashSequence()
    {
        audioSource.Stop();
        isControlable = false;
        audioSource.PlayOneShot(crash);
        crashPartical.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", LevelDelayinput);
    }

    void NextLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        int NextScene = CurrentScene + 1;
        if (NextScene == SceneManager.sceneCountInBuildSettings)
        {
            NextScene = 0;
        }
        SceneManager.LoadScene(NextScene);
      
    }
    void ReloadScene()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }
}

