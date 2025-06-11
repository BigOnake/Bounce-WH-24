using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetAttack : NetworkBehaviour
{
    #region Fields
    public GameObject attackHitbox;
    private BoxCollider hitbox;
    private MeshRenderer hitboxRenderer;
    private Transform cameraTransform;
    private Vector3 cameraDirection;
    private float transperent = 0f;
    private float visible = 0.2f;
    public float attackDuration;
    private bool isAttacking = false;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        Debug.Log("<color=green> Attack script is enabled. </color>");
        NetInputController.onPlayerAttack += Attack;
    }

    private void OnDisable()
    {
        Debug.Log("<color=red> Attack script is disabled. </color>");
        NetInputController.onPlayerAttack -= Attack;
    }

    private void Awake()
    {
        SetComponents();
        SetTransperency(transperent);
    }

    private void Update()
    {
        PositionHitbox();
    }
    #endregion

    private void Attack()
    {
        Debug.Log("<color=orange> Attack </color>" + "button is pressed");
        PerformAttackRPC();
    }

    private void PositionHitbox()
    {
        cameraDirection = cameraTransform.forward;
        attackHitbox.transform.rotation = Quaternion.LookRotation(cameraDirection);
        attackHitbox.transform.position = cameraTransform.position + cameraDirection;
    }

    private IEnumerator Attacking()
    {
        isAttacking = true;
        SetTransperency(visible);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        SetTransperency(transperent);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void PerformAttackRPC()
    {
        StartCoroutine(Attacking());
    }

    #region Auxiliary Methods
    public bool GetAttackStatus()
    {
        return isAttacking;
    }

    private void SetComponents()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        hitboxRenderer = attackHitbox.GetComponent<MeshRenderer>();
        hitbox = attackHitbox.GetComponent<BoxCollider>();
    }

    private void SetTransperency(float a = 0f)
    {
        hitboxRenderer.material.color = new Color(1f, 0f, 0f, a);
    }
    #endregion
}
