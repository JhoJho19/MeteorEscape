using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Rate : MonoBehaviour
{
    public void GameRating()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }
}
