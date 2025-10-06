using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    public float minHeight = 0;

    // Update is called once per frame
    void Update()
    {
        float yValue = target.transform.position.y;

        if (yValue <= minHeight) {
            yValue = minHeight;
        }

        transform.position = new(target.transform.position.x,yValue,transform.position.z);
    }
}
