using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SkipTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;  // Reference to the Playable Director
    public KeyCode skipKey = KeyCode.Space;    // Key to skip the Timeline, default is Space
    public Button skipButton;                  // Optional UI Button to skip

    private List<TimelineClip> timelineClips;  // Store all clips from the Timeline

    private void Start()
    {
        // Ensure Playable Director is set
        if (playableDirector == null)
            playableDirector = GetComponent<PlayableDirector>();

        // Gather all timeline clips on start
        GatherTimelineClips();

        // Attach the skip function to the button, if a button is assigned
        if (skipButton != null)
            skipButton.onClick.AddListener(SkipToNextClipStart);
    }

    private void Update()
    {
        // Check if the skip key is pressed
        if (Input.GetKeyDown(skipKey))
        {
            SkipToNextClipStart();
        }
    }

    private void GatherTimelineClips()
    {
        // Ensure PlayableDirector has a TimelineAsset
        if (playableDirector.playableAsset is TimelineAsset timelineAsset)
        {
            // Retrieve all clips in the Timeline asset, across all tracks
            timelineClips = timelineAsset.GetOutputTracks()
                .SelectMany(track => track.GetClips())
                .OrderBy(clip => clip.start) // Sort clips by start time
                .ToList();
        }
    }

    public void SkipToNextClipStart()  // Made public for button access
    {
        if (timelineClips == null || timelineClips.Count == 0)
            return;

        // Find the next clip's start time based on the current playback time
        double currentTime = playableDirector.time;
        TimelineClip nextClip = timelineClips.FirstOrDefault(clip => clip.start > currentTime);

        if (nextClip != null)
        {
            // Move the PlayableDirector's time to the start of the next clip
            playableDirector.time = nextClip.start;
            playableDirector.Evaluate(); // Evaluate the new time to update immediately
        }
        else
        {
            // Optional: Stop if there are no more clips to skip to
            playableDirector.time = playableDirector.duration;
            playableDirector.Stop();
        }
    }
}
