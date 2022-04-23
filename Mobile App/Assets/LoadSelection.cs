using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSelection : MonoBehaviour
{
    public Animator anim;

    public void Play()
    {
        StartCoroutine(LoadingPlay());
    }

    IEnumerator LoadingPlay()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(1);
    }

    public void Return()
    {
        StartCoroutine(LoadingMenu());
    }

    IEnumerator LoadingMenu()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(0);
    }
}
