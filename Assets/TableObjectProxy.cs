using UnityEngine;
using Meta.XR.MRUtilityKit; // Nécessaire pour accéder aux ancres
using System.Collections.Generic;

public class TableObjectProxy : MonoBehaviour
{
    public GameObject cubePrefab; // Ici ton Bateau
    public GameObject cylinderPrefab;

    public float detectionRadius = 0.5f;
    private static int _globalCubeCount = 0;
    private static List<Vector3> _occupiedPositions = new List<Vector3>();

    void Start()
    {
        // 1. Vérification de proximité (pour ne pas doubler sur une table)
        foreach (Vector3 pos in _occupiedPositions)
        {
            if (Vector3.Distance(transform.position, pos) < detectionRadius)
            {
                Destroy(gameObject);
                return;
            }
        }
        _occupiedPositions.Add(transform.position);

        // 2. Récupérer l'ancre la plus proche pour copier son orientation
        // Le FindSpawnPositions de Meta nous place sur une surface, 
        // mais nous voulons l'orientation précise de l'objet "Table".
        Quaternion finalRotation = transform.rotation;
        
        // On cherche l'ancre parente ou la plus proche
        MRUKAnchor anchor = GetComponentInParent<MRUKAnchor>();
        if (anchor != null)
        {
            // On récupère la rotation de la table pour que le bateau soit "droit"
            finalRotation = anchor.transform.rotation;
        }

        // 3. Choix du Prefab
        GameObject prefabToSpawn = (_globalCubeCount == 0) ? cubePrefab : cylinderPrefab;

        if (prefabToSpawn != null)
        {
            // 4. Spawn avec la rotation de la table au lieu de la rotation aléatoire de Meta
            GameObject visualInstance = Instantiate(prefabToSpawn, transform.position, finalRotation);
            visualInstance.transform.SetParent(this.transform);
            
            if (prefabToSpawn == cubePrefab) _globalCubeCount++;
        }
    }
}