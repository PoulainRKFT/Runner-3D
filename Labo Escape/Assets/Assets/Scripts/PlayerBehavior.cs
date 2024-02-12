using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;

    public GameObject deathImageObject;
    Image deathImage;

    public GameObject deathBackgroundObject;
    Image deathBackground;

    public GameObject inGameUis;
    public GameObject deathUis;

    public GameObject ragdollController;
    public GameObject upperColliderController;
    public GameObject lowerColliderController;
    int playerPositionX = 0;
    bool playerPositionYGround = true;
    public int dashSpeed = 8;
    public float overallSpeedRate = 1;
    public float runSpeed = 6;
    private bool isJumping = false;
    private float isJumpingDelay = 0f;
    bool dead = false;

    UnityEngine.Vector2 startTouchPosition;
    UnityEngine.Vector2 endTouchPosition;
    bool touchingScreen = false;

    public AudioSource backgroundMusic;
    public AudioSource deathSound;
    public AudioSource jumpSound;
    public AudioSource fileSound;

    //Player stats
    public int yellowDocuments = 0;
    public int blueDocuments = 0;

    public void Die() {
        dead = true;
        animator.enabled = false;
        ragdollController.SetActive(true);
        upperColliderController.SetActive(false);
        lowerColliderController.SetActive(false);

        foreach(Transform child in inGameUis.transform) {
            child.gameObject.SetActive(false);
        }

        deathUis.gameObject.SetActive(true);

        deathSound.Play();
        deathImage.color = new Color(1f, 1f, 1f, 1f);

        backgroundMusic.enabled = false;
    }
    

    public void CollectFile(bool blue) {
        if (blue) {
            blueDocuments += 1;
        } else {
            yellowDocuments += 1;
        }
        fileSound.Play();
    }

    public void GoUp() {
        playerPositionYGround = false;
    }

    public void GoDown() {
        playerPositionYGround = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        upperColliderController.SetActive(false);
        lowerColliderController.SetActive(true);
        animator = GetComponent<Animator>();
        deathImage = deathImageObject.GetComponent<Image>();
        deathBackground = deathBackgroundObject.GetComponent<Image>();
        deathImage.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            overallSpeedRate += Time.deltaTime * 0.015f;

            //Touchscreen behavior
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
                startTouchPosition = Input.GetTouch(0).position;
                touchingScreen = true;
            } else if (Input.touchCount > 0) {
                endTouchPosition = Input.GetTouch(0).position;
                if (touchingScreen && endTouchPosition.x > startTouchPosition.x + 250) {
                    if (playerPositionX != 1) {
                        playerPositionX += 1;
                        touchingScreen = false;
                    }
                } else if (touchingScreen && endTouchPosition.x < startTouchPosition.x - 250) {
                    if (playerPositionX != -1) {
                        playerPositionX -= 1;
                        touchingScreen = false;
                    }
                } else if (touchingScreen && endTouchPosition.y > startTouchPosition.y + 200) {
                    isJumpingDelay = 0.35f / overallSpeedRate;
                    touchingScreen = false;
                    jumpSound.Play();
                }

            } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
                touchingScreen = false;
            }

            //Keyboard behavior
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (playerPositionX != -1) {
                    playerPositionX -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (playerPositionX != 1) {
                    playerPositionX += 1;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                isJumpingDelay = 0.35f / overallSpeedRate;
                jumpSound.Play();
            }
            
            if (isJumpingDelay > 0f) {
                isJumping = true;
                isJumpingDelay -= Time.deltaTime;
            } else {
                isJumping = false;
            }

            if ((animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Jump" || isJumping) && (animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 <= 0.75f) {
                if (!upperColliderController.activeSelf) {
                    upperColliderController.SetActive(true);
                }
                if (lowerColliderController.activeSelf) {
                    lowerColliderController.SetActive(false);
                }
            } else {
                if (upperColliderController.activeSelf) {
                    upperColliderController.SetActive(false);
                }
                if (!lowerColliderController.activeSelf) {
                    lowerColliderController.SetActive(true);
                }
            }

            if (isJumping) {
                animator.SetBool("IsJumping", true);
            } else {
                animator.SetBool("IsJumping", false);
            }

            animator.speed = overallSpeedRate;
            this.transform.position = UnityEngine.Vector3.Lerp(this.transform.position, new UnityEngine.Vector3((2.25f * playerPositionX), this.transform.position.y, this.transform.position.z), (dashSpeed * overallSpeedRate) * Time.deltaTime);

            if (playerPositionYGround) {
                if (this.transform.position.y > 0f) {
                    this.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y - ((4.5f * overallSpeedRate) * Time.deltaTime), this.transform.position.z);
                }
            } else {
                if (this.transform.position.y < 4.5f) {
                    this.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y + ((4.5f * overallSpeedRate) * Time.deltaTime), this.transform.position.z);
                }
            }
        } else {
            runSpeed /= 1.1f;

            if (deathBackground.color.a < 1f) {
                deathBackground.color = new Color(0f, 0f, 0f, deathBackground.color.a + (Time.deltaTime * 0.3f));
            } else if (deathBackground.color.a > 1f) {
                deathBackground.color = new Color(0f, 0f, 0f, 1f);
            }
        }

        if (deathImage.color.a > 0f) {
            deathImage.color = new Color(1f, 1f, 1f, deathImage.color.a - Time.deltaTime);
        } else if (deathImage.color.a < 0f) {
            deathImage.color = new Color(1f, 1f, 1f, 0f);
        }

        this.transform.position = new UnityEngine.Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + ((runSpeed * overallSpeedRate) * Time.deltaTime));
    }
}
