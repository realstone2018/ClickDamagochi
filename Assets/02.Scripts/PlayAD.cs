using UnityEngine;
using UnityEngine.Advertisements;

public class PlayAD : MonoBehaviour {

#if UNITY_ANDROID
    private string gameId = "1486550";
#endif

    public string placementId = "rewardedVideo";

    void Start()
    {
        Advertisement.Initialize(gameId, true);
    }

    public void ShowRewardedVideo()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        if (Advertisement.IsReady(placementId))
            Advertisement.Show(placementId);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            // Reward your player here.

        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
