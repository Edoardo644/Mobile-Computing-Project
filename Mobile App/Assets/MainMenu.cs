using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform camera;

    public void PlayGame()
    {
        camera.position = new Vector3(22.0189991f, camera.position.y, camera.position.z);
        //camera.position = new Vector3(camera.position.x + Time.deltaTime * 2f, camera.position.y, camera.position.z);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
