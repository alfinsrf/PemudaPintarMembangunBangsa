using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDroppedByPlayer : Book_Item
{
    [SerializeField] private Vector2 speed;
    [SerializeField] protected Color transparentColor;

    protected bool canPickUp;

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        StartCoroutine(BlinkImage());
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(speed.x, speed.y) * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(canPickUp)
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    protected virtual IEnumerator BlinkImage()
    {
        anim.speed = 0;
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(0.1f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(0.1f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(0.2f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(0.3f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(0.3f);

        speed.x = 0;
        anim.speed = 1;
        canPickUp = true;
    }
}
