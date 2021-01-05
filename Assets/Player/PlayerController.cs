using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [Header("Propriétés générales")]
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _rotationHSpeed = 10; // horizontal rotation speed
    [SerializeField]
    private float _rotationVSpeed = 8; // vertical rotation speed

    // 
    private PlayerMotor _motor;
    private Vector2 _direction;
    private Vector2 _rotation;

    #region general event
    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _direction = new Vector2();
        _rotation = new Vector2();
    }

    private void Update()
    {
        // Déplacement du joueur
        Vector3 deplacement = Vector3.zero;
        // avant arrière
        deplacement += transform.forward * _direction.y * _speed;
        // chasse gauche droite
        deplacement += transform.right * _direction.x * _speed;
        _motor.Move(deplacement);

        // rotation du joueur
        Vector3 rotation = Vector3.zero;
        // droite gauche
        rotation += new Vector3(0f, _rotation.x, 0f) * _rotationHSpeed;
        _motor.Rotate(rotation);

        // rotation du torse du joueur
        Vector3 rotationTorso = Vector3.zero;
        // haut bas
        rotationTorso += new Vector3(_rotation.y * -1, 0f, 0f) * _rotationVSpeed;
        _motor.RotateTorso(rotationTorso);
        // reinitialisation du vecteur rotation (cumulé à chaque mouvement de la souris)
        _rotation = new Vector2();
    }

    #endregion

    #region action

    // update direction movement with player action
    public void OnMove(InputValue inputValue)
    {
        Vector2 newDirection = inputValue.Get<Vector2>();
        if(newDirection != null)
        {
            _direction = new Vector2(newDirection.x, newDirection.y);
        }
    }

    // update direction pointed by player
    public void OnLook(InputValue inputValue)
    {
        Vector2 delta = inputValue.Get<Vector2>();
        if(delta != null)
        {
            _rotation += delta;
        }
    }

    #endregion
}
