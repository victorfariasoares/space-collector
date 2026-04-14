using UnityEngine;
using UnityEngine.UI;

public class JoystickMenuSupport : MonoBehaviour
{
    public Button botaoPrincipal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (botaoPrincipal != null && botaoPrincipal.gameObject.activeInHierarchy)
                botaoPrincipal.onClick.Invoke();
        }
    }
}