using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CityBlock : MonoBehaviour {
    [SerializeField]
    private Mesh mesh;
	// Use this for initialization
	void Start () {
        RefreshMesh();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RefreshMesh()
    {
        mesh = new Mesh();
        Vector3[] vertices = new Vector3[32];
        int[] indices = new int[3];
        #region SetupVertices
        // +x -z = topleft
        // Starting from top left of outside

        vertices[0] = new Vector3(4, 0, -10);
        vertices[1] = new Vector3(4, 0, -4);
        vertices[2] = new Vector3(10, 0, -4);

        vertices[3] = new Vector3(10, 0, -3);

        vertices[4] = new Vector3(4, 0, -3);
        vertices[5] = new Vector3(4, 0, 3);
        vertices[6] = new Vector3(10, 0, 3);

        vertices[7] = new Vector3(10, 0, 4);

        vertices[8] = new Vector3(4, 0, 4);
        vertices[9] = new Vector3(4, 0, 10);

        vertices[10] = new Vector3(3, 0, 10);

        vertices[11] = new Vector3(3, 0, 4);
        vertices[12] = new Vector3(-3, 0, 4);
        vertices[13] = new Vector3(-3, 0, 10);
        vertices[14] = new Vector3(-4, 0, 10);
        vertices[15] = new Vector3(-4, 0, 4);
        vertices[16] = new Vector3(-10, 0, 4);
        vertices[17] = new Vector3(-10, 0, 3);
        vertices[18] = new Vector3(-4, 0, 3);
        vertices[19] = new Vector3(-4, 0, -3);
        vertices[20] = new Vector3(-10, 0, -3);
        vertices[21] = new Vector3(-10, 0, -4);
        vertices[22] = new Vector3(-4, 0, -4);
        vertices[23] = new Vector3(-4, 0, -10);
        vertices[24] = new Vector3(-3, 0, -10);
        vertices[25] = new Vector3(-3, 0, -4);
        vertices[26] = new Vector3(3, 0, -4);
        vertices[27] = new Vector3(3, 0, -10);

        //Lastly add inner square starting from top-left
        vertices[28] = new Vector3(3, 0, -3);
        vertices[29] = new Vector3(3, 0, 3);
        vertices[30] = new Vector3(-3, 0, 3);
        vertices[31] = new Vector3(-3, 0, -3);
        #endregion

        #region SetupIndicies
        List<int> dynInd = new List<int>();
        // Up leftmost outcrop
        dynInd.Add(0);
        dynInd.Add(27);
        dynInd.Add(1);

        dynInd.Add(1);
        dynInd.Add(27);
        dynInd.Add(26);

        // Up leftmost intersection
        dynInd.Add(1);
        dynInd.Add(26);
        dynInd.Add(28);

        dynInd.Add(1);
        dynInd.Add(28);
        dynInd.Add(4);

        // Upmost left outcrop
        dynInd.Add(1);
        dynInd.Add(3);
        dynInd.Add(2);

        dynInd.Add(1);
        dynInd.Add(4);
        dynInd.Add(3);

        // Up street
        dynInd.Add(4);
        dynInd.Add(28);
        dynInd.Add(5);

        dynInd.Add(5);
        dynInd.Add(28);
        dynInd.Add(29);

        // up right intersection
        dynInd.Add(5);
        dynInd.Add(29);
        dynInd.Add(11);

        dynInd.Add(5);
        dynInd.Add(11);
        dynInd.Add(8);

        // upmost right outcropping
        dynInd.Add(5);
        dynInd.Add(7);
        dynInd.Add(6);

        dynInd.Add(5);
        dynInd.Add(8);
        dynInd.Add(7);

        // up rightmost outcropping
        dynInd.Add(8);
        dynInd.Add(10);
        dynInd.Add(9);

        dynInd.Add(8);
        dynInd.Add(11);
        dynInd.Add(10);

        // right street
        dynInd.Add(11);
        dynInd.Add(30);
        dynInd.Add(12);

        dynInd.Add(11);
        dynInd.Add(29);
        dynInd.Add(30);

        // down right intersection
        dynInd.Add(12);
        dynInd.Add(30);
        dynInd.Add(18);

        dynInd.Add(12);
        dynInd.Add(18);
        dynInd.Add(15);

        // down rightmost outcrop
        dynInd.Add(12);
        dynInd.Add(14);
        dynInd.Add(13);

        dynInd.Add(12);
        dynInd.Add(15);
        dynInd.Add(14);

        // downmost right outcrop
        dynInd.Add(15);
        dynInd.Add(17);
        dynInd.Add(16);

        dynInd.Add(15);
        dynInd.Add(18);
        dynInd.Add(17);

        // down street
        dynInd.Add(18);
        dynInd.Add(30);
        dynInd.Add(31);

        dynInd.Add(18);
        dynInd.Add(31);
        dynInd.Add(19);

        // down left intersection
        dynInd.Add(19);
        dynInd.Add(31);
        dynInd.Add(25);

        dynInd.Add(19);
        dynInd.Add(25);
        dynInd.Add(22);

        // downmost left outcrop
        dynInd.Add(19);
        dynInd.Add(21);
        dynInd.Add(20);

        dynInd.Add(19);
        dynInd.Add(22);
        dynInd.Add(21);

        // down leftmost outcrop
        dynInd.Add(22);
        dynInd.Add(25);
        dynInd.Add(24);

        dynInd.Add(22);
        dynInd.Add(24);
        dynInd.Add(23);

        // left street
        dynInd.Add(25);
        dynInd.Add(31);
        dynInd.Add(28);

        dynInd.Add(25);
        dynInd.Add(28);
        dynInd.Add(26);

        indices = dynInd.ToArray();
        #endregion

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.Optimize();
        GetComponent<MeshFilter>().mesh = mesh;
        // vertices -- Generate all?
        // indices -- indices define mesh chunks used
        // normals
        // uv
    }
}
