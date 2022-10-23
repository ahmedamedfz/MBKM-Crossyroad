using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject road;
    [SerializeField] GameObject river;
    [SerializeField] int extent ;
    [SerializeField] int frontDistance= 10;
    [SerializeField] int backDistance=-5;
    [SerializeField] int maxSameTerrain;
    [SerializeField] AudioSource suarabomb;
    
    Dictionary <int, TerrainBlock> map = new Dictionary <int, TerrainBlock>(50);

    TMP_Text gameOverText;
   
    private void Start() 
    {   
        
        
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();

        for (int z= backDistance; z<=0;z++)
        {
            CreateTerrain(grass, z);
        }

        for(int z =1; z < frontDistance; z++)
        {
            var prefab = GetNextRandomTerrainPrefab(z);
            CreateTerrain(prefab,z);
        }
        player.SetUp(backDistance,extent);
    }
    private int playerLastMaxTravel;
   private void Update()
     {
        if(player.IsDie && gameOverPanel.activeInHierarchy == false)
        { 
            StartCoroutine(showGameOverPanel());
        }

        if( player.MaxTravel == playerLastMaxTravel)
           return;
            
        playerLastMaxTravel = player.MaxTravel;

        var randTbPrefab = GetNextRandomTerrainPrefab(player.MaxTravel+frontDistance-1);
        CreateTerrain(randTbPrefab,player.MaxTravel+frontDistance-1); 

        var lastTB = map[player.MaxTravel-1+ backDistance];
        map.Remove(player.MaxTravel-1+backDistance);
        Destroy(lastTB.gameObject);
        player.SetUp(player.MaxTravel + backDistance, extent);

    }
    IEnumerator showGameOverPanel()
    {
        yield return new WaitForSeconds(5);
        suarabomb.Play();

        player.enabled = false;
        
        gameOverText.text = "" + player.MaxTravel;
        gameOverPanel.SetActive(true);
        
    }
    private void CreateTerrain(GameObject prefab, int zPos)
    {
            var go = Instantiate(prefab,new Vector3 (0,0,zPos), Quaternion.identity);
            var tb = go.GetComponent<TerrainBlock>();
            tb.Build(extent);
            map.Add(zPos,tb);            
    }
    private GameObject GetNextRandomTerrainPrefab(int nextPos)
    {
        bool isUniform = true;
        var tbRefRef = map [nextPos-3];
        var tbRef = map[nextPos-1];
        GameObject[] terrainType = new GameObject[]{grass,road,river};
         
        
        for (int distance = 2; distance <= maxSameTerrain; distance++)
        {
            if(map[nextPos - distance].GetType()!= tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }
        if (isUniform)
        {
            if (tbRef is Grass)
                {
                    if (tbRefRef is Road)
                        return river;
                    else
                        return road;
                }
            else if (tbRef is Road)
                {
                    if (tbRefRef is Grass)
                        return river;
                    else
                        return grass;
                }
            else if (tbRef is River)
                {
                    if (tbRefRef is Road)
                        return grass;
                    else
                        return road;
                }
        }

        
         int choice = Random.Range (0,3);

         return terrainType[choice];
           
    }
}
