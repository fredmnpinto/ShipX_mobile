using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool IsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        /* Disable the INGAME button */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnpauseGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        
        /* Turn this Screen invisible */
    }
}
