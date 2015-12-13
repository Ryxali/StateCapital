using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class CityBlockState : MonoBehaviour {
    protected CityBlock parent;
    public AudioClip[] plopSounds;
    [SerializeField, Tooltip("more capitalist, values approaching 1.0")]
    private float rightCap;
    [SerializeField]
    private CityBlockState rightTransitionPrefab;

    [Range(0.0f, 1.0f), SerializeField]
    private float randomHeightMultiplierRange = 0.1f;

    [SerializeField, Tooltip("more communist, values approaching -1.0")]
    private float leftCap;
    [SerializeField]
    private CityBlockState leftTransitionPrefab;
    [Range(-10.0f, 10.0f)]
    public float happinessGain = 0.5f;
    [Range(-10.0f, 10.0f)]
    public float cashFlowGain = 0.5f;

    private bool disabled = true;

    private bool canTransition = true;
    
    void Start()
    {
        parent = transform.parent.GetComponent<CityBlock>();
        StartCoroutine(Appear());
        transform.localRotation = Quaternion.Euler(0, 90 * Random.Range(0, 3), 0);
    }

    private void PlayRandomPlop()
    {
        if (plopSounds.Length != 0) // && Vector3.Distance(transform.position, Camera.main.transform.position) < 50.0f)
            GetComponent<AudioSource>().PlayOneShot(plopSounds[Random.Range(0, plopSounds.Length)]);
    }

    void StopSimulation()
    {
        StopAllCoroutines();
        enabled = false;
    }

    void LateUpdate()
    {

        if (disabled) return;
        Game.cashFlowAggregate += cashFlowGain * Time.deltaTime;
        Game.happinessAggregate += happinessGain * Time.deltaTime;
        if (!canTransition) return;
        if (leftTransitionPrefab != null && parent.leaning <= leftCap)
        {
            StartCoroutine(FadeToNext(leftTransitionPrefab));
        } else if(rightTransitionPrefab != null && rightCap <= parent.leaning)
        {
            StartCoroutine(FadeToNext(rightTransitionPrefab));
        }
        parent.leaning = Mathf.Clamp(parent.leaning, leftCap, rightCap);
    }

    private IEnumerator Appear()
    {
        foreach(Animator a in GetComponentsInChildren<Animator>()) {
            a.SetTrigger("Show");
            Vector3 scale = a.transform.localScale;
            scale.y *= 1 + Random.Range(0.0f, randomHeightMultiplierRange);
            a.transform.localScale = scale;
            
            PlayRandomPlop();
            yield return new WaitForSeconds(Random.Range(0.02f, 0.2f));
        }
        yield return new WaitForSeconds(1.0f);
        disabled = false;
    }

    private IEnumerator FadeToNext(CityBlockState state)
    {
        canTransition = false;
        foreach (Animator a in GetComponentsInChildren<Animator>())
        {
            a.SetTrigger("Hide");
            
            yield return new WaitForSeconds(Random.Range(0.02f, 0.2f));
        }
        yield return new WaitForSeconds(1.0f);
        var obj = Instantiate<CityBlockState>(state);
        obj.transform.parent = parent.transform;
        obj.transform.localPosition = Vector3.down * 0.05f;
        disabled = true;
        Destroy(gameObject);
    }
}