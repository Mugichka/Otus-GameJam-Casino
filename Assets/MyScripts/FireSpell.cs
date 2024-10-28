using UnityEngine;

public class FireSpell : MonoBehaviour, IBuffable
{
    private float damage = 10f;

    public void ApplyBuff(float buffAmount)
    {
        damage += buffAmount;
        Debug.Log("FireSpell damage increased to: " + damage);
    }
}
