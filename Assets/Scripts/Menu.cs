using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = clickSound;

        //Button button = GetComponent<Button>();
    }

    public IEnumerator WaitForSoundClipSingle()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("SinglePlayer");
    }
    public IEnumerator WaitForSoundClipMulti()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        SceneManager.LoadScene("Multiplayer_Launcher");
    }

    public void OnClickSinglePlayer()
    {
        //Debug.Log("Loading singleplayer game");
        //WaitForSoundClip();
        StartCoroutine(WaitForSoundClipSingle());
    }

    public void OnClickMultiPlayer()
    {
        //Debug.Log("Loading multiplayer game");
        StartCoroutine(WaitForSoundClipMulti());
    }

}
