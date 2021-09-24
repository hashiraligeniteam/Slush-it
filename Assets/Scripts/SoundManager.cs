using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public AudioClip FillClip, DummyKillClip, ButtonClip, WinClip, FailClip, SwishClip;
   public AudioSource mySource;
   
   public void FillSound()
   {
      if(FillClip)
         mySource.PlayOneShot(FillClip);
      HapptinManager.instance.LowVibrate();
   }


   public void SwishSound()
   {
      if(SwishClip)
         mySource.PlayOneShot(SwishClip);
      HapptinManager.instance.LowVibrate();
   }


   public void KillSound()
   {
      if(DummyKillClip)
         mySource.PlayOneShot(DummyKillClip);
      HapptinManager.instance.HighVibrate();
   }


   public void ButtonSound()
   {
      if(ButtonClip)
         mySource.PlayOneShot(ButtonClip);
      HapptinManager.instance.MediumVibrate();
   }


   public void WinSound()
   {
      if(WinClip)
         mySource.PlayOneShot(WinClip);
   }

   public void FailSound()
   {
      if(FailClip)
         mySource.PlayOneShot(FailClip);
   }

}
