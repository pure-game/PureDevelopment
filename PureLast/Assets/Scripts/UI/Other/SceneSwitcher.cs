using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadStartGame()
    {
        SceneManager.LoadScene("PROCEDURE", LoadSceneMode.Single);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MAINMENU", LoadSceneMode.Single);
    }
    public void LoadShop()
    {
        SceneManager.LoadScene("SHOP", LoadSceneMode.Single);
    }
    public void LoadBeasts()
    {
        SceneManager.LoadScene("BEASTS", LoadSceneMode.Single);
    }
}
