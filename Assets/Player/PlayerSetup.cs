using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
//public class PlayerSetup : MonoBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    private Camera _sceneCamera;

    private void Start()
    {
        // desactivation des composants si n'est pas le joueur local
        if (!isLocalPlayer)
        {
            foreach (Behaviour behaviour in componentsToDisable)
            {
                Behaviour.Destroy(behaviour);
            }
        }
        // notre joueur
        else
        {
            _sceneCamera = Camera.main;
            if (_sceneCamera != null)
            {
                _sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if (_sceneCamera != null)
        {
            _sceneCamera.gameObject.SetActive(true);
        }
    }
}
