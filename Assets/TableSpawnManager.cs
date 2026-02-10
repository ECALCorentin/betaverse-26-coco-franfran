using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;

public class TableSpawnManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;

    public void OnSceneLoaded()
    {
        SpawnObjectsOnTables();
    }

    void Start()
    {
        // On garde le délai de 2s pour laisser le MRUK charger les ancres
        Invoke("OnSceneLoaded", 2.0f);
    }

    private void SpawnObjectsOnTables()
    {
        if (MRUK.Instance == null) return;

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        if (room == null) return;

        List<MRUKAnchor> tables = new List<MRUKAnchor>();

        foreach (var anchor in room.Anchors)
        {
            // On vérifie si c'est une table
            if (anchor.Label.HasFlag(MRUKAnchor.SceneLabels.TABLE))
            {
                tables.Add(anchor);
            }
        }

        if (tables.Count == 0) return;

        for (int i = 0; i < tables.Count; i++)
        {
            MRUKAnchor currentTable = tables[i];
            
            // Position exacte de l'ancre (le centre de la surface haute)
            Vector3 spawnPos = currentTable.transform.position;
            
            // Choix du prefab
            GameObject prefabToSpawn = (i == 0) ? cubePrefab : cylinderPrefab;

            if (prefabToSpawn != null)
            {
                // Instanciation simple : le pivot du prefab fera le reste du travail
                GameObject spawnedObj = Instantiate(prefabToSpawn, spawnPos, currentTable.transform.rotation);
                
                // On lie l'objet à la table
                spawnedObj.transform.SetParent(currentTable.transform, true);
            }
        }
    }
}