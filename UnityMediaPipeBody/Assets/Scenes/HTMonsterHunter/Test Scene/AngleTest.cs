using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour
{
    public GameObject LeftHip;
    public GameObject RightHip;
    public GameObject RightAnkle;

    // 두 벡터간의 각도 계산()
    public float GetAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;
        Debug.Log("referenceVector : " + referenceVector);
        Debug.Log("directionVector : " + directionVector);

        referenceVector.Normalize();
        directionVector.Normalize();
        Debug.Log("referenceVector Normalize : " + referenceVector);
        Debug.Log("directionVector Normalize : " + referenceVector);

        float angle = Vector3.Angle(referenceVector, directionVector);
        Debug.Log("angle : " + angle);

        return angle;
    }

    // 두 벡터간의 부호있는 각도 계산()
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

    


    void Start()
    {
        Debug.Log("[부호없는 각도]");
        GetAngle(LeftHip, RightHip, RightAnkle, RightHip);

        Debug.Log("[부호있는 각도]");
        GetSignedAngle(LeftHip, RightHip, RightAnkle, RightHip);
        
        /*
        Debug.Log("[각도 계산 테스트]");
        Vector3 vectorAB = RightHip.transform.position - LeftHip.transform.position;
        Vector3 vectorBC = RightHip.transform.position - RightAnkle.transform.position;

        // 두 벡터를 정규화
        vectorAB.Normalize();
        vectorBC.Normalize();

        // 벡터 AB와 벡터 BC 사이의 각도를 계산
        float angle = Vector3.Angle(vectorAB, vectorBC);

        // 결과를 출력
        Debug.Log("각도 : " + angle );
        */
    }

    void Update()
    {
        
    }
}
