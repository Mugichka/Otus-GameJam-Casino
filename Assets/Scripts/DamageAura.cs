using UnityEngine;
using System.Collections;

public class DamageAura : MonoBehaviour
{
    [SerializeField] private Animator _smokeAnimator;
    [SerializeField] private Transform _smokeTransform;
    [SerializeField] private int _auraDamage;
    [SerializeField] private float _auraCheckInterval;

    private AuraUpgrade _currentUpgrade;
    private float _damageDelay = 0.3f;
    private Collider2D[] _hitColliders = new Collider2D[128];

    private void Start()
    {
        StartCoroutine(AuraDamageRoutine());
    }

    private IEnumerator AuraDamageRoutine()
    {
        while (true)
        {
            _smokeAnimator.SetTrigger("DoSmoke"); 
            yield return new WaitForSeconds(_damageDelay); 
            ApplyDamageToNearby();

            yield return new WaitForSeconds(_auraCheckInterval - _damageDelay);
        }
    }

    private void ApplyDamageToNearby()
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _currentUpgrade.Radius, _hitColliders);

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D collider = _hitColliders[i];

            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_auraDamage);
            }
        }
    }

    public void ApplyChipShootingUpgrade(AuraUpgrade newUpgrade)
    {
        _currentUpgrade = newUpgrade;
        _smokeTransform.localScale = newUpgrade.SmokeScale;
    }

    //private void OnDrawGizmos()
    //{
    //    // Для визуализации радиуса ауры в редакторе
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 2.1f);
    //}
}
