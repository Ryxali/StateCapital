using UnityEngine;
using System.Collections;

public abstract class CityBlockState : MonoBehaviour {
    protected CityBlock parent;

    [SerializeField]
    private float rightCap;
    [SerializeField]
    private CityBlockState rightTransitionPrefab;

    [SerializeField]
    private float leftCap;
    [SerializeField]
    private CityBlockState leftTransitionPrefab;

    private bool disabled = true;
    void Start()
    {
        parent = transform.parent.GetComponent<CityBlock>();
        StartCoroutine(Appear());
    }

    void LateUpdate()
    {
        if (disabled) return;
        if(parent.leaning <= leftCap)
        {
            Instantiate<CityBlockState>(leftTransitionPrefab);
            StartCoroutine(Fade());
        } else if(rightCap <= parent.leaning)
        {
            Instantiate<CityBlockState>(rightTransitionPrefab);
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Appear()
    {
        yield return null;
        disabled = false;
    }

    private IEnumerator Fade()
    {
        disabled = true;
        yield return null;
    }
}