using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private InputSystem_Actions controls;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

        controls = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Restart.performed += OnRestartInput;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Restart.performed -= OnRestartInput;
    }

    #region Restart
    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnRestartInput(InputAction.CallbackContext ctx)
    {
        RestartScene();
    }
    public void RestartOnDeath(float delay = 4f)
    {
        Invoke(nameof(RestartScene), delay);
    }
    #endregion Restart
}
