using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play: MonoBehaviour
{
    [SerializeField] bool isClassik;
    [SerializeField] bool isCosmos;
    [SerializeField] bool isBalls;
    string sceneName;

    private void Start()
    {
        if (isClassik) { sceneName = "Classik"; }
        if (isCosmos) { sceneName = "Space"; }
        if (isBalls) { sceneName = "Sport"; }
    }

    public void LetTheGameStart()
    {
        SceneManager.LoadScene(sceneName);
    }
}

