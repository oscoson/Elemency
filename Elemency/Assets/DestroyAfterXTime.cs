using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXTime : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyParticles(destroyTime));
    }

    IEnumerator DestroyParticles(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Destroy(gameObject);
    }

}
