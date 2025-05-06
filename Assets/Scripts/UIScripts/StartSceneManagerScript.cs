using UnityEngine;

public class StartSceneManagerScript : MonoBehaviour
{
    ///// BUTTONS /////
    public void Challenge()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
