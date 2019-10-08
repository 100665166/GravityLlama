using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpaceToPlay : MonoBehaviour
{
    //void OnGUI()
    //{
    //    Event e = Event.current;
    //    if (e.isKey)
    //    {
    //        Debug.Log("Detected key code: " + e.keyCode);
    //    }
    //}
    bool playable = false;
    void Start()
    {
        StartCoroutine(PauseTutorial());
    }

    void Update()
    {
        if (playable)
        {
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("SpaceToPlay");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    IEnumerator PauseTutorial()
    {
        yield return new WaitForSeconds(7);
        Debug.Log("Wait 7s");
        playable = true;
    }
}
