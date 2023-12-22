using UnityEngine;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicTracks;
    private int currentTrackIndex = 0;
    [SerializeField] private TextMeshProUGUI musicTitleText;

    void Start()
    {
        // Assurez-vous que vous avez défini l'AudioSource dans l'inspecteur
        if (audioSource == null)
        {
            Debug.LogError("Veuillez définir l'AudioSource dans l'inspecteur.");
            return;
        }

        // Assurez-vous que vous avez ajouté des pistes audio dans l'inspecteur
        if (musicTracks.Length == 0)
        {
            Debug.LogError("Veuillez ajouter des pistes audio dans l'inspecteur.");
            return;
        }

        // Commencez par jouer la première piste audio
        PlayMusic(currentTrackIndex);
    }

    void Update()
    {
        // Exemple : Appuyez sur la barre d'espace pour basculer entre pause et lecture
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePlayPause();
        }

        // Exemple : Appuyez sur les touches fléchées pour changer de morceau
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextTrack();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousTrack();
        }
        UpdateMusicTitle();

        // Exemple : Utilisez les touches haut/bas pour ajuster le volume
        float volumeChange = Input.GetAxis("Vertical");
        if (volumeChange != 0f)
        {
            // Utilisez les touches haut/bas pour ajuster le volume
            if (volumeChange > 0f)
            {
                IncreaseVolume();
            }
            else
            {
                DecreaseVolume();
            }
        }
    }

    public void PlayMusic(int trackIndex)
    {
        // Vérifiez si l'index est valide
        if (trackIndex >= 0 && trackIndex < musicTracks.Length)
        {
            // Chargez et jouez la piste audio spécifiée
            audioSource.clip = musicTracks[trackIndex];
            audioSource.Play();
            currentTrackIndex = trackIndex;
        }
        UpdateMusicTitle();
    }

    public void TogglePlayPause()
    {
        // Basculez entre pause et lecture
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void NextTrack()
    {
        // Jouez la piste audio suivante dans la liste
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        PlayMusic(currentTrackIndex);
    }

    public void PreviousTrack()
    {
        // Jouez la piste audio précédente dans la liste
        currentTrackIndex = (currentTrackIndex - 1 + musicTracks.Length) % musicTracks.Length;
        PlayMusic(currentTrackIndex);
    }

    public void IncreaseVolume()
    {
        // Augmentez le volume
        audioSource.volume = Mathf.Clamp01(audioSource.volume + 0.1f);
    }

    public void DecreaseVolume()
    {
        // Diminuez le volume
        audioSource.volume = Mathf.Clamp01(audioSource.volume - 0.1f);
    }
    private void UpdateMusicTitle()
    {
        if (musicTitleText != null && musicTracks.Length > 0)
        {
            // Affichez le nom de la piste dans l'objet Text
            musicTitleText.text = "Actuellement: " + musicTracks[currentTrackIndex].name;
        }
    }
}
