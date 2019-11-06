using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem Ground;
    public ParticleSystem Sky;
    public float offSet;

    protected void OnTriggerEnter(Collider c)
    {
        Debug.Log("c:" + c.tag);
        if (c.gameObject.CompareTag("Pickups"))
        {
            if(this.GetComponent<Player>().IsGrounded)
            {
                Ground.transform.position = this.transform.position + new Vector3(0,0,offSet);
                Ground.enableEmission = true;
            }
            else
            {
                Sky.transform.position = this.transform.position + new Vector3(0, 0, offSet);
                Sky.enableEmission = true;
            }
            StartCoroutine(stopParticles());
        }
    }

    IEnumerator stopParticles()
    {
        yield return new WaitForSecondsRealtime(.3f);
        Ground.enableEmission = false;
        Sky.enableEmission = false;
    }
}
