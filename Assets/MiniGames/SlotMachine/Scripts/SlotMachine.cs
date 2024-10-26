using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] GameObject reel1;

    public RectTransform reelTransform; // Reference to the reel's RectTransform
    public float singleSpinHeight = 135f; // The height for one spin loop
    public int loopCount = 3; // Number of loops before stopping
    public float spinDuration = 0.5f; // Duration of each spin loop
    // Start is called before the first frame update
    void Start()
    {
        SpinToResult(SpinResult());
    }

    // Update is called once per frame
    void Update()
    {
        //spin to -405
    }

    int SpinResult()
    {
        
            return  Random.Range(0, 4); // 4 possible symbols
        
    }

     // Spin the reel with pre-determined result index
    public void SpinToResult(int resultIndex)
    {
        // Calculate the position where the reel should stop
        float finalPosition = singleSpinHeight * resultIndex;

        // Perform looping spin animation before stopping at the result
        reelTransform.DOLocalMoveY(singleSpinHeight, spinDuration)
            .SetLoops(loopCount, LoopType.Yoyo)
            .OnComplete(() =>
            {
                // Stop at the final result after looping
                reelTransform.DOLocalMoveY(finalPosition, spinDuration);
            });
    }
}
