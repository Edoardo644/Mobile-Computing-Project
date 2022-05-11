using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSelection : MonoBehaviour
{
    public Animator anim;

    // CARICAMENTO LEVEL SELECTION
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

    // CARICAMENTO MENU PRINCIPALE
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

    // CARICAMENTO LIVELLO ALIENI
    public void LevelA()
    {
        StartCoroutine(LoadingA());
    }

    IEnumerator LoadingA()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(2);
    }

    // CARICAMENTO LIVELLO DEMONI
    public void LevelD()
    {
        StartCoroutine(LoadingD());
    }

    IEnumerator LoadingD()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(3);
    }

    // CARICAMENTO LIVELLO SCHELETRI
    public void LevelS()
    {
        StartCoroutine(LoadingS());
    }

    IEnumerator LoadingS()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(4);
    }

    // CARICAMENTO LIVELLO SUCCESSIVO
    public void NextLevel()
    {
        StartCoroutine(LoadingNext());
    }

    IEnumerator LoadingNext()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // S/BLOCCO TEMPO
    public void TimeStop()
    {
        Time.timeScale = 0f;
    }

    public void TimePlay()
    {
        Time.timeScale = 1f;
    }

    // RICARICA LIVELLO CORRENTE
    public void Retry()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
