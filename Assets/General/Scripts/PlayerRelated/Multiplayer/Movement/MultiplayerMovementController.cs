using System;
using System.Collections;
using System.Linq;
using Fusion;
using Scripts.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;

namespace Scripts.Player.Controllers

{
    public class MultiplayerMovementController : NetworkBehaviour
    {
        [Header("Movement Blocks")] //
        private const float MovementGridAdjust = .8f;

        [field: SerializeField] public BoxCollider2D LeftCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D RightCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D UpCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D DownCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D PlayerCollider { get; private set; }

        private MultiplayerSpriteChanger _multiplayerSpriteChanger;
        private MultiplayerInput _multiplayerInput;

        public TilemapCollider2D[] LevelObstacles { get; private set; }

        private Vector2 _finalTransform;
        [Networked] public NetworkBool IsMoving { get; set; } = false;
        private bool _isPressingToWalk;
        [SerializeField] private float moveSpeed = 4;


        public void Awake()
        {
            PlayerCollider.transform.parent.gameObject.SetActive(HasInputAuthority);
            _multiplayerSpriteChanger = GetComponentInChildren<MultiplayerSpriteChanger>();
            _multiplayerInput = GetComponent<MultiplayerInput>();
            
        }

        private void Start()
        {
            LevelObstacles = FindObjectsByType<TilemapCollider2D>(sortMode: FindObjectsSortMode.None,
                findObjectsInactive: FindObjectsInactive.Include);
        }

        private void Update()
        {
            if (!HasInputAuthority) return;
            transform.position = Vector3.MoveTowards(transform.position,
                _finalTransform, Time.deltaTime * moveSpeed);
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasInputAuthority) return;

            var direction = _multiplayerInput.InputTreatment();
            if (IsMoving) return;

            _multiplayerSpriteChanger.SetAnimationValue(direction);

            _finalTransform =
                (Vector2)transform.position + (direction * MovementGridAdjust);

            StartCoroutine(GridMoveCooldown(direction));
        }


        private IEnumerator GridMoveCooldown(Vector2 direction)
        {
            if (IsMoving) yield break;
            IsMoving = true;

            yield return new WaitUntil(() => (Vector2)transform.position == _finalTransform);

            IsMoving = false;
        }
    }
}