using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float timeToExplosion = 4.0f;
    private float timer = 0.0f;
    private GameManager gm = null;
    public GameObject prefabExplosion;
    public ParticleSystem bombParticleSystem;
    public AudioSource activeSound;
    public AudioSource explosionSound;
    
    void Start()
    {
        GameObject o = GameObject.FindGameObjectWithTag("GameManager");
        // Reproduce el sonido activo al inicio
        activeSound.Play();
        
        if (o == null)
        {
            Debug.LogError("There's no gameObject with GameManager tag.");
        }
        else
        {
            gm = o.GetComponent<GameManager>();
            if (gm == null)
            {
                Debug.LogError("The GameObject with GameManager tag doesn't have the GameManager script attached to it");
            }
        }
        
        GetComponent<MeshRenderer>().material.color = Color.green;
        // Obtén el sistema de partículas de la bomba
        bombParticleSystem = GetComponentInChildren<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Si han passat 4 segons --> Destroy this gameObject & damage using GameManeger:
        timer += Time.deltaTime;
        if (timer > timeToExplosion)
        {
            timer = 0.0f;
            gm.TakeDamage();
            GameObject explosion = GameObject.Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            explosionSound.Play();
            StartCoroutine(DestroyAfterSound(explosion));
        }

        Color newColor = Color.Lerp(Color.green, Color.red, timer / timeToExplosion);
        GetComponent<MeshRenderer>().material.color = newColor;
        

       
        bombParticleSystem.Play();

        

    }
    
    //Si l'usuari fa click sobre la bomba=>
    // Destroy
    private void OnMouseDown()
    {
        gm.AddScore();
        Destroy(gameObject);
    }
    
    private IEnumerator DestroyAfterSound(GameObject explosion)
    {
        yield return new WaitForSeconds(explosionSound.clip.length);
        Destroy(explosion);
        Destroy(gameObject);
    }
    
   
    
}
