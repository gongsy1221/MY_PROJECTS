using Cinemachine;
using Hellmade.Sound;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public enum UrgotStateEnum
{
    Idle,
    Battle,
    Sting,
    Shoot,
    Die,
    Up,
    Down,
    Heal,
    Ultimate,
    Laser
}

public class EnemyUrgot : Enemy
{
    public EnemyStateMachine<UrgotStateEnum> StateMachine { get; private set; }

    public int damage = 1;

    [Header("AttackRange")]
    public float attackCheckerRadius;

    public Transform attackCheckerTrm;
    public Transform UpattackCheckerTrm;
    public Transform DownattackCheckerTrm;
    public Collider2D downAttackCol;
    [SerializeField] private ParticleSystem _hitParticles;

    public AudioClip[] audioList;

    [SerializeField] private BossHPUI _bossUI;
    public PlayableDirector EndTimeline;

    public UnityEvent OnDead;

    private CinemachineImpulseSource _camImpulse;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine<UrgotStateEnum>();
        _camImpulse = GetComponent<CinemachineImpulseSource>();

        foreach (UrgotStateEnum state in Enum.GetValues(typeof(UrgotStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Urgot{typeName}State");

            if (t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<UrgotStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"Enemy goul: no State[{typeName}]");
            }
        }
    }

    protected override void HandleHitEvent()
    {
        _hitParticles.Play();
        var player = PlayerManager.Instance.PlayerTrm;
        Vector2 dir = -(player.transform.position - transform.position).normalized;
        dir = dir / 2;
        CamImpulse(dir);
    }

    public void CamImpulse(Vector3 impulse)
    {
        _camImpulse.m_DefaultVelocity = impulse;
        _camImpulse.GenerateImpulse();
    }

    protected void Start()
    {
        StateMachine.Initialize(UrgotStateEnum.Idle, this);
        _bossUI.SetBoss(this);
        downAttackCol.enabled =false;
    }

    protected void Update()
    {
        if(PlayerManager.Instance.Player.isDead)
        {
            // �÷��̾� �׾����� ���Ŵ���
            StateMachine.ChangeState(UrgotStateEnum.Idle);
        }

        StateMachine.CurrentState.UpdateState();
    }

    public override void OnAnimationTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public override void Attack()
    {
        DamageCasterCompo.CastDamage(0, attackCheckerTrm);
    }
    protected override void HandleDead(Vector2 direction)
    {
        StateMachine.ChangeState(UrgotStateEnum.Die, true);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}
