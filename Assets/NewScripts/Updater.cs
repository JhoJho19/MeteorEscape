using Google.Play.AppUpdate;
using Google.Play.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    //AppUpdateManager appUpdateManager = new AppUpdateManager();

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AppUpdateManager appUpdateManager = new AppUpdateManager();
            StartCoroutine(CheckForUpdate(appUpdateManager));
        }
    }

    IEnumerator CheckForUpdate(AppUpdateManager appUpdateManager)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
        appUpdateManager.GetAppUpdateInfo();

            // Wait until the asynchronous operation completes.
            yield return appUpdateInfoOperation;

            if (appUpdateInfoOperation.IsSuccessful)
            {
                var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
                // Check AppUpdateInfo's UpdateAvailability, UpdatePriority,
                // IsUpdateTypeAllowed(), etc. and decide whether to ask the user
                // to start an in-app update.
            }
            else
            {
                // Log appUpdateInfoOperation.Error.
            }
        }    
    }
}
