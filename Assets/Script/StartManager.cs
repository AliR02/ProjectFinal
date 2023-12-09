
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour//load personal project scene(game)
{
    public string SceneName;
    public void goToNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
