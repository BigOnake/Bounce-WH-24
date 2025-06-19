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
    public float knockMult;
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
        /*if(!IsOwner)
        {
            return;
        }*/

        if (CheckHitbox(other))
        {
            Debug.Log($"{other.gameObject.name} was hit");
            NetMovement otherP = other.gameObject.GetComponentInChildren<NetMovement>();
            NetworkObject otherPNet = other.gameObject.GetComponent<NetworkObject>();
            if (otherP)
                KnockBackPlayer(otherP, other.transform.position, otherPNet.OwnerClientId);
        }
    }

    private void KnockBackPlayer(NetMovement otherP, Vector3 pPos, ulong otherId)
    {
        Debug.Log($"{otherP.gameObject.name} recognized");
        //otherP.isKnocked.Value = true;
        Vector3 kbDis = pPos - transform.root.position;
        otherP.SendKbDirRpc(kbDis, otherId);
    }

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
            other.GetComponent<NetworkObject>()
            );
    }
}
