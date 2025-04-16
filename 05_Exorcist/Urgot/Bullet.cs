using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _fireSpeed;
    [SerializeField] private int _damage = 15;

    private Rigidbody2D _rb;
    private Entity _player;
    private Vector3 dir;

    private bool _isHitPlayer = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = PlayerManager.Instance.Player;
    }

    void Start()
    {
        dir = (_player.transform.position - transform.position).normalized;
        _rb.velocity = dir * _fireSpeed;

        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (_isHitPlayer)
        {
            _player.HealthCompo.ApplyDamage(_damage, Vector2.zero, Vector2.zero);
            _isHitPlayer=false;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isHitPlayer = true;
        }
    }
}
