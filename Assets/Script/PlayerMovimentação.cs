using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimentação : MonoBehaviour
{
    private Rigidbody2D rig;
    public Camera cam;
    public Vector2 movement;
    public Vector2 mousePosition;
    private RaycastHit2D[] _enemyCollisions;
    private float distance;
    public GameObject enemy;
    public float speed;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float _enemycheckcircleradius;

    [SerializeField]
    private float _enemyCheckDistance;

    [SerializeField]
    private LayerMask _enemyLayerMask;

     

    private void Awake()
    {
        _enemyCollisions = new RaycastHit2D[100];
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FollowEnemy();
        Debug.Log("follow");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rig.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rig.rotation = angle;
    }

    public void SetTarget(GameObject target)
    {
        enemy = target;
    }

    public void FollowEnemy()
    {
        distance = Vector2.Distance(transform.position, transform.forward);
        Vector2 direction = enemy.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, enemy.transform.position, speed * Time.deltaTime);

        HandleEnemys();
        Debug.Log("avoid");
    }

    private void HandleEnemys()
    {
        var contactFilter = new ContactFilter2D();

        contactFilter.SetLayerMask(_enemyLayerMask);

        int numberOfColisions = Physics2D.CircleCast(
            transform.position,
            _enemycheckcircleradius,
            transform.up,
            contactFilter,
            _enemyCollisions,
            _enemyCheckDistance);

        for ( int index = 0; index < numberOfColisions; index++)
        {
            var enemyCollision = _enemyCollisions[index];

            //saveforenemyslater
            //if(enemyColision.collider.gameObject == gameObject)
            //{
            //    continue;
            //}

            movement = enemyCollision.normal;
            break;
        }
    }


}
