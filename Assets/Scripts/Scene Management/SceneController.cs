using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /*
     * Picturing 3 scenes from handwritten notes Rue made + title screen 
     * So, Bowels Scene, Bay Scene, & Deck Scene
     * Bowels to Bay:
     * Player needs Maintenance worker access key, High Maintenance key, & Elevator Emergency Override key 
     * Bay to Deck:
     * AW access key, Emergency Shield, Living Quarters Access Badge 
     * for now, the player just needs to walk to the edge of the map to get to the next stage
     */
    public GameObject player;
    public GameObject checkpoint;
    public Scene sceneLoaded;
    void Start()
    {
        sceneLoaded = SceneManager.GetActiveScene();
    }

    
    void Update()
    {
        if (player.transform.localPosition.x > checkpoint.transform.localPosition.x - 3 && player.transform.localPosition.x < checkpoint.transform.localPosition.x + 3)
        {
            SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
        }     
    }
}
