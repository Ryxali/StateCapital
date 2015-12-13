using UnityEngine;
using System.Collections;

public class CityGrower : MonoBehaviour {
	[SerializeField]
	private GameObject blockPrefab;

    private BlockGrid grid;
	// Use this for initialization
	void Start () {
        grid = GetComponentInChildren<BlockGrid>();
        grid[0, 0] = Instantiate<GameObject>(blockPrefab);
        StartCoroutine(Builder());
        //grid.Add(Instantiate<GameObject>(blockPrefab), 1, 0);
        //grid.Add(Instantiate<GameObject>(blockPrefab), 2, 1);
        //grid.Add(Instantiate<GameObject>(blockPrefab), 3, 3);
    }
    void StopSimulation()
    {
        StopAllCoroutines();
    }
    IEnumerator Builder()
    {
        float t = 1.0f;
        for (int i = 0; i < 500; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.8f, 4.0f) * t);
            grid.AddToRandomOnoccupiedBlock(Instantiate<GameObject>(blockPrefab));
            
            t *= 0.995f;
        }
        
    }

    // Update is called once per frame
    void Update () {
	
	}
}
