using Emc2.Scripts.GameplayEvents;
using JCC.Utils.GameplayEventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Emc2.Scripts.UI
{
    public class UIGeneralCanvasController : MonoBehaviour, IEventListener<LoseGameEvent>
    {
        [SerializeField] private GameObject _loseScreen = null;

        #region IEventListener
        public void OnEvent(LoseGameEvent event_data)
        {
            _loseScreen.SetActive(true);
        }
        #endregion

        #region private
        private void Start()
        {
            _loseScreen.SetActive(false);
            EventManager.AddListener(this);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(this);
        }
        #endregion private

        #region public
        public void ResetLevel() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        #endregion public
    }
}