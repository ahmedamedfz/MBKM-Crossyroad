using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject carPrefab1;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] float minSpawnDuration=1;
    [SerializeField] float maxSpawnDuration = 3;
    bool  isRight;
    float timer;
    private void Start()
    {
        isRight = Random.value>0.5f?true:false;
        timer = Random.Range(minSpawnDuration,maxSpawnDuration);
    }
    private void Update() 
    {
        if (timer>0)
        {
            timer-= Time.deltaTime;
            return;

        }
        timer = Random.Range(minSpawnDuration,maxSpawnDuration);

        var spawnPos = this.transform.position+Vector3.right*(isRight?-(terrain.Extent+1):terrain.Extent+1);
        GameObject[] MusuhCar = new GameObject[]{carPrefab,carPrefab1};
        int Pilih = Random.Range(0,2); 
        var go = Instantiate( original: MusuhCar[Pilih], position: spawnPos, rotation: Quaternion.Euler(0, isRight? 90:-90,0),parent: this.transform);

        var car = go.GetComponent<Car>();
        car.SetUp(terrain.Extent);
    }

}
