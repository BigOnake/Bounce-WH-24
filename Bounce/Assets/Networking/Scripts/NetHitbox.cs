using Unity.Netcode;
using UnityEngine;

public class NetHitbox : MonoBehaviour
{
    #region Fields
    private NetAttack attack;
    private BoxCollider hitbox;
    private CapsuleCollider hurtbox;
    private MeshRenderer hitboxRenderer;
    private Transform cameraTransform;
    private Vector3 cameraDirection;
    private float transperent = 0f;
    private float visible = 0.2f;
    public bool displayHitbox = true;
    #endregion

    #region GameEngineLoop
    private void Awake()
    {
        SetComponents();
        SetTransperency(transperent);
    }

    private void Update()
    {
        PositionHitboxRpc();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (CheckHitbox(other))
        {
            Debug.Log($"{other.gameObject.name} was hit");
        }
    }

    private void PositionHitboxRpc()
    {
        cameraDirection = cameraTransform.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection);
        transform.position = cameraTransform.position + cameraDirection;
    }

    private void SetComponents()
    {
        attack = GetComponentInParent<NetAttack>();
        cameraTransform = transform.parent.GetComponentInChildren<Camera>().transform;
        hitboxRenderer = GetComponent<MeshRenderer>();
        hitbox = GetComponent<BoxCollider>();
    }

    private void SetTransperency(float a = 0f)
    {
        if (displayHitbox)
        {
            hitboxRenderer.material.color = new Color(1f, 0f, 0f, a);
        }
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
        SetTransperency(visible);
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
        SetTransperency(transperent);
    }

    private bool CheckHitbox(Collider other)
    {
        return (
            attack.GetAttackStatus() && 
            other.tag == "Player" && 
            other is CapsuleCollider
            );
    }
}
