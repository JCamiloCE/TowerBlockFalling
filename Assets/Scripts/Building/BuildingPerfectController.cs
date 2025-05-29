using Emc2.Scripts.GameplayEvents;
using JCC.Utils.GameplayEventSystem;
using UnityEngine;

public class BuildingPerfectController : MonoBehaviour, IEventListener<PerfectBlockFalledEvent>
{
    [SerializeField] private float _timeInPerfect = 3f;

    private bool _isInPerfectTime = false;
    private float _timeLastPerfect = 0;

    public bool IsInPerfectTime => _isInPerfectTime;

    #region IEventListener
    public void OnEvent(PerfectBlockFalledEvent event_data)
    {
        if (!_isInPerfectTime)
        {
            _isInPerfectTime = true;
            EventManager.TriggerEvent<PerfectTimeEvent>(true);
        }
        _timeLastPerfect = Time.time;
    }
    #endregion

    #region private
    private void Start()
    {
        EventManager.AddListener(this);
    }

    public void OnDestroy() 
    {
        EventManager.RemoveListener(this);
    }

    private void Update()
    {
        if (_isInPerfectTime) 
        {
            if (_timeLastPerfect + _timeInPerfect < Time.time) 
            {
                _isInPerfectTime = false;
                EventManager.TriggerEvent<PerfectTimeEvent>(false);
            }
        }
    }
    #endregion private
}
