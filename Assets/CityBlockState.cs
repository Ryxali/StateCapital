using UnityEngine;
using System.Collections;

public class CityBlockState : MonoBehaviour {
    protected CityBlock parent;

    [SerializeField, Tooltip("more capitalist, values approaching 1.0")]
    private float rightCap;
    [SerializeField]
    private CityBlockState rightTransitionPrefab;

    [SerializeField, Tooltip("more communist, values approaching -1.0")]
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
        if(leftTransitionPrefab != null && parent.leaning <= leftCap)
        {
            Instantiate<CityBlockState>(leftTransitionPrefab);
            StartCoroutine(Fade());
        } else if(rightTransitionPrefab != null && rightCap <= parent.leaning)
        {
            Instantiate<CityBlockState>(rightTransitionPrefab);
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Appear()
    {
        foreach(Animator a in GetComponentsInChildren<Animator>()) {
            a.SetTrigger("Show");
            yield return new WaitForSeconds(Random.Range(0.02f, 0.2f));
        }
        yield return new WaitForSeconds(1.0f);
        disabled = false;
    }

    private IEnumerator Fade()
    {
        disabled = true;
        yield return null;
    }
}