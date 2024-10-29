using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Artifacts/Artifact")]
public class ArtifactSO : ScriptableObject
{
    public string artifactName;
    public string description;
    public float speedBuffAmount;
    public float damageBuffAmount;
    public float delayBuffAmount;
    public float duration;
    public List<string> artifactType;
    public List<string> targetSpellNames; // Leave empty to target all spells
    

    // Apply the buff to specific spells or all if targetSpellNames is empty
    public void ApplyBuffToSpells(GameObject player)
    {
        if (targetSpellNames == null || targetSpellNames.Count == 0)
        {
            if (artifactType.Contains("Speed"))
            {
                // Apply to all spells if no specific targets are set
                foreach (ISpeedBuffable spell in player.GetComponents<ISpeedBuffable>())
                {
                    spell.ApplySpeedBuff(speedBuffAmount);
                }
            }
            if (artifactType.Contains("Damage"))
            {
                // Apply to all spells if no specific targets are set
                foreach (IDamageBuffable spell in player.GetComponents<IDamageBuffable>())
                {
                    spell.ApplyDamageBuff(damageBuffAmount);
                }
            }
            if (artifactType.Contains("Delay"))
            {
                // Apply to all spells if no specific targets are set
                foreach (IDelayBuffable spell in player.GetComponents<IDelayBuffable>())
                {
                    spell.ApplyDelayBuff(delayBuffAmount);
                }
            }
        }
        else
        {
            // Apply to specific spells listed in targetSpellNames
            foreach (string spellName in targetSpellNames)
            {
                var spellScript = player.GetComponent(spellName) as MonoBehaviour;

                if(artifactType.Contains("Speed"))
                if (spellScript != null && spellScript is ISpeedBuffable speedBuffableSpell)
                {
                    speedBuffableSpell.ApplySpeedBuff(speedBuffAmount);
                }

                if(artifactType.Contains("Damage"))
                if (spellScript != null && spellScript is IDamageBuffable damageBuffableSpell)
                {
                    damageBuffableSpell.ApplyDamageBuff(damageBuffAmount);
                }

                if(artifactType.Contains("Delay"))
                if (spellScript != null && spellScript is IDelayBuffable delayBuffableSpell)
                {
                    delayBuffableSpell.ApplyDelayBuff(delayBuffAmount);
                }
            }
            
        }
    }
}
