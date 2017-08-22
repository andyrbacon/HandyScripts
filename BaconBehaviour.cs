using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class BaconBehaviour : MonoBehaviour {

    ParticleSystem particles;
    Renderer rend;
    Color originalColor;

	public virtual void Start () {
        rend = GetComponent<Renderer>();
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();

        originalColor = rend.material.color;

        particles = GetComponent<ParticleSystem>();
        if (particles == null)
            particles = GetComponentInChildren<ParticleSystem>();
    }
	
	void Update () {
		
	}

    public void PlayParticles()
    {
        if (particles)
        {
            particles.Clear();
            particles.Play();
        }
    }

    public void PunchScale(Transform trans, float scale, TweenCallback onComplete, float duration = 0.5f)
    {
        string id = gameObject.GetInstanceID() + "PunchScale";
        DOTween.Complete(id);
        Vector3 punchScale = new Vector3(scale, scale, scale);
        trans.DOPunchScale(punchScale, duration).SetId(id).OnComplete(onComplete);
    }

    public void ScaleOverTime(Transform trans, float scale, float duration, TweenCallback onComplete, Ease ease = Ease.InSine)
    {
        string id = gameObject.GetInstanceID() + "ScaleOverTime";
        DOTween.Kill(id);
        trans.DOScale(scale, duration).SetEase(ease).SetId(id).OnComplete(onComplete);
    }

    public void MoveOverTime(Transform trans, Vector3 target, float duration, TweenCallback onComplete, Ease ease = Ease.InSine)
    {
        string id = gameObject.GetInstanceID() + "MoveOverTime";
        DOTween.Kill(id);
        trans.DOMove(target, duration).SetEase(ease).SetId(id).OnComplete(onComplete);
    }

    public void EmptyFunction() { }

    public void ChangeColor(Color c, float duration = 0.75f)
    {
        if (!rend) return;
        rend.material.DOColor(c, duration);
    }

    public void ResetColor()
    {
        ResetColor(0.75f);
    }

    public void ResetColor(float duration = 0.75f)
    {
        if (!rend) return;
        rend.material.DOColor(originalColor, duration);
    }

    bool IsLayerMatch(LayerMask layer1, LayerMask layer2)
    {
        if ((1 << layer1 & layer2) > 0)
            return true;
        else
            return false;
    }
}
