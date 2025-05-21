using UnityEngine;
using UnityEngine.SceneManagement;

namespace Emc2.Scripts.Testing 
{
    public class ResetScene_Testing : MonoBehaviour
    {
        public void ResetScene() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}