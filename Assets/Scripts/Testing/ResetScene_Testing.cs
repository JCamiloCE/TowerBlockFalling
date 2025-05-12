using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene_Testing : MonoBehaviour
{
    public void ResetScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
