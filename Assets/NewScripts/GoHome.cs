using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{
    public void GoHomeNow()
    {
        Time.timeScale = 1.0f;

        bool doWeNeedToGoHome = true;
        if (doWeNeedToGoHome ) { SceneManager.LoadScene("MM"); }
    }
}
