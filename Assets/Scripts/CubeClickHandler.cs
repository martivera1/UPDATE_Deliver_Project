using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeClickHandler : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Cube clicked!");
        // Reemplaza "MainGameScene" con el nombre de tu escena principal
        SceneManager.LoadScene("Scene");
    }
}
