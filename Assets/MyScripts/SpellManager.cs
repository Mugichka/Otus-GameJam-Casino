using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public GameObject player;

    public void UnlockSpell(MonoBehaviour spellScript)
    {
        spellScript.enabled = true;
        
        // Apply any active buffs to all spells, including newly enabled ones
        PlayerBuffs.Instance.ReapplyBuffsToAllSpells(player);
    }
}

