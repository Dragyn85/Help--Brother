using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //          https://opengameart.org/users/luckius
    //          Sound Effect By Nicole Marie T

    [SerializeField] private InteractionType _typeOfInteraction;
    [SerializeField] private float range = 4f;
    [SerializeField] private AudioSource _audioSource;


    [Tooltip("only needed for light interaction type")] [SerializeField]
    private List<Light> _lightSources;

    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private GuidedBrother _brother;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif


    [ContextMenu("Simulate Interaction")]
    public void Interacted()
    {
        if (_typeOfInteraction == InteractionType.sound)
            MakeSound();
        else if (_typeOfInteraction == InteractionType.light)
            MakeLight();
    }

    private void MakeLight()
    {
        var lights = _lightSources.OrderBy(t => Vector3.Distance(_brother.transform.position, t.transform.position));
        foreach (var light in lights)
        {
            var playerdirection = (_brother.transform.position - light.transform.position).normalized;
            light.enabled = !light.enabled;

            if (light.enabled)
            {
                var ray = new Ray(light.transform.position, playerdirection);
                Debug.DrawRay(light.transform.position, playerdirection, Color.red, 5f);
                if (Physics.Raycast(ray, out var hitInfo, 20f))
                {
                    var player = hitInfo.collider.GetComponent<GuidedBrother>();
                    if (player != null)
                    {
                        SetPlayerNavMeshTarget(light.transform.position);
                        break;
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