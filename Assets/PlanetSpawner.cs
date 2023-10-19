using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetSpawner : MonoBehaviour
{
    [System.Serializable]public struct spawnObject
    {
        public GameObject gameObject;
        public Transform parent;
    }

    [SerializeField] Camera mainCamera;
    [SerializeField] List<spawnObject> PlanetsToBeSpawned;
    [SerializeField] List<Material> Materials;
    [SerializeField] int numberOfPlanets;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlanet(numberOfPlanets);
    }

    // Update is called once per frame
    void Update()
    {
        PlanetChecker();
    }

    void PlanetChecker()
    {
        if (PlanetsToBeSpawned[0].parent.childCount < numberOfPlanets)
        {
            SpawnPlanet(numberOfPlanets - PlanetsToBeSpawned[0].parent.childCount);
        }
    }

    void SpawnPlanet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 vPos = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
            int RandomPlanet = Random.Range(0, PlanetsToBeSpawned.Count);
            float scale = Random.Range(0.5f, 1.5f);
            float z = Random.Range(300f, 900f);
            float x = vPos.x + (1.214f * z + 275.02f);
            float yBottem = 1.14286f * z + 100.02f;
            float yTop = -1.43f * z - 159.9f;
            float y = Random.Range(yBottem, yTop);
            Vector3 position = new Vector3(x, y,z);
            GameObject newPlanet = Instantiate(PlanetsToBeSpawned[RandomPlanet].gameObject, position, Quaternion.identity, PlanetsToBeSpawned[RandomPlanet].parent);
            newPlanet.GetComponent<MeshRenderer>().material = Materials[Random.Range(0, Materials.Count)];
            newPlanet.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
