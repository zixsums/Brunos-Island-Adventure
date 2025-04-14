using UnityEngine.SceneManagement;

namespace RPG.Core
{
    public class SceneTransition
    {
            public static void Initiate(int sceneIndex)
            {
                SceneManager.LoadScene(sceneIndex);
            }
     }
    
 }

