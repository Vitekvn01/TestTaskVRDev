using UnityEngine;

public class LoadingCircle : MonoBehaviour
{
    [SerializeField] private RectTransform _rectFillComponent;
    [SerializeField] private float _rotateSpeed = 200f;

    private void Start()
    {
        _rectFillComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectFillComponent.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);
    }
}
