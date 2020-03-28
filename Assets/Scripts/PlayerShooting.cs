using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public ParticleSystem shell;
    public Transform bulletSpawnParent;

    public GameObject bullet;

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            PlayShootEffect();
            Instantiate(bullet, bulletSpawnParent.position, this.transform.localRotation);

            
        }
    }

    public void PlayShootEffect()
    {
        this.muzzleFlash.Play();
        this.shell.Play();
    }
}
