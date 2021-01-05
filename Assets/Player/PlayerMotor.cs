using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 _deplacement;
    private Vector3 _rotation;
    private Vector3 _rotationTorso;

    [Header("Composants")]
    private Rigidbody _rigidbody;
    [SerializeField]
    private GameObject _torso;

    #region Unity event
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _deplacement = Vector3.zero;
        _rotation = Vector3.zero;
        _rotationTorso = Vector3.zero;
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }
    #endregion

    #region public
    public void Move(Vector3 deplacement)
    {
        //Debug.Log("deplacement " + deplacement);
        _deplacement = deplacement;
    }

    public void Rotate(Vector3 rotation)
    {
        _rotation += rotation;
    }

    public void RotateTorso(Vector3 rotation)
    {
        //Debug.Log("rotation " + rotation);
        _rotationTorso += rotation;
    }
    #endregion

    #region private 
    private void PerformMovement()
    {
        //Debug.Log("_deplacement " + _deplacement);
        if(_deplacement != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + _deplacement * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        if(_rotation != Vector3.zero)
        {
            // tourne le joueur
            _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(new Vector3(0f, _rotation.y, 0f) * Time.fixedDeltaTime));
            _rotation = Vector3.zero;

            // tourne le torse 
            if(_torso != null)
            {
                // haut bas
                _torso.transform.Rotate(new Vector3(_rotationTorso.x, 0f, 0f) * Time.fixedDeltaTime); 
            }
        }
        _rotationTorso = Vector3.zero;
    }
    #endregion
}
