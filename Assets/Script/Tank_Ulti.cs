using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tank_Ulti : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoomedOutSize = 30f;
    public float zoomDuration = 1f;
    public Transform gunBarrel;
    public List<Canvas> canvasList;
    public LineRenderer laserPreview;
    public float laserLength = 100f;
    public float waitUntiEffect = 5f;
    public GameObject bigBulletPrefab;
    public LayerMask enemyLayer;
    public float cooldown = 30f;
    public TextMeshProUGUI cooldownText;
    public Image cooldownImage;
    public GameObject laserUltiAnimator;
    public int laserDamage = 50;
    public Vector3 skillCameraPosition = new Vector3(101.4f, 717.1f, -10f);
    private Collider2D curConfiner;
    CinemachineConfiner confiner;
    private bool isUsingSkill = false;
    private float defaultSize;
    private bool isLaserActive = false;
    private float remainingCooldown = 0f;
    void Start()
    {
        defaultSize = vcam.m_Lens.OrthographicSize;
        laserPreview.enabled = false;
        confiner = FindFirstObjectByType<CinemachineConfiner>();
        cooldownImage.fillAmount = 0;
    }
    void Update()
    {
        if ( remainingCooldown <= 0 )
        {
            cooldownText.text = "";
        }
        if (Input.GetKeyDown(KeyCode.J) && !isUsingSkill && remainingCooldown <= 0 )
        {
            if (confiner != null)
            {
                curConfiner = confiner.m_BoundingShape2D;
            }
            isUsingSkill = true;
            isLaserActive = true;
            confiner.m_BoundingShape2D = null;
            StartCoroutine(ActivateUltimate());
        } else if ( remainingCooldown > 0 )
        {
            remainingCooldown -= Time.deltaTime;
            float fill = Mathf.Clamp01(remainingCooldown / cooldown);
            cooldownImage.fillAmount = fill;
            cooldownText.text = Mathf.CeilToInt(remainingCooldown).ToString();
        }

        if (isLaserActive)
        {
            ShowLaser();
        }
    }

    IEnumerator ZoomOutCamera()
    {
        float t = 0;
        float startSize = vcam.m_Lens.OrthographicSize;
        vcam.Follow = null;
        vcam.transform.position = skillCameraPosition;
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, zoomedOutSize, t / zoomDuration);
            yield return null;
        }
    }

    void ShowLaser()
    {
        Vector3 start = gunBarrel.position;
        Vector3 direction = gunBarrel.up;
        Vector3 end = start + direction * laserLength;
        laserPreview.sortingOrder = 100;
        laserPreview.positionCount = 2;
        laserPreview.SetPosition(0, start);
        laserPreview.SetPosition(1, end);
        laserPreview.enabled = true;
    }


    GameObject FireLaser()
    {
        return Instantiate(laserUltiAnimator, gunBarrel.position, gunBarrel.rotation);
    }

    void changeStateAllCanvas( bool state )
    {
        foreach ( Canvas c in canvasList)
        {
            c.enabled = state;
        }
    }


    IEnumerator ActivateUltimate()
    {
        changeStateAllCanvas(false);
        yield return StartCoroutine(ZoomOutCamera());

        ShowLaser(); 
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;

        GameObject laserObj = FireLaser();
        isLaserActive = false;
        laserPreview.enabled = false;
        Animator laserAnim = laserObj.GetComponent<Animator>();
        float animLength = 0f;
        if (laserAnim != null && laserAnim.runtimeAnimatorController != null)
        {
            AnimatorStateInfo stateInfo = laserAnim.GetCurrentAnimatorStateInfo(0);
            animLength = stateInfo.length;
            yield return new WaitForSeconds(animLength + waitUntiEffect);
        }
        vcam.Follow = transform;
        float t = 0;
        float startSize = vcam.m_Lens.OrthographicSize;
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, defaultSize, t / zoomDuration);
            yield return null;
        }
        confiner.m_BoundingShape2D = curConfiner;
        changeStateAllCanvas(true);
        isUsingSkill = false;
        remainingCooldown = cooldown;
    }

}
