using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BlockGrid : MonoBehaviour {
    private struct Coord
    {
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x, y;
    }
    private Dictionary<Coord, GameObject> container = new Dictionary<Coord, GameObject>();

    [SerializeField]
    private Mesh mesh;
   

    public GameObject this[int x, int y] {
        get {

            return container[new Coord(x, y)];
        }
        set {
            if(container.ContainsKey(new Coord(x, y)))
            {
                Destroy(container[new Coord(x, y)].gameObject);
            }
            float oY = value.transform.localPosition.y;
            value.transform.parent = transform;
            value.transform.localPosition = new Vector3(x * 7, oY, y * 7);
            container[new Coord(x, y)] = value;
        }
    }


    void Start()
    {
        GenerateMesh();
        
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        mesh.vertices = new Vector3[]{ Vector3.one, Vector3.zero, Vector3.down};
        mesh.triangles = new int[] { 0, 1, 2 };
        GetComponent<MeshFilter>().mesh = mesh;
    }

    /// <summary>
    /// Can we place/replace on the given block?
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool DoesBlockExist(int x, int y)
    {
        Coord c = new Coord(x, y);
        for(int ix = -1; ix <= 1; ix++)
        {
            for(int iy = -1; iy <= 1; iy++)
            {
                c.x = x + ix;
                c.y = y + iy;
                if (container.ContainsKey(c)) return true;
            }
        }

        return false;
    }



    public void Add(GameObject block, int x, int y)
    {
        Debug.Assert(DoesBlockExist(x, y), "Trying to populate non-existant street block!");
        this[x, y] = block;
    }



    private bool HasAdjacentEmptyBlock(Coord coord, out Coord outCoord)
    {
        int x = coord.x;
        int y = coord.y;
        Coord[] coords = new Coord[]
        {
            new Coord(x-1, y),
            new Coord(x-1, y-1),
            new Coord(x-1, y+1),
            new Coord(x+1, y-1),
            new Coord(x+1, y),
            new Coord(x+1, y+1),
            new Coord(x, y+1),
            new Coord(x, y-1)
        };
        for(int i = 0; i < Random.Range(1, 16); i++)
        {
            int r0 = Random.Range(0, coords.Length);
            int r1 = Random.Range(0, coords.Length);
            Coord tC = coords[r0];
            coords[r0] = coords[r1];
            coords[r1] = tC;
        }
        foreach(Coord c in coords)
        {
            outCoord.x = c.x;
            outCoord.y = c.y;
            if (!container.ContainsKey(c)) return true;
        }

        outCoord = new Coord();
        return false;
    }

    public void AddToRandomOnoccupiedBlock(GameObject block)
    {
        //int n = Random.Range(0, container.Count);
        var e = container.GetEnumerator();
        Coord[] keys = new Coord[container.Keys.Count];
        container.Keys.CopyTo(keys, 0);
        for (int i = 0; i < Random.Range(1, keys.Length); i++)
        {
            int r0 = Random.Range(0, keys.Length);
            int r1 = Random.Range(0, keys.Length);
            Coord tC = keys[r0];
            keys[r0] = keys[r1];
            keys[r1] = tC;
        }
        /*for( int i = 0; i < n; n++)
        {
            e.MoveNext();
        }*/
        Coord outCoord;
        int j = 0;
        for (; j < keys.Length && !HasAdjacentEmptyBlock(keys[j], out outCoord); j++)
        {

        }
        Debug.Log("Added");
        this[outCoord.x, outCoord.y] = block;
    }


}
