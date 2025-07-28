using UnityEngine;

public class BeforeCompleteCover : MonoBehaviour
{
    public GameObject cover;
    public void SwitchOnTheCover()
    {
        cover.SetActive(true);
    }
    public void SwitchOffTheCover()
    {
        cover.SetActive(false);
    }
}
