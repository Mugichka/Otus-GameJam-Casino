using UnityEngine;

public class FireSpell : MonoBehaviour, IDamageBuffable
{
    private float damage = 10f;

    public void ApplyBuff(float buffAmount)
    {
        damage += buffAmount;
        Debug.Log("FireSpell damage increased to: " + damage);
    }

    public void ApplyDamageBuff(float buffAmount)
    {
        throw new System.NotImplementedException();
    }
}
