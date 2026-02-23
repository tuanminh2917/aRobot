using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    [SerializeField] Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(() => 
        {
            // Load the game scene when the button is clicked
            // SceneManager.LoadScene("MainGameAndroid");
            SceneManager.LoadScene("MainGame 1");
        });
    }

}
