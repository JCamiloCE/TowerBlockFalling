using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Testing 
{
    public class ResetScene_Testing : MonoBehaviour
    {
        public void ResetScene() 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}