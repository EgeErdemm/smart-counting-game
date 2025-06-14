using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void Load2DScene()
    {
        SceneManager.LoadScene("2D");
    }

    public void Load3DScene()
    {
        SceneManager.LoadScene("3D");
    }
}
