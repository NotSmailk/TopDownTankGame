using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectFactory : ScriptableObject
{
    private Scene _scene;

    protected T CreatObject<T>(T prefab) where T : MonoBehaviour
    {
        if (!_scene.isLoaded)
        {
            if (Application.isEditor)
            {
                _scene = SceneManager.GetSceneByName(name);
                if (!_scene.isLoaded)
                {
                    _scene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                _scene = SceneManager.CreateScene(name);
            }
        }

        T instance = Instantiate(prefab);
        SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);
        return instance;
    }
}
