using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionHandeler : MonoBehaviour
{
    Movment movment;

    private void Start()
    {
        movment = GetComponent<Movment>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Frendly":
                print("I Hit a Frendly");
                break;
            case "Finish":
                collision.gameObject.GetComponent<AudioSource>().Play();
                Invoke("loadLevel", 3);
                break;
            default:
                startCrashSequence();
                break;

        }
    }

    void loadLevel()
    {
        int NextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (NextScene == SceneManager.sceneCountInBuildSettings)
        {
            NextScene = 0;
        }
        SceneManager.LoadScene(NextScene);
    }

    void startCrashSequence()
    {
        movment.enabled = false;
        GetComponent<AudioSource>().Stop();
        Invoke("restartGame", 3);
    }

    void restartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
