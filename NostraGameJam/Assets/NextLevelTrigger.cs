using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something collider");
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayClip(AudioManager.instance.winAudio, true, 0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


        }

    }
}
