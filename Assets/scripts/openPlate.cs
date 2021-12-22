using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class openPlate : MonoBehaviour
{
    GameObject gameControl;

    public SpriteRenderer spriteRenderer;
    public Sprite[] faces;

    public Animator animator;

    private bool isCliked = false;

    public int faceIndex;

    public bool mathced = false;

    private void OnMouseDown()
    {
        if (mathced == false)
        {
            if (animator.GetBool("wasClicked") == false)
            {
                if (gameControl.GetComponent<gameControl>().TwoCardsUp() == false)
                {

                    spriteRenderer.sprite = faces[faceIndex];

                    animator.SetBool("wasClicked", !isCliked);

                    isCliked = !isCliked;

                    gameControl.GetComponent<gameControl>().AddVisibleFace(faceIndex);

                    mathced = gameControl.GetComponent<gameControl>().CheckMatch();
                }
            }
            else
            {
                animator.SetBool("wasClicked", !isCliked);

                isCliked = !isCliked;

                gameControl.GetComponent<gameControl>().RemoveVisibleFace(faceIndex);
            }
        }
    }

    private void Awake()
    {
        gameControl = GameObject.Find("gameControl");
    }
}
