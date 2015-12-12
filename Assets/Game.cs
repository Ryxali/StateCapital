using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Game : MonoBehaviour {
    public static float leaning { get; private set; }

    public static float leaningAggregate;
    public Text leaningText;
	// Use this for initialization
	void Start () {
        leaning = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        float incr = Mathf.Log10(Mathf.Abs(leaningAggregate) + 1) * Time.deltaTime * 0.1f;
        if (leaningAggregate < 0.0f)
            incr *= -1.0f;
        leaning += incr;
        Debug.Log(leaning);
        if(Input.GetKeyDown(KeyCode.A)) {
            leaning -= 0.1f;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            leaning += 0.1f;
        }
        leaning = Mathf.Clamp(leaning, -1.0f, 1.0f);
        leaningText.text = leaning.ToString();
        leaningAggregate = 0.0f;
    }

    void LateUpdate()
    {
        
    }
}
