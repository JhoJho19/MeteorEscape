using Google.Play.AppUpdate;
using Google.Play.Common;
using Google.Play.Review;
using Google.Play.Integrity;
using System.Collections;
using UnityEngine;
using Google.Play.Instant;

public class Updater : MonoBehaviour
{
    private AppUpdateManager _appUpdateManager;
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;
    private IntegrityManager _integrityManager;
    bool isReview;
    bool isInstant;
    bool isIntegrity;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _appUpdateManager = new AppUpdateManager();
            _reviewManager = new ReviewManager();
            _integrityManager = new IntegrityManager();

            StartCoroutine(CheckForUpdate());
            if (isReview)
            StartCoroutine(RequestReviewFlowAsync());
            if (isInstant)
            StartCoroutine(InitializePlayIntegrity());
            if (isIntegrity)
            InstallLauncher.ShowInstallPrompt();
        }
    }

    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = _appUpdateManager.GetAppUpdateInfo();

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
            Debug.LogError("App update info operation error: " + appUpdateInfoOperation.Error.ToString());
        }
    }

    private IEnumerator RequestReviewFlowAsync()
    {
        PlayAsyncOperation<PlayReviewInfo, ReviewErrorCode> requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;

        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.LogError("Review flow request error: " + requestFlowOperation.Error.ToString());
            yield break;
        }

        _playReviewInfo = requestFlowOperation.GetResult();
        PlayAsyncOperation<VoidResult, ReviewErrorCode> launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;

        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.LogError("Review flow launch error: " + launchFlowOperation.Error.ToString());
            yield break;
        }

        // Review flow was successfully launched
    }

    private IEnumerator InitializePlayIntegrity()
    {
        var integrityTokenRequest = new IntegrityTokenRequest("sn2", 34343542);
        PlayAsyncOperation<IntegrityTokenResponse, IntegrityErrorCode> integrityOperation = _integrityManager.RequestIntegrityToken(integrityTokenRequest);
        yield return integrityOperation;

        if (integrityOperation.IsSuccessful)
        {
            var integrityTokenResponse = integrityOperation.GetResult();
            string integrityToken = integrityTokenResponse.Token;
            Debug.Log("Integrity token received: " + integrityToken);
            // Continue with the rest of your logic here, e.g., send the token to your server for validation
        }
        else
        {
            Debug.LogError("Play Integrity API error: " + integrityOperation.Error.ToString());
        }
    }
}
