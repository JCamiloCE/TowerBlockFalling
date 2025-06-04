using Emc2.Scripts.GameplayEvents;
using JCC.Utils.GameplayEventSystem;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener,
                                        IUnityAdsShowListener,
                                        IUnityAdsLoadListener,
                                        IEventListener<LoseGameEvent>
{
    [SerializeField] private bool _testMode = true;

    private string _gameID = "5868605";
    string _androidAdUnitId = "Interstitial_Android";

    #region IEventListener
    public void OnEvent(LoseGameEvent event_data)
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(_androidAdUnitId, this);
        }
    }
    #endregion

    #region private
    private void Start()
    {
        EventManager.AddListener(this);
        if (!Advertisement.isInitialized) 
        {
            Advertisement.Initialize(_gameID, _testMode, this);
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(this);
    }

    private void LoadVideo() 
    {
        Advertisement.Load(_androidAdUnitId, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("OnInitializationComplete");
        Invoke(nameof(LoadVideo), 3f);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"OnInitializationFailed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"OnUnityAdsShowFailure: {_androidAdUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"OnUnityAdsShowStart: {placementId} ");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"OnUnityAdsShowClick: {placementId} ");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"OnUnityAdsShowComplete: {placementId} - {showCompletionState}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"OnUnityAdsAdLoaded: {placementId} ");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"OnUnityAdsFailedToLoad: {_androidAdUnitId} - {error.ToString()} - {message}");
    }
    #endregion private
}
