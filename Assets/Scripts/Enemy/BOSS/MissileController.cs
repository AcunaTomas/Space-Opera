using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    public Transform _missilePoint;
    public GameObject _missileHurtBox;
    public LayerMask playerLayers;
    public Animator _animator;
    public float bombRange = 1f;
    public float bombDuration = 0.5f;
    
    
    private bool isBombActive = false;
    private float queryStartTime;
    int _attackDamage = 1;
    bool _coolingCollision = false;


    private void FixedUpdate()
    {
        if (isBombActive)
        {
            if (Time.time - queryStartTime >= bombDuration)
            {
                isBombActive = false;
            }
            else
            {
                Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_missilePoint.position, bombRange, playerLayers);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                foreach (Collider2D enemyCollider in hitPlayer)
                {
                    enemyCollider.GetComponent<Player>().LoseHP(_attackDamage);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_coolingCollision)
        {
            isBombActive = true;
            queryStartTime = Time.time;

            _animator.SetTrigger("Explode");
            Debug.Log(_animator);
            Destroy(gameObject, 0.7f);

            _coolingCollision = true;
            StartCoroutine(StartCooldown());

            _missileHurtBox.SetActive(false);
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.7f);

        _coolingCollision = false;
    }

    void OnDrawGizmosSelected()
    {
        if (_missilePoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(_missilePoint.position, bombRange);
    }
}
