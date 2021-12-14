using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


public class MiniGame : MonoBehaviour
{
    public List<AnimationClip> SpawnAnim = new List<AnimationClip>();
    public List<AnimationClip> DespawnAnim = new List<AnimationClip>();

    [SerializeField] private GameObject LosePrefab;
    [SerializeField] private GameObject WinPrefab;
    protected bool IsGameFinishLoseCoroutineStarted = false;
    protected bool IsGameFinishWinCoroutineStarted = false;

    public IEnumerator SpawnAnimation()
    {
        Animator fwAnimation = GetComponentInParent<Animator>();
        int RandomIndex = Random.Range(0, SpawnAnim.Count);
        fwAnimation.Play(SpawnAnim[RandomIndex].name);
        yield return new WaitForSeconds(SpawnAnim[RandomIndex].length);
    }

    public IEnumerator DespawnAnimation()
    {
        Animator fwAnimation = GetComponentInParent<Animator>();
        int RandomIndex = Random.Range(0, DespawnAnim.Count);
        fwAnimation.Play(DespawnAnim[RandomIndex].name);
        yield return new WaitForSeconds(DespawnAnim[RandomIndex].length);
    }

    public IEnumerator SpawnEffect(GameObject prefab, GameObject target, Vector2 offset)
    {
        Vector3 targetPos = target.transform.position;
        GameObject instGameObject = Instantiate(prefab, (Vector2)targetPos + offset, Quaternion.identity, this.transform);
        Animator instAnimator = instGameObject.GetComponentInChildren<Animator>();
        yield return new WaitForSeconds(instAnimator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(instGameObject);
    }

    public IEnumerator GameFinishLose()
    {
        IsGameFinishLoseCoroutineStarted = true;
        GameObject instGameObject = Instantiate(LosePrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        Animator myAnimator = instGameObject.GetComponent<Animator>();
        //fwAnimation.Play(GameFinishList.name);
        yield return StartCoroutine(DespawnAnimation());
        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(instGameObject);

        GameManager.Instance.SpawnFortuneWheel();
        IsGameFinishLoseCoroutineStarted = false;
        Destroy(this.transform.parent.gameObject);
        
    }

    public IEnumerator GameFinishWin()
    {
        IsGameFinishWinCoroutineStarted = true;
        GameObject instGameObject = Instantiate(WinPrefab, GameObject.FindGameObjectWithTag("GameContainer").transform);
        Animator myAnimator = instGameObject.GetComponent<Animator>();
        //fwAnimation.Play(GameFinishList.name);
        yield return StartCoroutine(DespawnAnimation());
        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(instGameObject);


        GameManager.Instance.SpawnFortuneWheel();
        IsGameFinishWinCoroutineStarted = false;
        Destroy(this.transform.parent.gameObject);
    }
}
