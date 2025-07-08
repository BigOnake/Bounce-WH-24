using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Attack_Network : NetworkBehaviour
{
    #region Fields
    public P_Hitbox_Network hitbox;
    public float attackDuration = 0.2f;
    private bool isAttacking = false;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        //Debug.Log("<color=green> Attack script is enabled. </color>");
        NetInputController.onPlayerAttack += Attack;
    }

    private void OnDisable()
    {
        //Debug.Log("<color=red> Attack script is disabled. </color>");
        NetInputController.onPlayerAttack -= Attack;
    }
    #endregion

    private void Attack()
    {
        Debug.Log("<color=orange> Attack </color>" + "button is pressed");
        PerformAttackRpc();
    }

    private IEnumerator Attacking()
    {
        isAttacking = true;
        hitbox.EnableHitbox();
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        hitbox.DisableHitbox();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void PerformAttackRpc()
    {
        StartCoroutine(Attacking());
    }

    #region Auxiliary Methods
    public bool GetAttackStatus()
    {
        return isAttacking;
    }
    #endregion
}
