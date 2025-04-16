using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationFinsihTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void AnimationEnd()
    {
        _player.StateMachine.CurrentState.AnimationFinishTrigger();
    }
}
