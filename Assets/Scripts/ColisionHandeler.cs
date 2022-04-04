using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandeler : MonoBehaviour
{
    Movment movment;
    AudioSource audioSource;
    public AudioClip sucsess;
    public AudioClip crash;

    public ParticleSystem sucsessParticleSystem;
    public ParticleSystem crashParticleSystem;

    bool isTransitioning = false;
    bool disableCollision = false;


    private void Start()
    {
        movment = GetComponent<Movment>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        respondToDebugKeys();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) { return; }

        switch (collision.gameObject.tag)
        {
            case "Frendly":
                print("I Hit a Frendly");
                break;
            case "Finish":
                startFinishSequence();
                break;
            default:
                startCrashSequence();
                break;
        }

    }

    void loadLevel()
    {
        isTransitioning = true;
        int NextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (NextScene == SceneManager.sceneCountInBuildSettings)
        {
            NextScene = 0;
        }
        SceneManager.LoadScene(NextScene);
    }

    void startFinishSequence()
    {
        audioSource.PlayOneShot(sucsess);
        sucsessParticleSystem.Play();
        Invoke("loadLevel", 3);
    }

    void startCrashSequence()
    {
        if (disableCollision) { return;}
        isTransitioning = true;
        audioSource.Stop();
        crashParticleSystem.Play();
        audioSource.PlayOneShot(crash);
        movment.enabled = false;
        Invoke("restartGame", 3);
    }

    void restartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void respondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            loadLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            disableCollision = !disableCollision;
        }
    }
}
