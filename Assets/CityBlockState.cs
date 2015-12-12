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
        transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 3), 0);
    }

    void LateUpdate()
    {
        if (disabled) return;
        if(leftTransitionPrefab != null && parent.leaning <= leftCap)
        {
            StartCoroutine(FadeToNext(leftTransitionPrefab));
        } else if(rightTransitionPrefab != null && rightCap <= parent.leaning)
        {
            Debug.Log("FADY");
            StartCoroutine(FadeToNext(rightTransitionPrefab));
        }
        parent.leaning = Mathf.Clamp(parent.leaning, leftCap, rightCap);
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

    private IEnumerator FadeToNext(CityBlockState state)
    {
        disabled = true;
        foreach (Animator a in GetComponentsInChildren<Animator>())
        {
            a.SetTrigger("Hide");
            yield return new WaitForSeconds(Random.Range(0.02f, 0.2f));
        }
        yield return new WaitForSeconds(1.0f);
        var obj = Instantiate<CityBlockState>(state);
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = Vector3.zero;
        Destroy(gameObject);
    }
}