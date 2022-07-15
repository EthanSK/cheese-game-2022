using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETGgames.Extensions
{
    public static class AudioSourceExtensions
    {

        //not a thorough copy, but good enough for our cases, and doesn't need reflection. can add more fields as needed
        public static void CopyFieldsTo(this AudioSource sourceAudioSource, AudioSource targetAudioSource)
        {
            var t = targetAudioSource;
            var s = sourceAudioSource;

            t.bypassEffects = s.bypassEffects;
            t.bypassReverbZones = s.bypassReverbZones;
            t.bypassListenerEffects = s.bypassListenerEffects;
            t.volume = s.volume;
            t.dopplerLevel = s.dopplerLevel;
            t.pitch = s.pitch;
            t.loop = s.loop;
            t.priority = s.priority;
            t.mute = s.mute;
            t.panStereo = s.panStereo;
            t.spatialBlend = s.spatialBlend;
            t.reverbZoneMix = s.reverbZoneMix;
            t.spread = s.spread;
            t.rolloffMode = s.rolloffMode;
            t.minDistance = s.minDistance;
            t.maxDistance = s.maxDistance;
        }
    }
}

