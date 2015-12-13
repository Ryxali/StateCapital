using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour {
    public static float leaning { get; private set; }

    public static float happiness { get; private set; }
    public static float cashFlow { get; private set; }

    public static float happinessAggregate;
    public static float cashFlowAggregate;
    
    public static float leaningAggregate;
    public Text leaningText;

    public ProgressBar leaningBar_Commie;
    public ProgressBar leaningBar_Cappie;
    public ProgressBar cashFlowBar;
    public ProgressBar happinessBar;

    public EndGamePanel commieLosePanel;
    public EndGamePanel cappieLosePanel;

    private bool shouldUpdate = true;
	// Use this for initialization
	void Start () {
        InitVars();
    }

    private void InitVars()
    {
        leaning = 0.0f; //(Random.Range(0, 2) == 1 ? -0.05f : 0.05f)
        happiness = 60.0f;
        cashFlow = 60.0f;
        leaningAggregate = 0.0f;
        happinessAggregate = 0.0f;
        cashFlowAggregate = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        leaningBar_Commie.sliderVal = 1 - (leaning + 1) / 2;
        leaningBar_Cappie.sliderVal = (leaning + 1) / 2;
        if (!shouldUpdate) return;
        

        float incr = Mathf.Log10(Mathf.Abs(leaningAggregate) + 1) * Time.deltaTime * 0.3f;
        if (leaningAggregate < 0.0f)
            incr *= -1.0f;
        leaning += incr;
        Debug.Log(leaning);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            leaning -= 0.1f;
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
        
        happinessBar.sliderVal = happiness / 100.0f;
        cashFlowBar.sliderVal = cashFlow / 100.0f;

        if(cashFlow <= 0.0f)
        {
            //BroadcastMessage("StopSimulation");
            commieLosePanel.gameObject.SetActive(true);
            
            commieLosePanel.SetText(FindObjectOfType<BlockGrid>().count * 157);
            leaning = -1.0f;
            shouldUpdate = false;

        } else if(happiness <= 0.0f) {
            //BroadcastMessage("StopSimulation");
            cappieLosePanel.gameObject.SetActive(true);
            cappieLosePanel.SetText(FindObjectOfType<BlockGrid>().count * 127);
            leaning = 1.0f;
            shouldUpdate = false;
        }
    }

    void LateUpdate()
    {
        
    }

    void Restart()
    {
        InitVars();
        SceneManager.LoadScene("main");
        
    }
}
