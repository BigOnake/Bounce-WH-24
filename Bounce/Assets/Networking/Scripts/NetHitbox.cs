using Unity.Netcode;
using UnityEngine;

public class NetHitbox : NetworkBehaviour
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
        PositionHitbox();
    }

    private void OnEnable()
    {
        //Debug.Log("<color=green> Hitbox script is enabled. </color>");
    }

    private void OnDisable()
    {
        //Debug.Log("<color=red> Hitbox script is disabled. </color>");
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        NetworkObject hitNetObj = new();

        if (!attack.GetAttackStatus() || !(hitNetObj = other.GetComponent<NetworkObject>()))
            return;
        /* Important
         * Can attack multiple times for the duration of the coroutine in NetAttack.
         * Logical issues, needs prevention in some way not to overload bandwidth.
         */

        CheckHitboxForPlayer(ref other, ref hitNetObj);
    }

    #region Knockback
    private void KnockBackPlayer(NetMovement hitPlayerMovement, Vector3 hitPlayerPosition, ulong hitPlayerId)
    {
        Debug.Log($"{hitPlayerMovement.gameObject.name} recognized");
        Vector3 knockBackDir = hitPlayerPosition - transform.root.position; //Get dir from playerA to playerB
        knockBackDir.y = 0;
        hitPlayerMovement.SendKbDirRpc(knockBackDir.normalized, hitPlayerId);
    }

    private void CheckHitboxForPlayer(ref Collider col, ref NetworkObject netObj)
    {
        if (col.gameObject.tag != "Player")
            return;

        Debug.Log($"{col.gameObject.name} was hit");
        NetMovement hitPlayerMovement = col.GetComponentInChildren<NetMovement>(); //Needed for rpc

        if (hitPlayerMovement && netObj)
            KnockBackPlayer(hitPlayerMovement, col.transform.position, netObj.OwnerClientId);
    }
    #endregion

    private void PositionHitbox()
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

    #region Auxiliary Methods
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
    #endregion

}
