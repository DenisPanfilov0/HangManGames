using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void Load()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
