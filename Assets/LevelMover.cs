using UnityEngine;
using System.Collections;

public class LevelMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}

    void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
