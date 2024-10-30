using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffs : MonoBehaviour
{
    public static PlayerBuffs Instance;

    [SerializeField]private List<ArtifactSO> activeBuffs = new List<ArtifactSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnlyAddBuff(ArtifactSO artifact)
    {
        activeBuffs.Add(artifact);
    }

    public void AddBuff(ArtifactSO artifact, GameObject player)
    {
        activeBuffs.Add(artifact);
        artifact.ApplyBuffToSpells(player); // Apply buff to all eligible spells immediately

        if (artifact.duration > 0)
            StartCoroutine(RemoveBuffAfterDuration(artifact, artifact.duration));
    }

    private IEnumerator RemoveBuffAfterDuration(ArtifactSO artifact, float duration)
    {
        yield return new WaitForSeconds(duration);
        activeBuffs.Remove(artifact);
    }

    public void ReapplyBuffsToAllSpells(GameObject player)
    {
        foreach (var artifact in activeBuffs)
        {
            artifact.ApplyBuffToSpells(player);
            Debug.Log("Reapplied buff: " + artifact.name);
        }
    }
}
