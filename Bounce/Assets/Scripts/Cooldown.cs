using UnityEngine;

[System.Serializable]

public class Cooldown
{
    [SerializeField] private float cooldownDuration = 1f;
    private float _cooldownEndTime = 0f;

    public bool IsOnCooldown()
    {
        return Time.time < _cooldownEndTime;
    }

    public void StartCooldown()
    {
        _cooldownEndTime = Time.time + cooldownDuration;
    }

    public void SetCooldownDuration(float newCooldownDuration) { cooldownDuration = newCooldownDuration; }

    public float TimeRemaining()
    {
        return Mathf.Max(0f, _cooldownEndTime - Time.time);
    }
}
