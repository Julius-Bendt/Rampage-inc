using UnityEngine;
using System.IO;
using Juto;
using UnityEngine.SceneManagement;

public class App : Singleton<App>
{
    // (Optional) Prevent non-singleton constructor use.
    protected App() { }



    public bool isPlaying = false;
    public bool isNewGame = false;

    public bool stairDeath, gasDeath, claymoreDeath, truckDeath;

    public int death, killed;

    public Settings settings;
    public CameraScript cameraController;
    public string CurrentLevel;

    public RespawnManager respawn;

    private static bool doneDontDestroy = false;

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int loadedScene = SceneManager.GetActiveScene().buildIndex;
        App.Instance.CurrentLevel = scene.name;

        if (loadedScene > 2)
        {
            isPlaying = true;
            cameraController.OnLevelLoaded();
        }

    }

    private void Start()
    {
        if(!doneDontDestroy)
        {
            DontDestroyOnLoad(gameObject);
            doneDontDestroy = true;
        }

        if(cameraController == null)
            cameraController = FindObjectOfType<CameraScript>();

        if(respawn == null)
            respawn = FindObjectOfType<RespawnManager>();
    }





}
