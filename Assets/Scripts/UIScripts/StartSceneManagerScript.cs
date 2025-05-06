using UnityEngine;

public class StartSceneManagerScript : MonoBehaviour
{
    public void Challenge()
    {
        // Load the challenge scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
