using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetAttack : MonoBehaviour
{
    #region Fields
    public GameObject attackHitbox;
    private BoxCollider hitbox;
    private MeshRenderer hitboxRenderer;
    private Transform cameraTransform;
    private Vector3 cameraDirection;
    private Color transperent = new Color(1f, 0f, 0f, 0f);
    public int hitboxCoolDown;
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

        hitboxRenderer.material.color = transperent;
        Debug.Log($"{hitbox.name} {cameraTransform.name}");
    }
    #endregion

    private void Attack()
    {
        Debug.Log("<color=orange> Attack </color>" + "button is pressed");
        PositionHitbox();
        StartCoroutine(DisplayHitbox());
    }

    private void PositionHitbox()
    {
        cameraDirection = cameraTransform.forward;
        attackHitbox.transform.rotation = Quaternion.LookRotation(cameraDirection);
        attackHitbox.transform.position = cameraTransform.position + cameraDirection;
    }

    private IEnumerator DisplayHitbox()
    {
        hitboxRenderer.material.color = new Color(1f, 0f, 0f, 0.2f);
        yield return new WaitForSeconds(hitboxCoolDown);
        hitboxRenderer.material.color = transperent;
    }

    private void SetComponents()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        hitboxRenderer = attackHitbox.GetComponent<MeshRenderer>();
        hitbox = attackHitbox.GetComponent<BoxCollider>();
    }
}
