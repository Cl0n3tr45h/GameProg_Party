using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBlock_StampfGraben : MonoBehaviour
{ 
    //Hard blocks function differently from soft block in that you need to stomp on them three consecutive times
    public float Health = 6;
    public int FirstHealthBracket;
    public int SecondHealthBracket;
    private float airTime;
    private PlayerController playerController;
    private ParticleController particleController;
    public GameObject DestroyPrefab;
    private Material mat;
    private AudioSource audio;
    public Color firstLevelCrack;
    public Color secondLevelCrack;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
        particleController.StopEffect();
        float oldHealth = Health;
        Health -= playerController.StompPower;
        if(Input.GetKey(playerController.controls["stomp"]))
        {
            playerController.StompPower -= oldHealth;
            playerController.TempAirTime = airTime;
        }
        else
        {
            playerController.StompPower = 0;
            playerController.TempAirTime = airTime;
        }
        if (Health <= 0)
        {
            playerController.TempAirTime = 0;
            DestroyEffect();
            playerController.TempAirTime = airTime;
        }
        else
        {
            audio.Play();
            CrackStone();
        }

    }

    public void CrackStone()
    {
        if (Health <= FirstHealthBracket)
        {
            mat.color = firstLevelCrack;
        }

        if (Health <= SecondHealthBracket)
        {
            mat.color = secondLevelCrack;
        }
    }
    
    public void DestroyEffect()
    {
        //Particle effect of Rock exploding
        var destroyer = Instantiate(DestroyPrefab, transform.position, Quaternion.identity);
        destroyer.GetComponent<DestroyBlock_StampfGraben>().StartCoroutine(nameof(DestroyBlock_StampfGraben.DestroyEffect));
        
        Destroy(this.gameObject);
    }
}
