using UnityEngine;

namespace Shinjingi
{
    [RequireComponent(typeof(Controller))]
    public class Move : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;
        [SerializeField, Range(0.05f, 0.5f)] private float _wallStickTime = 0.25f;

        private Controller _controller;
        public Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody2D _body;
        private CollisionDataRetriever _collisionDataRetriever;
        private WallInteractor _wallInteractor;

        private float _maxSpeedChange, _acceleration, _wallStickCounter;
        private bool _onGround;

        public Animator animator;

        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
            _controller = GetComponent<Controller>();
            _wallInteractor = GetComponent<WallInteractor>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _direction.x = _controller.input.RetrieveMoveInput();
            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _collisionDataRetriever.Friction, 0f);
            animator.SetFloat("Speed", Mathf.Abs(_direction.x));

            Flip();
        }

        private void Flip()
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                spriteRenderer.flipX = false;
            }

        }

        private void FixedUpdate()
        {
            _onGround = _collisionDataRetriever.OnGround;
            _velocity = _body.velocity;

            _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
            _maxSpeedChange = _acceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

            #region Wall Stick
            if(_collisionDataRetriever.OnWall && !_collisionDataRetriever.OnGround && !_wallInteractor.WallJumping)
            {
                if(_wallStickCounter > 0)
                {
                    _velocity.x = 0;

                    if(_controller.input.RetrieveMoveInput() == _collisionDataRetriever.ContactNormal.x)
                    {
                        _wallStickCounter -= Time.deltaTime;
                    }
                    else
                    {
                        _wallStickCounter = _wallStickTime;
                    }
                }
                else
                {
                    _wallStickCounter = _wallStickTime;
                }
            }
            #endregion

            _body.velocity = _velocity;
        }
    }
}
