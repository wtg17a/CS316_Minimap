using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Manager : MonoBehaviour
{
    [Header("Inscribed")]
    public List<TrackTagInfo> tagsToTrack;

    private List<Minimap_Blip> activeBlips = new List<Minimap_Blip>();

	// Use this for initialization
	void Start ()
    {
        AssignBlips();
	}

    void AssignBlips()
    {
        if(activeBlips != null && activeBlips.Count > 0)
        {
            DestroyActiveBlips();
        }

        GameObject blipGO;
        Minimap_Blip blip;

        foreach(TrackTagInfo tC in tagsToTrack)
        {
            GameObject[] tGOs = GameObject.FindGameObjectsWithTag(tC.tag);

            if(tGOs.Length == 0)
            {
                Debug.LogWarning("Minimap_Manager.AssignBlips() - No GameObjects were found with tag \""+tC.tag+"\"");
                continue;
            }

            foreach(GameObject tGO in tGOs)
            {
                blipGO = Instantiate<GameObject>(tC.prefab);
                blip = blipGO.GetComponent<Minimap_Blip>();
                blip.transform.SetParent(transform.parent);
                blip.color = tC.color;
                blip.trackedTransform = tGO.transform;
            }
        }
    }

    private void DestroyActiveBlips()
    {
        Minimap_Blip blip;
        while (activeBlips.Count > 0)
        {
            blip = activeBlips[activeBlips.Count - 1];
            activeBlips.Remove(blip);
            Destroy(blip.gameObject);
        }
    }
}

[System.Serializable]
public struct TrackTagInfo
{
    public GameObject prefab;
    public Color color;
    public string tag;
}