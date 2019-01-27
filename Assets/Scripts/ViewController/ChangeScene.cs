using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene :MonoBehaviour
{
    public void LoadScene(int sceneId)
    {

        SceneManager.LoadScene(sceneId);
    }
}
