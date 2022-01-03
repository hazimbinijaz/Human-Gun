//Shady
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    [Title("AUDIO MANAGER", "SINGLETON", titleAlignment: TitleAlignments.Centered)]
    [SerializeField] AudioSource BGSource  = null;
    [SerializeField] AudioSource SFXSource = null;

    [FoldoutGroup("Background Music")]
    [SerializeField] AudioClip BGMusic = null;

    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip ClickClip       = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip GameWinClip     = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip GameLoseClip    = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip ItemCollectClip = null;
    [FoldoutGroup("SFX Clips")]
    [SerializeField] AudioClip HurdleClip      = null;
    
    private void Awake()
    {
        if(!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }//Awake() end

    public void StartGame()
    {
        if(BGSource.isPlaying)
            return;
        BGSource.clip = BGMusic;
        BGSource.loop = true;
        BGSource.Play();
    }//StartGame() end

    public void GameEnd() => BGSource.Stop();

    public void ButtonClick() => SFXSource.PlayOneShot(ClickClip);

    public void GameWin() => SFXSource.PlayOneShot(GameWinClip);

    public void GameLose() => SFXSource.PlayOneShot(GameLoseClip);

    public void ItemCollected() => SFXSource.PlayOneShot(ItemCollectClip);

    public void HurdleHit() => SFXSource.PlayOneShot(HurdleClip);

}//class end