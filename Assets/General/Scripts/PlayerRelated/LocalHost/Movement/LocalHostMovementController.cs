using System;
using System.Collections;
using System.Linq;
using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Vector2 = UnityEngine.Vector2;

namespace Scripts.Player.Controllers

{
    public class LocalHostMovementController : NetworkBehaviour
    {
        [Header("Movement Blocks")] //
        private const float MovementGridAdjust = .8f;

        [field: SerializeField] public BoxCollider2D LeftCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D RightCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D UpCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D DownCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D PlayerCollider { get; private set; }
        public TilemapCollider2D[] LevelObstacles { get; private set; }

        [SerializeField] private LocalHostSpriteChanger localHostSpriteChanger;

        [Networked] public NetworkBool IsMoving { get; set; } = false;
        private Vector2 _finalTransform;
        [SerializeField] private float moveSpeed = 4;

        private void Awake()
        {
            PlayerCollider.transform.parent.gameObject.SetActive(true);
        }

        private void Start()
        {
            LevelObstacles = FindObjectsByType<TilemapCollider2D>(sortMode: FindObjectsSortMode.None,
                findObjectsInactive: FindObjectsInactive.Include);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _finalTransform, Time.deltaTime * moveSpeed);
        }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out PlayerNetworkData data)) return;
            if (IsMoving) return;

            localHostSpriteChanger.RPC_SetAnimationValue(data.Direction);

            _finalTransform =
                (Vector2)transform.position + (data.Direction * MovementGridAdjust);

            StartCoroutine(GridMoveCooldown(data));
        }


        private IEnumerator GridMoveCooldown(PlayerNetworkData data)
        {
            if (IsMoving) yield break;
            IsMoving = true;

            yield return new WaitUntil(() => (Vector2)transform.position == _finalTransform);

            IsMoving = false;
        }
    }
}