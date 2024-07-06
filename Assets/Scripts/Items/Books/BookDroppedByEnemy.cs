using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDroppedByEnemy : BookDroppedByPlayer
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2[] dropDirection;
    [SerializeField] private float force;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        base.Start();

        int random = Random.Range(0, dropDirection.Length);
        rb.velocity = dropDirection[random] * force;
    }

    protected override IEnumerator BlinkImage()
    {
        anim.speed = 0;
        sr.color = transparentColor;

        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        
        yield return new WaitForSeconds(0.1f);
        sr.color = transparentColor;

        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);
        sr.color = transparentColor;

        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;

        yield return new WaitForSeconds(0.2f);
        sr.color = transparentColor;

        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        anim.speed = 1;
        canPickUp = true;
    }
}
