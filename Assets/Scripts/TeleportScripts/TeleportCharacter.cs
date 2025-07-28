using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCharacter : MonoBehaviour
{
    public bool canUse;
    public Animator _thisAnimator,_otherAnimator;

    public SpriteRenderer _thisSprite;
    public Transform _other_Teleport,player;

    public Color[] colorSet;
    public TeleportCharacter other_teleport_character;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable() {
        canUse=true;
    }
    private void OnEnable() {
        
        _thisSprite.color=colorSet[Random.Range(0,5)];
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag=="Player"&&canUse&&_other_Teleport.gameObject.activeInHierarchy)
        {
            //teleport audio
            FindObjectOfType<ShieldIcon>().playerAudioSource.PlayOneShot(AudioManager.instance.teleportAudio,GameManager.instance.volume);
            canUse=false;
            other_teleport_character.canUse=false;
            player.position=_other_Teleport.position;
            _thisAnimator.SetBool("used",true);
            _otherAnimator.SetBool("used",true);
            StartCoroutine(revert());
        }
        
    }
    IEnumerator revert()
    {
        yield return new WaitForSeconds(2f);
        canUse=true;
        other_teleport_character.canUse=true;
        _thisAnimator.SetBool("used",false);
        _otherAnimator.SetBool("used",false);
    }
}
