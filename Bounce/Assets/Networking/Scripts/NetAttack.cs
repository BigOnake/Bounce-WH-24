using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetAttack : MonoBehaviour
{
    #region Fields
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
    #endregion

    private void Attack()
    {
        Debug.Log("<color=orange> Attack </color>" + "button is pressed");
    }
}
