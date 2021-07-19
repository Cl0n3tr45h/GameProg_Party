using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyBlock_StampfGraben : MonoBehaviour
{
    
    public Rigidbody[] DestroyedBlocks;
    public VisualEffect vfx;
    public float FlyPower;
    
    private void Awake()
    {
        vfx.pause = true;
        /*for (int i = 0; i < transform.childCount; i++)
        {
            DestroyedBlocks[i] = transform.GetChild(i).GetComponent<Rigidbody>();
        }*/
    }
    
    public IEnumerator DestroyEffect()
    {
        for (int i = 0; i < DestroyedBlocks.Length; i++)
        {
            //DestroyedBlocks[i] = transform.GetChild(i).GetComponent<Rigidbody>();
            DestroyedBlocks[i].isKinematic = false;
            if(i % 2 == 0)
                DestroyedBlocks[i].AddForce(-FlyPower, FlyPower, -FlyPower, ForceMode.Impulse);
            else
                DestroyedBlocks[i].AddForce(FlyPower, FlyPower, FlyPower, ForceMode.Impulse);
        }

        vfx.pause = false;
        vfx.Reinit();
        vfx.Play();
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
