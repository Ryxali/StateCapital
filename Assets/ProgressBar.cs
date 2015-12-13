using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour {
    [SerializeField, Range(0.0f, 1.0f)]
    private float _sliderVal = 0.5f;
    [SerializeField]
    private UnityEngine.UI.Image bar;
    [SerializeField]
    private bool horizontal = true;


    private float targetSliderVal;

    public float sliderVal {
        get { return targetSliderVal; }
        set
        {
            targetSliderVal = Mathf.Clamp01(value);
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
    
	void Update () {
        if (bar != null)
        {
            _sliderVal = Mathf.MoveTowards(_sliderVal, targetSliderVal, Time.deltaTime * Mathf.Abs(targetSliderVal - _sliderVal) * 10.0f);
            RectTransform bt = bar.GetComponent<RectTransform>();
            Vector2 r = bt.sizeDelta;
            RectTransform tt = GetComponent<RectTransform>();
            if(horizontal)
                r.x = tt.sizeDelta.x * _sliderVal;
            else
                r.y = tt.sizeDelta.y * _sliderVal;
            bt.sizeDelta = r;
        }
	}
}
