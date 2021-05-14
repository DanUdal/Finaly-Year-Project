using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNote : MonoBehaviour
{
    [SerializeField] private AnimationCurve yAxisAnimationCurve;
    [SerializeField] private AnimationCurve zAxisAnimationCurve;

    private Transform _transform;
    private Vector3 _spawnPosition;






    private void Awake()
    {
        _transform = transform;
    }

    public void Initialise(Vector3 spawnPosition, float note, float octave)
    {
        SetColour(octave);
        SetBehavour();

        _spawnPosition = spawnPosition;

        StartCoroutine(PlayNoteSound());
        StartCoroutine(KillNote());
    }

    private void SetBehavour()
    {
        StartCoroutine(StartTravel());
    }

    private void SetColour(float octave)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = new Material(meshRenderer.material);

        float inverseLerpOctave = Mathf.InverseLerp(0, 7, octave);
        material.color = Color.Lerp(Color.green, Color.blue, inverseLerpOctave);

        meshRenderer.material = material;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }




    private IEnumerator StartTravel()
    {
        float lifeTime = 0;
        while (Application.isPlaying)
        {
            Vector3 offset = new Vector3(0, yAxisAnimationCurve.Evaluate(lifeTime), zAxisAnimationCurve.Evaluate(lifeTime));

            _transform.position = _spawnPosition + offset;

            lifeTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator PlayNoteSound()
    {
        yield return new WaitForSeconds(ProjeectileSpawner.NoteDelay);

        // play note sound
    }

    private IEnumerator KillNote()
    {
        yield return new WaitForSeconds(ProjeectileSpawner.NoteDelay + ProjeectileSpawner.Timeout);

        Destroy(this.gameObject);
    }
}
