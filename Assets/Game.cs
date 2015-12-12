using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Game : MonoBehaviour {
    public static float leaning { get; private set; }

    public static float happiness { get; private set; }
    public static float cashFlow { get; private set; }

    public static float happinessAggregate;
    public static float cashFlowAggregate;
    
    public static float leaningAggregate;
    public Text leaningText;
	// Use this for initialization
	void Start () {
        leaning = 0.0f; //(Random.Range(0, 2) == 1 ? -0.05f : 0.05f)
        happiness = 60.0f;
        cashFlow = 60.0f;
    }

    // Update is called once per frame
    void Update() {
        float incr = Mathf.Log10(Mathf.Abs(leaningAggregate) + 1) * Time.deltaTime * 0.3f;
        if (leaningAggregate < 0.0f)
            incr *= -1.0f;
        leaning += incr;
        Debug.Log(leaning);
        if (Input.GetKeyDown(KeyCode.A)) {
            leaning -= 0.1f;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            leaning += 0.1f;
        }
        leaning = Mathf.Clamp(leaning, -1.0f, 1.0f);

        leaningAggregate = 0.0f;

        happiness = Mathf.Min(happiness + happinessAggregate, 100.0f);
        cashFlow = Mathf.Min(cashFlow + cashFlowAggregate, 100.0f);
        happinessAggregate = 0.0f;
        cashFlowAggregate = 0.0f;

        leaningText.text = leaning.ToString() + "\n" + happiness.ToString() + "\n" + cashFlow.ToString();
        if (happiness <= 0.0f || cashFlow <= 0.0f) Debug.Log("GAME OVER");
    }

    void LateUpdate()
    {
        
    }
}
