using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(RectTransform))]
public class EndGamePanel : MonoBehaviour {
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text content;

    private string randomCityName
    {
        get
        {
            string[] arr = { "Nevilleville", "Hayman City", "Barkelona" };
            return arr[Random.Range(0, arr.Length)];
        }
    }

    private string newCommieNationName
    {
        get
        {
            string[] arr = { "Torvalia Union" };
            return arr[Random.Range(0, arr.Length)];
        }
    }

    private string coorporationName
    {
        get
        {
            string[] arr = { "Unicorp inc." };
            return arr[Random.Range(0, arr.Length)];
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetText(int population)
    {
        string s = content.text;
        s = s.Replace("%Pop%", population.ToString());
        s = s.Replace("%CityName%", randomCityName);
        s = s.Replace("%CommieCivName%", newCommieNationName);
        s = s.Replace("%CoorporationName%", coorporationName);
        content.text = s;
    }
}
