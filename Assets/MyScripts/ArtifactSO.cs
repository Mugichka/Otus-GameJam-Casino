using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts/Artifact")]
public class ArtifactSO : ScriptableObject
{
    public string artifactName;
    public string description;
    public float buffAmount;
    public float duration;
    public List<string> targetSpellNames; // Leave empty to target all spells

    // Apply the buff to specific spells or all if targetSpellNames is empty
    public void ApplyBuffToSpells(GameObject player)
    {
        if (targetSpellNames == null || targetSpellNames.Count == 0)
        {
            // Apply to all spells if no specific targets are set
            foreach (IBuffable spell in player.GetComponents<IBuffable>())
            {
                spell.ApplyBuff(buffAmount);
            }
        }
        else
        {
            // Apply to specific spells listed in targetSpellNames
            foreach (string spellName in targetSpellNames)
            {
                var spellScript = player.GetComponent(spellName) as MonoBehaviour;
                if (spellScript != null && spellScript is IBuffable buffableSpell)
                {
                    buffableSpell.ApplyBuff(buffAmount);
                }
            }
        }
    }
}
