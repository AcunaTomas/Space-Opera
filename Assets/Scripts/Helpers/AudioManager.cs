using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager INSTANCE;

    [SerializeField]
    private AudioSource _audioUI;
    [SerializeField]
    private AudioSource _audioPlayer;
    [SerializeField]
    private AudioSource _audioPlayerFall;
    [SerializeField]
    private AudioSource _meleePlayer;
    [SerializeField]
    private AudioSource _bombPlayer;
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _audioAlarm;
    [SerializeField]
    private AudioSource _audioInteractor;
    [SerializeField]
    private AudioSource _audioElevator;
    [SerializeField]
    private AudioSource _audioDoor;
    [SerializeField]
    private AudioSource _audioEnemyAttack;
    [SerializeField]
    private AudioSource _audioEnemyHit;
    [SerializeField]
    private AudioClip _buttonConfirm;
    [SerializeField]
    private AudioClip _musicIntro;
    [SerializeField]
    private AudioClip _elevatorInteractor;
    [SerializeField]
    private AudioClip _elevatorSound;
    [SerializeField]
    private AudioClip _elevatorStop;
    [SerializeField]
    private AudioClip _pinchosSound;
    [SerializeField]
    private AudioClip _ambientOutside;
    [SerializeField]
    private AudioClip _ambientInside;
    [SerializeField]
    private AudioClip _quilomboMusic;
    [SerializeField]
    private AudioClip _fallPlayer;
    [SerializeField]
    private AudioClip _doorOpen;
    [SerializeField]
    private AudioClip _doorClose;
    [SerializeField]
    private AudioClip _alarm;
    [SerializeField]
    private AudioClip _enemyAttack;
    [SerializeField]
    private AudioClip _enemyHit;
    [SerializeField]
    private AudioClip _interactDialogue;
    [SerializeField]
    private AudioClip _playerJump;
    [SerializeField]
    private AudioClip _playerHit;
    [SerializeField]
    private AudioClip[] _pasoslvl1;
    private int _lastSound;
    private List<int> _numbers; 
    [SerializeField]
    private AudioClip[] _attackMelee;
    private int _meleeNum = 0;
    [SerializeField]
    private AudioClip _attackBomb;


    private void Awake()
    {
        INSTANCE = this;
    }
    private void Start()
    {
        _numbers = new List<int>();
        for (int x = 0 ; x < _pasoslvl1.Length; x++)
        {
            _numbers.Add(x);
        }
    }
    //ALL_SOUNDS_NOT_LOOPED
    public void PlayUI()
    {
        _audioUI.clip = _buttonConfirm;
        _audioUI.Play();
    }

    public void PlayPlayer()
    {
        int _random = Random.Range(0,_numbers.Count);
        int _number = _numbers[_random];

        _audioPlayer.clip = _pasoslvl1[_number];
        _audioPlayer.Play();
        
        if (_numbers.Count != _pasoslvl1.Length)
        {
            _numbers.Add(_lastSound);
        }
        _numbers.Remove(_number);
        _lastSound = _number;
    }

    public void PlayPlayerHit()
    {
        _audioPlayerFall.volume = 0.3f;
        _audioPlayerFall.clip = _playerHit;
        _audioPlayerFall.Play();
    }

    public void PlayPlayerJump()
    {
        _audioPlayerFall.volume = 0.15f;
        _audioPlayerFall.clip = _playerJump;
        _audioPlayerFall.Play();
    }

    public void PlayPlayerFall()
    {
        _audioPlayerFall.volume = 0.1f;
        _audioPlayerFall.clip = _fallPlayer;
        _audioPlayerFall.Play();
    }

    public void PlayPlayerMelee()
    { 
        _meleePlayer.clip = _attackMelee[_meleeNum];
        _meleePlayer.Play();
        _meleeNum++;
        if (_meleeNum == _attackMelee.Length)
        {
            _meleeNum = 0;
        }
    }

    public void PlayPlayerBomb()
    {
        _bombPlayer.clip = _attackBomb;
        _bombPlayer.Play();
    }

    public void PlayElevatorInteractor()
    {
        _audioInteractor.clip = _elevatorInteractor;
        _audioInteractor.Play();
    }

    public void PlayDialogueInteractor()
    {
        _audioInteractor.clip = _interactDialogue;
        _audioInteractor.Play();
    }

    public void PlayDoorOpen()
    {
        _audioDoor.clip = _doorOpen;
        _audioDoor.Play();
    }

    public void PlayDoorClose()
    {
        _audioDoor.clip = _doorClose;
        _audioDoor.Play();
    }

    public void PlayEnemyHit()
    {
        _audioEnemyHit.clip = _enemyHit;
        _audioEnemyHit.Play();
    }

    public void PlayEnemyAttack()
    {
        _audioEnemyAttack.clip = _enemyAttack;
        _audioEnemyAttack.Play();
    }

        public void PlayPinchos()
    {
        _audioElevator.clip = _pinchosSound;
        _audioElevator.Play();
    }
    //ALL_MUSIC OR LOOPED_SOUNDS
    public void PlayMusic()
    {
        _musicSource.clip = _musicIntro;
        _musicSource.Play();
    }

    public void PlayAlarm()
    {
        _audioAlarm.clip = _alarm;
        _audioAlarm.Play();
    }

    public void PlayQuilombo()
    {

        _musicSource.clip = _quilomboMusic;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }

    public void PlayAmbientOutsideLVL1()
    {
        _musicSource.clip = _ambientOutside;
        _musicSource.Play();
    }
    public void PlayAmbientInsideLVL1()
    {
        _musicSource.clip = _ambientInside;
        _musicSource.Play();
    }



    public void PlayElevator()
    {
        _audioElevator.loop = true;
        _audioElevator.clip = _elevatorSound;
        _audioElevator.Play();
    }
    public void StopElevator()
    {
        _audioElevator.loop = false;
        _audioElevator.Stop();
        _audioElevator.clip = _elevatorStop;
        _audioElevator.Play();
    }
}
