using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{
    public Vector3 BackGroundPos;
    public float width;
    public float height;
    public float X;
    public float Y;
    void OnInvisble()
    {
        BackGroundPos=gameObject.transform.position;
        X=BackGroundPos.x+width;
        Y=BackGroundPos.y+height;
        gameObject.transform.position=new Vector3(X,Y,BackGroundPos.z);
    }

}
