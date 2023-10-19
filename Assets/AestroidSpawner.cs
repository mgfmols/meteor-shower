using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Size
{
    large,
    small
}

public class AestroidSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> largeAestroids;
    [SerializeField] List<GameObject> normalAestroids;
    [SerializeField] Transform largeParent;
    [SerializeField] Transform normalParent;
    [SerializeField] Camera mainCamera;
    [SerializeField] ScoreHandler handler;

    private bool canSpawnAsteroids = false;

    public bool CanSpawnAsteroids { get { return canSpawnAsteroids; } set { canSpawnAsteroids = value; } }

    void Update()
    {
        if (CanSpawnAsteroids)
        {
            AestroidChecker();
        } 
    }

    void AestroidChecker()
    {
        float expected = 0.003f * handler.Score + 1;
        if (largeParent.childCount + normalParent.childCount < expected)
        {
            if (handler.Score < 300)
            {
                SpawnAestroid(Size.small, false, 0);
            }
            else if (handler.Score >= 300 && handler.Score < 500)
            {
                SpawnAestroid(Size.small, true, 1);
            }
            else if (handler.Score >= 500 && handler.Score < 1000)
            {
                int random = Random.Range(0, 1);

                if (random == 1)
                {
                    SpawnAestroid(Size.small, true, 2);
                }
                else
                {
                    SpawnAestroid(Size.large, false, 0);
                }   
            }
            else
            {
                int random = Random.Range(0, 1);

                if (random == 1)
                {
                    SpawnAestroid(Size.small, true, 3);
                }
                else
                {
                    SpawnAestroid(Size.large, true, 2);
                } 
            }
            
        }
    }

    void SpawnAestroid(Size size, bool force, int forcePower)
    {
        int RandomAestroid;
        Vector3 position = new Vector3(mainCamera.transform.position.x + 150, Random.Range(-75, 65), 0);
        
        GameObject newAestroid;
        if (size == Size.large)
        {
            RandomAestroid = Random.Range(0, largeAestroids.Count);
            newAestroid = Instantiate(largeAestroids[RandomAestroid], position, Quaternion.identity, largeParent);
        }
        else
        {
            RandomAestroid = Random.Range(0, normalAestroids.Count);
            newAestroid = Instantiate(normalAestroids[RandomAestroid], position, Quaternion.identity, normalParent);
        }

        if (force)
        {
            Rigidbody rb = newAestroid.GetComponent<Rigidbody>();
            switch (forcePower)
            {
                default:
                case 1:
                    rb.AddForce(Random.Range(-500f, -300f), Random.Range(-500f, 500f), 0);
                    break;
                case 2:
                    rb.AddForce(Random.Range(-1000f, -500f), Random.Range(-1000f, 1000f), 0);
                    break;
                case 3:
                    rb.AddForce(Random.Range(-1500f, -700f), Random.Range(-1500f, 1500f), 0);
                    break;
            }
        }
        
    }
}
