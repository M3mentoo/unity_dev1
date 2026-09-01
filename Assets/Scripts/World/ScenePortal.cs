using Tutorial.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tutorial.World
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class ScenePortal : MonoBehaviour
    {
        [SerializeField] private string _targetSceneName;

        private bool _isLoading;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isLoading || other.GetComponentInParent<PlayerMovement>() == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(_targetSceneName)
                || !Application.CanStreamedLevelBeLoaded(_targetSceneName))
            {
                Debug.LogError(
                    $"Portal target scene '{_targetSceneName}' is not enabled in Build Settings.",
                    this);
                return;
            }

            _isLoading = true;
            SceneManager.LoadScene(_targetSceneName);
        }
    }
}
