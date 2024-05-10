using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    PlayerController _playerController;
    GameObject enemyTarget;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (_playerController.isPunching())
        {
            if (target.gameObject.CompareTag("Hurtbox"))
            {
                enemyTarget = target.transform.parent.gameObject;
                _playerController.DealDamage(enemyTarget);
            }
        }
    }
}
