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
        grid.Add(Instantiate<GameObject>(blockPrefab), 1, 0);
        grid.Add(Instantiate<GameObject>(blockPrefab), 2, 1);
        grid.Add(Instantiate<GameObject>(blockPrefab), 3, 3);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
