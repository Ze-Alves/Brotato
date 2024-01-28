using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCDestinationPicker : MonoBehaviour
{
    public static NPCDestinationPicker Instance;

    [SerializeField] private Transform _possibleZones;
    [SerializeField] private float _distanceFromZoneOrigin = 5f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("NPCDestinationPicker already exists!");
            Destroy(gameObject);
        }
    }

    public Vector3 GetRandomDestination(Transform requester)
    {
        int randomZoneIndex = Random.Range(0, _possibleZones.childCount);
        Transform randomZone = _possibleZones.GetChild(randomZoneIndex);
        Vector3 randomOffset = new Vector3(Random.Range(-_distanceFromZoneOrigin, _distanceFromZoneOrigin), 0, Random.Range(-_distanceFromZoneOrigin, _distanceFromZoneOrigin));

        Vector3 randomDestination = randomZone.position + randomOffset;
        return randomDestination;
    }

    private void OnDrawGizmos()
    {
        if (_possibleZones == null) return;

        Gizmos.color = new Color(1,0,0,.5f);
        foreach (Transform zone in _possibleZones)
        {
            Gizmos.DrawSphere(zone.position, _distanceFromZoneOrigin);
        }
    }
}
