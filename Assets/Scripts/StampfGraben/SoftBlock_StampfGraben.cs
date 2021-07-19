using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlock_StampfGraben : MonoBehaviour
{
    public float Health;
    private PlayerController playerController;
    private float airTime;
    private ParticleController particleController;
    public GameObject DestroyPrefab;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerController == null)
            {
                playerController = other.gameObject.GetComponent<PlayerController>();
                particleController = other.gameObject.GetComponent<ParticleController>();
                airTime = playerController.StopInAirTime;
            }

            checkDestruction();
        }
    }

    public void checkDestruction()
    {
        float oldHealth = Health;
        Health -= playerController.StompPower;
        particleController.StopEffect();
        if(Input.GetKey(playerController.controls["stomp"]))
        {
            playerController.StompPower -= oldHealth;
            playerController.TempAirTime = 0;
        }
        else
        {
            playerController.StompPower = 0;
            playerController.TempAirTime = airTime;
        }
        
        if (Health <= 0)
            DestroyEffect();
    }

    public void DestroyEffect()
    {
        //Particle effect of Rock exploding
        var destroyer = Instantiate(DestroyPrefab, transform.position, Quaternion.identity);
        destroyer.GetComponent<DestroyBlock_StampfGraben>().StartCoroutine(nameof(DestroyBlock_StampfGraben.DestroyEffect));
        
        Destroy(this.gameObject);
    }
}
