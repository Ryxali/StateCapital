using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    private static Dictionary<string, Mesh> batchedMeshes = new Dictionary<string, Mesh>();


    [ContextMenu("Batchy")]
    private void CreateBatches()
    {

        foreach (SkinnedMeshRenderer r in FindObjectsOfType<SkinnedMeshRenderer>())
        {
            r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            r.receiveShadows = false;
        }
        /*foreach (var o in FindObjectsOfType<CityBlockState>())
        {
            Animator[] renderers = o.GetComponentsInChildren<Animator>();
            foreach (var render in renderers)
            {
                //render.StartPlayback();
                //render.PlayInFixedTime("Show", 0, 1.0f);
                SkinnedMeshRenderer r;
                AnimationClip c = render.GetComponent<Animation>().GetClip("Show"); //render.GetNextAnimatorClipInfo(0)[0].clip;
                Debug.Log(c.ToString());
                c.SampleAnimation(render.gameObject, c.length);
                //render.StopPlayback();

            }
        }*/
    }

    private void Freeze()
    {
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            string key = renderers[i].sharedMesh.name;
            if (!batchedMeshes.ContainsKey(key))
            {
                Debug.Log(key);
                Mesh m = new Mesh();
                Vector3 lScale = renderers[i].GetComponentInParent<Animator>().transform.localScale;
                renderers[i].GetComponentInParent<Animator>().transform.localScale = Vector3.one;//new Vector3(lScale.x, 1.0f, lScale.z);
                renderers[i].BakeMesh(m);

                batchedMeshes.Add(key, m);
                renderers[i].GetComponentInParent<Animator>().transform.localScale = lScale;
            }
            GameObject staticMesh = new GameObject("StaticMeshInstance");
            staticMesh.transform.parent = renderers[i].transform.parent;
            staticMesh.transform.localPosition = renderers[i].transform.localPosition;
            staticMesh.transform.localRotation = renderers[i].transform.localRotation;
            staticMesh.transform.localScale = Vector3.one;//renderers[i].transform.localScale;
            staticMesh.AddComponent<MeshFilter>().sharedMesh = batchedMeshes[key];
            staticMesh.AddComponent<MeshRenderer>().sharedMaterial = renderers[i].sharedMaterial;
            staticMesh.GetComponent<MeshRenderer>().receiveShadows = false;
            staticMesh.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderers[i].enabled = false;
        }
        
    }

    private void UnFreeze()
    {
        foreach (var m in GetComponentsInChildren<MeshRenderer>())
        {
            m.enabled = false;
        }
        foreach (var m in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            m.enabled = true;
        }
    }
    
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
        float t = Time.time;
        var anims = GetComponentsInChildren<Animator>();
        foreach(Animator a in anims) {
            a.SetTrigger("Show");
            Vector3 scale = a.transform.localScale;
            scale.y *= 1 + Random.Range(0.0f, randomHeightMultiplierRange);
            a.transform.localScale = scale;
            
            PlayRandomPlop();
            yield return new WaitForSeconds(Random.Range(0.02f, 0.2f));
        }
        foreach (Animator a in anims)
        {
            bool b = true;
            b &= Mathf.Approximately(a.GetCurrentAnimatorStateInfo(0).normalizedTime, 1.0f);
            if (b)
            {
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        disabled = false;
        Freeze();
    }

    private IEnumerator FadeToNext(CityBlockState state)
    {
        UnFreeze();
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