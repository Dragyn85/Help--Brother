using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Interaction : MonoBehaviour
{
    //          Sound Effect By https://opengameart.org/users/luckius
    //          Sound Effect By Nicole Marie T
    //          Music By    https://opengameart.org/users/emmama

    [SerializeField] private InteractionType _typeOfInteraction;
    [SerializeField] private float range = 4f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;


    [Tooltip("only needed for light interaction type")] [SerializeField]
    private List<Light> _lightSources;

    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private GuidedBrother _brother;
    private int interactID = Animator.StringToHash("interact");


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(_typeOfInteraction == InteractionType.sound)
            Gizmos.DrawWireSphere(transform.position, range);
        else
        {
            foreach (Light lightSource in _lightSources)
            {
                Gizmos.DrawWireSphere(lightSource.transform.position, range);
            }
        }
    }

    private void OnValidate()
    {
        _lightSources = GetComponentsInChildren<Light>().ToList();
        _brother = FindObjectOfType<GuidedBrother>();
    }
#endif


    [ContextMenu("Simulate Interaction")]
    public void Interacted()
    {
        if (_animator != null)
            _animator.SetTrigger(interactID);
        if (_typeOfInteraction == InteractionType.sound)
            MakeSound();
        else if (_typeOfInteraction == InteractionType.light)
            MakeLight();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GhostController>())
            Interacted();
    }

    private void MakeLight()
    {
        PlayRandomSound();
        var lights = _lightSources.OrderBy(t => Vector3.Distance(_brother.transform.position, t.transform.position));
        bool firstLightDetected = false;
        foreach (var light in lights)
        {
            var playerdirection = (_brother.transform.position - light.transform.position).normalized;
            light.enabled = !light.enabled;

            if (light.enabled)
            {
                var ray = new Ray(light.transform.position, playerdirection);
                Debug.DrawRay(light.transform.position, playerdirection, Color.red, 20f);
                if (Physics.Raycast(ray, out var hitInfo, 20f))
                {
                    var player = hitInfo.collider.GetComponent<GuidedBrother>();
                    if (player != null && firstLightDetected == false && Vector3.Dot(playerdirection,player.GetDirection()) < -0.5)
                    {
                        firstLightDetected = true;
                        SetPlayerNavMeshTarget(light.transform.position);
                    }
                }
            }
        }
    }

    private void MakeSound()
    {
        PlayRandomSound();
        if (Vector3.Distance(_brother.transform.position, transform.position) <= range)
        {
            Debug.Log("I Can here a sound");
            SetPlayerNavMeshTarget();
        }
    }

    private void PlayRandomSound()
    {
        var random = Random.Range(0, _clips.Count);
        _audioSource.PlayOneShot(_clips[random]);
    }

    [ContextMenu("Set Navmesh Target")]
    public void SetPlayerNavMeshTarget(Vector3? position = null)
    {
        if (position == null)
            _brother.SetDestionation(transform.position);
        else
            _brother.SetDestionation((Vector3) position);
    }

    private enum InteractionType
    {
        sound,
        light
    }
}