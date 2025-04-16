using Hellmade.Sound;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] private float _chainLength = 3f;
    [SerializeField] private float _chainSpeed = 0.08f;
    [SerializeField] private int _damage = 35;

    private EnemyUrgot _enemy;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private Entity _player;

    private float _currentX = 0;
    private float _playerX;
    private int _moveDirection;
    private bool _isHitPlayer = false;

    private void Awake()
    {
        _player = PlayerManager.Instance.Player;
        _enemy = GameObject.Find("Urgot").GetComponent<EnemyUrgot>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        EazySoundManager.PlaySound(_enemy.audioList[3]);

        if (_player.transform.position.x > _enemy.transform.position.x)
            _moveDirection = 1;
        else
            _moveDirection = -1;

        _enemy.FlipController(_moveDirection);

        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }

    private void Update()
    {
        if (_isHitPlayer)
        {
            HitPlayerEvent();
            _player.StopImmediately(true);
        }
        else
        {
            if (transform.localScale.x <= _chainLength && transform.localScale.x >= -_chainLength)
            {
                _currentX += _chainSpeed * _moveDirection;
                transform.localScale = new Vector3(_currentX, transform.localScale.y, 0);
            }
            else
            {
                Destroy(gameObject);
                _enemy.OnAnimationTrigger();
            }
        }
    }

    private void HitPlayerEvent()
    {
        if (_currentX >= 0.1 || _currentX <= -0.1)
        {
            _currentX -= _chainSpeed * _moveDirection;
            _playerX -= _chainSpeed * _moveDirection * 3f;

            transform.localScale = new Vector3(_currentX, transform.localScale.y, 0);
            _player.transform.position = new Vector3(_playerX, _player.transform.position.y, 0);
        }
        else
        {
            Destroy(gameObject);
            _enemy.OnAnimationTrigger();
            _player.StopImmediately(false);
            _player.HealthCompo.ApplyDamage(_damage, Vector2.zero, Vector2.zero);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerX = _player.transform.position.x;
            _isHitPlayer = true;
        }
    }
}
