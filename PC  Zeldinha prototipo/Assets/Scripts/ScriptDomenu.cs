using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;






public class ScriptDomenu : MonoBehaviour
{
    public void IniciarGame () 
    {
        SceneManager.LoadScene("Test");
    }
    public void FecharJogo()
    {
        Application.Quit ();
        Debug.Log("FechouOJOgo");
    }
}
