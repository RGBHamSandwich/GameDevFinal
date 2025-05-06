using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PowerBarControllerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public Image _powerFrameFill;

    ///// METHODS /////
    public void ShowForce(float force)   // not yet implemented
    {
        _powerFrameFill.fillAmount = force;
    }
}
