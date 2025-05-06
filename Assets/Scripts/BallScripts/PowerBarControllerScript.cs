using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PowerBarControllerScript : MonoBehaviour
{
    public Image _powerFrameFill;

    public void ShowForce(float force)   // not yet implemented
    {
        _powerFrameFill.fillAmount = force;
    }
}
