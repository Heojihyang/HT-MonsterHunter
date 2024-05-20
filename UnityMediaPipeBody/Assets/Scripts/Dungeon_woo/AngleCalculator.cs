using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleCalculator : MonoBehaviour
{
    // 두 벡터간의 각도 계산() 0도에서 180도 사이의 값
    public float GetAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;

        referenceVector.Normalize();
        directionVector.Normalize();

        float angle = Vector3.Angle(referenceVector, directionVector);

        return angle;
    }

    /*
    // 두 벡터간의 부호있는 각도 계산() -180도에서 180
    public float GetSignedAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;
        Debug.Log("referenceVector : " + referenceVector);
        Debug.Log("directionVector : " + directionVector);

        referenceVector.Normalize();
        referenceVector.Normalize();
        Debug.Log("referenceVector Normalize : " + referenceVector);
        Debug.Log("directionVector Normalize : " + referenceVector);

        Vector3 crossProduct = Vector3.Cross(referenceVector, directionVector);
        Debug.Log("crossProduct : " + crossProduct);

        float signedAngle = Vector3.SignedAngle(referenceVector, directionVector, crossProduct);
        Debug.Log("signedAngle : " + signedAngle);

        return signedAngle;
    }
    */
}
