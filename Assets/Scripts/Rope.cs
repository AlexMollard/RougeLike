using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    float ropeSegLen = 0.06f;
    float ropeSegLenMultiplier;
    int segmentCount = 35;
    public float lineWidth;
    Vector2 StartPos;
    Vector2 EndPos;
    int frameCount = 0;
    int unwindAmount = 1000;
    public GameObject player;
    GameObject heldObject;
    public GameObject rod;
    float height;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        heldObject = player.GetComponent<PlayerController>().heldObject;
        UpdateStartPos();

        EndPos = StartPos;
        
        ropeSegLen = ropeSegLen * 0.1f;
        ropeSegLenMultiplier = ropeSegLen / unwindAmount;

        for (int i = 0; i < segmentCount; i++)
            this.ropeSegments.Add(new RopeSegment(StartPos));
    }

    void UnWind()
    {
        ropeSegLen -= ropeSegLenMultiplier;
    }

    private void Update()
    {
        rod.GetComponent<FishingRodBehaviour>().TestDistance();
        rod.GetComponent<FishingRodBehaviour>().UpdateTimers();

        if (frameCount != unwindAmount)
        {
            UnWind();
            frameCount++;
        }
        UpdateStartPos();
        DrawRope();
    }

    public void UpdateStartPos()
    {
        StartPos = new Vector2(heldObject.transform.position.x, heldObject.transform.position.y);
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    void Simulate()
    {
        Vector2 gravityForce = new Vector2(0.0f,-1.0f);

        for (int i = 0; i < segmentCount; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += gravityForce * Time.deltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        // Constraints
        for (int i = 0; i < 100; i++)
        {
            ApplyConstraints();
        }
    }

    void ApplyConstraints()
    {
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = StartPos;
        this.ropeSegments[0] = firstSegment;

        RopeSegment lastSegment = this.ropeSegments[segmentCount - 1];
        lastSegment.posNow = transform.position;
        this.ropeSegments[segmentCount - 1] = lastSegment;


        for (int i = 0; i < segmentCount - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }

    }

    void DrawRope()
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[segmentCount];
        for (int i = 0; i < segmentCount; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);

    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
