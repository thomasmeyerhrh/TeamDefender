using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [Header("Propriétés générales")]
    [SerializeField]
    private float _speed = 20;
    [SerializeField]
    private float _rotationSpeed = 10;

    // 
    private PlayerMotor _motor;
    private Vector2 _direction;
    private Vector2 _rotation;

    [Header("Composants")]
    [SerializeField]
    private GameObject _torso;

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
        // avant arrière
        transform.position += transform.forward * _direction.y * _speed * Time.deltaTime;
        // chasse gauche droite
        transform.position += transform.right * _direction.x * _speed * Time.deltaTime;

        // rotation du joueur
        // droite gauche
        transform.Rotate(new Vector3(0f, _rotation.x, 0f) * _rotationSpeed * Time.deltaTime);
        // haut bas
        if(_torso != null)
        {
            _torso.transform.Rotate(new Vector3(_rotation.y * -1, 0f, 0f) * _rotationSpeed * Time.deltaTime);
        }
        _rotation = new Vector2();
    }

    #endregion

    #region action

    // update direction with player action
    public void OnMove(InputValue inputValue)
    {
        Vector2 newDirection = inputValue.Get<Vector2>();
        Debug.Log("new direction :" + newDirection);
        if(newDirection != null)
        {
            _direction = new Vector2(newDirection.x, newDirection.y);
        }
    }

    // update direction pointed by player
    public void OnPointer(InputValue inputValue)
    {
        Vector2 delta = inputValue.Get<Vector2>();
        Debug.Log("OnPoint :" + delta);
        if(delta != null)
        {
            _rotation += delta;
        }
    }

    #endregion
}
