using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
public class Panner : MonoBehaviour {
    public BlockGrid blockGrid;
    private DepthOfField dof;
	// Use this for initialization
	void Start () {
        transform.position = transform.position.normalized * (8 + Mathf.Sqrt(blockGrid.count) * (Mathf.PI + 1));


    }
	
	// Update is called once per frame
	void Update () {
        float target = (8 + Mathf.Sqrt(blockGrid.count) * (Mathf.PI + 1));
        float start = transform.position.magnitude;
        if(!Mathf.Approximately(target, start))
            transform.position = transform.position.normalized * Mathf.MoveTowards(start, target, Time.deltaTime * (target - start));
        //dof.focalLength = transform.position.magnitude;

    }
}
