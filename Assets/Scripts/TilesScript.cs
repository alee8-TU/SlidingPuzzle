using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 correctPosition;
    private SpriteRenderer _sprite;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        correctPosition = targetPosition;
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        if(targetPosition == correctPosition)
        {
            _sprite.color = Color.green;
        }
        else
        {
            _sprite.color = Color.white;
        }
    }

    void SetTransform(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }
}
