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
    private Color hitboxColor;
    private Color transperent = new Color(1f, 0f, 0f, 0f);
    private Color visible = new Color(1f, 0f, 0f, 0.2f);
    public float hitboxCoolDown;
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

        hitboxColor = hitboxRenderer.material.color;
        hitboxColor = transperent;
        //Debug.Log($"{hitbox.name} {cameraTransform.name}");
    }

    private void Update()
    {
        PositionHitbox();
    }
    #endregion

    private void Attack()
    {
        Debug.Log("<color=orange> Attack </color>" + "button is pressed");
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
        hitboxRenderer.material.color = visible;
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
