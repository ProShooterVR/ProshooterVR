using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public static InstructionManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public AudioSource audioSource;
    public AudioClip[] instuctionsAudioList;
    public AudioClip[] gameInstruction;
    public AudioClip buzzer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float playInstruction(int no)
    {
        audioSource.PlayOneShot(instuctionsAudioList[no]);
        return instuctionsAudioList[no].length;
    }
    public float playInGameSounds(int no)
    {
        audioSource.PlayOneShot(gameInstruction[no]);
        return instuctionsAudioList[no].length;
    }


}
