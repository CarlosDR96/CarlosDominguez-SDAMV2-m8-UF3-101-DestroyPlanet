using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bombParent;
    public float initialRespawnTime = 3.0f;
    private float respawnTime;
    private float timer = 0.0f;
    public GameObject prefabBomb;
    public float timeToDecreaseRespawn = 30.0f; // Tiempo después del cual disminuirá el tiempo de respawn
    public float decreaseAmount = 0.1f; // Cantidad de disminución del tiempo de respawn

    void Start()
    {
        respawnTime = initialRespawnTime;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Disminuir el tiempo de respawn después de cierto tiempo transcurrido
        if (timer > timeToDecreaseRespawn)
        {
            respawnTime -= decreaseAmount;
            timer = 0.0f;
        }

        if (timer > respawnTime)
        {
            timer = 0.0f;
            CreateNewBomb();
        }
    }

    private void CreateNewBomb()
    {
        Vector3 randPosition = Random.onUnitSphere * 0.5f;
        GameObject bomb = GameObject.Instantiate(prefabBomb, randPosition, Quaternion.identity, bombParent.transform);

        Vector3 toCenter = bombParent.transform.position - bomb.transform.position;
        Quaternion rotationToCenter = Quaternion.LookRotation(toCenter, Vector3.up);
        bomb.transform.rotation = rotationToCenter;
    }
}

