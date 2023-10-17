using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float timer;
    private Transform spawnPoint;
    
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void LoadLevelScene(string levelScene)
    {
        SceneManager.LoadScene(levelScene);
        spawnPoint = FindObjectOfType<PlayerController>().transform;
        
    }
}
