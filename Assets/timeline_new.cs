using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using System.Linq;


public class timeline_new : MonoBehaviour
{
        public static timeline_new Instance;
        public GameObject lipsync_player;
        public AudioClip[] a_Audioarray;
        public List<AudioClip> a_AudioList;
        public float audio_time;
        public PlayableGraph graph;
        public PlayableDirector director;
        public bool intr_flag, expl_flag, activ_flag;
        public int count = 0;
        public TimelineAsset[] introduction_timelines;
        public audi_files_creator afc;
        public TrackAsset temp;
        public Load_audio_n_Play_storyBoard lapa;
        public AnimationClip loadnext, activity;
       
      
        public AudioClip temp_audio_clip;
        public bool avatar_flag;//if false then avatar if true then judge
        public bool currentAvater = true, is_pause = false;
        public float length_of_audio;
        public bool fromTimeLine = false;
        [SerializeField]
        public int AudioCount = 5;
        public bool isSecondobj = false;
    
    
        int end_index;

    public delegate void EventCalled(string EventName);
    public static event EventCalled OnEventCalled;

    //public delegate void LoadTimeLineJason(string question);
    //public static event LoadTimeLineJason OnLoadTimeLineJason;
    private void OnEnable()
        {
            audi_files_creator.OnTimeLineRun += startPlayingTimeLine;
        }
        private void OnDisable()
        {
            audi_files_creator.OnTimeLineRun -= startPlayingTimeLine;
        }

    public void Awake()
    {
        Instance = this;
        end_index = 0;
        afc = GameObject.Find("audio file creator").GetComponent<audi_files_creator>();
        lapa = GameObject.Find("audio file creator").GetComponent<Load_audio_n_Play_storyBoard>();
        lipsync_player = GameObject.Find("Player");
    }

    // Use this for initialization
    void Start()
        {
        
            if (Application.isEditor)
            {
                //StartCoroutine(wait_for_5());
                a_Audioarray = Resources.LoadAll<AudioClip>("Sounds");
                a_AudioList = a_Audioarray.ToList<AudioClip>();
            }
           
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        public void startPlayingTimeLine()
        {
            Debug.LogError("startPlayingTimeLine");
            if (!SceneManager.GetActiveScene().name.Equals("visual q type"))
                StartCoroutine(wait_for_5());
        }

    //  IEnumerator wait_for_5()
    //  {

    //      Debug.LogError("startPlayingTimeLine wait_for_5");
    //      yield return new WaitForSeconds(3);

    //  setflag();
    //  if (Application.isEditor)
    //  {
    //      a_Audioarray = Resources.LoadAll<AudioClip>("Sounds");
    //      a_AudioList = a_Audioarray.ToList<AudioClip>();
    //      foreach (AudioClip ac in a_AudioList)
    //      {
    //      //    Debug.Log("AudioClip Name: " + ac.name);
    //      }
    //      playSequentially();
    //  }
    //  else
    //  {
    //      playSequentially();
    //  }



    ////  play_as_per_choice("sequential", 0, afc.stry_data.Introduction.list.Count);

    //  }

    // new logic for incorporating traversal

    IEnumerator wait_for_5()
    {

        Debug.LogError("startPlayingTimeLine wait_for_5");
        yield return new WaitForSeconds(3);

        setflag();
        if (Application.isEditor)
        {
            if (UtilityArtifacts.backTraversal == true)
            {
                play_as_per_choice("context_based", UtilityArtifacts.loadStartingpoint, UtilityArtifacts.loadEndingpoint);
            }
            // for obj4
            //if (UtilityArtifacts.loading_pos == "Obj4_Lo1")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj10")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj14")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj15")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj12")
            //{
            //    play_as_per_choice("sequential", 20, afc.stry_data.Introduction.list.Count);
            //}
            //// for obj 5
            //else if (UtilityArtifacts.loading_pos == "Obj5_Lo1_from_obj6")
            //{
            //    play_as_per_choice("context_based", 6, 10);
            //}
            //// for obj 8
            //else if (UtilityArtifacts.loading_pos == "Obj8_Lo1_from_obj10")
            //{
            //    play_as_per_choice("sequential", 6, afc.stry_data.Introduction.list.Count);
            //}
            //// for obj 11
            //else if (UtilityArtifacts.loading_pos == "Obj11_Lo1_from_obj12")
            //{
            //    play_as_per_choice("context_based", 8, 15);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj11_Lo4_from_obj12")
            //{
            //    play_as_per_choice("sequential", 29, afc.stry_data.Introduction.list.Count);
            //}
            //// for obj 12

            //else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj13")
            //{
            //    play_as_per_choice("context_based", 3, 12);
            //}
            //// for obj 13

            //else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj14")
            //{
            //    play_as_per_choice("context_based", 3, 12);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj12_Lo2_from_obj14")
            //{
            //    play_as_per_choice("context_based", 12, 23);
            //}

            //// for obj 15
            //else if (UtilityArtifacts.loading_pos == "Obj15_Lo1")
            //{
            //    play_as_per_choice("context_based", 7, 17);
            //}
            else if (UtilityArtifacts.comingbackafterTraversal)
            {
                play_as_per_choice("sequential", UtilityArtifacts.loadStartingpointforcomingback, afc.stry_data.Introduction.list.Count);
                Debug.LogError("obj16");

            }
            else
            {
                playSequentially();
            }

        }
        //if (Application.isEditor)
        //{
        //    a_Audioarray = Resources.LoadAll<AudioClip>("Sounds");
        //    a_AudioList = a_Audioarray.ToList<AudioClip>();
        //    foreach (AudioClip ac in a_AudioList)
        //    {
        //        //    Debug.Log("AudioClip Name: " + ac.name);
        //    }
        //    // for obj4
        //    if (UtilityArtifacts.loading_pos == "Obj4_Lo1")
        //    {
        //        play_as_per_choice("context_based", 4, 11);
        //    }
        //    else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj10")
        //    {
        //        play_as_per_choice("context_based", 4, 11);
        //    }
        //    else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj14")
        //    {
        //        play_as_per_choice("context_based", 4, 11);
        //    }
        //    else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj15")
        //    {
        //        play_as_per_choice("context_based", 4, 11);
        //    }
        //    else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj12")
        //    {
        //        play_as_per_choice("sequential", 20, afc.stry_data.Introduction.list.Count);
        //    }
        //    // for obj 5
        //    else if (UtilityArtifacts.loading_pos == "Obj5_Lo1_from_obj6")
        //    {
        //        play_as_per_choice("context_based", 6, 10);
        //    }
        //    // for obj 8
        //    else if (UtilityArtifacts.loading_pos == "Obj8_Lo1_from_obj10")
        //    {
        //        play_as_per_choice("sequential", 6, afc.stry_data.Introduction.list.Count);
        //    }
        //    // for obj 11
        //    else if (UtilityArtifacts.loading_pos == "Obj11_Lo1_from_obj12")
        //    {
        //        play_as_per_choice("context_based", 8, 15);
        //    }
        //    else if (UtilityArtifacts.loading_pos == "Obj11_Lo4_from_obj12")
        //    {
        //        play_as_per_choice("sequential", 29, afc.stry_data.Introduction.list.Count);
        //    }
        //    // for obj 12

        //    else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj13")
        //    {
        //        play_as_per_choice("context_based", 3, 12);
        //    }
        //    // for obj 13

        //    else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj14")
        //    {
        //        play_as_per_choice("context_based", 3, 12);
        //    }
        //    else if (UtilityArtifacts.loading_pos == "Obj12_Lo2_from_obj14")
        //    {
        //        play_as_per_choice("context_based", 12, 23);
        //    }

        //    // for obj 15
        //    else if (UtilityArtifacts.loading_pos == "Obj15_Lo1")
        //    {
        //        play_as_per_choice("context_based", 7, 17);
        //    }
        //    else
        //    {
        //        Debug.LogError("obj16");
        //        if (UtilityArtifacts.coming_back_from == "quest1")
        //            play_as_per_choice("sequential", 5, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "quest2")
        //            play_as_per_choice("sequential", 11, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "quest3")
        //            play_as_per_choice("sequential", 17, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "quest4")
        //            play_as_per_choice("sequential", 21, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj6_quest1")
        //            play_as_per_choice("sequential", 21, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj15_quest1")
        //            play_as_per_choice("sequential", 4, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj15_quest2")
        //            play_as_per_choice("sequential", 5, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj15_quest3")
        //            play_as_per_choice("sequential", 6, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj11_quest1")
        //            play_as_per_choice("sequential", 6, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj11_quest2")
        //            play_as_per_choice("sequential", 7, afc.stry_data.Introduction.list.Count);
        //        else if (UtilityArtifacts.coming_back_from == "to_Obj11_quest3")
        //            play_as_per_choice("sequential", 8, afc.stry_data.Introduction.list.Count);
        //        else
        //            playSequentially();
        //    }

        //}
        else
        {
            if (UtilityArtifacts.backTraversal == true)
            {
                play_as_per_choice("context_based", UtilityArtifacts.loadStartingpoint, UtilityArtifacts.loadEndingpoint);
            }
            // for obj4
            //if (UtilityArtifacts.loading_pos == "Obj4_Lo1")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj10")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj14")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj15")
            //{
            //    play_as_per_choice("context_based", 4, 11);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj12")
            //{
            //    play_as_per_choice("sequential", 20, afc.stry_data.Introduction.list.Count);
            //}
            //// for obj 5
            //else if (UtilityArtifacts.loading_pos == "Obj5_Lo1_from_obj6")
            //{
            //    play_as_per_choice("context_based", 6, 10);
            //}
            //// for obj 8
            //else if (UtilityArtifacts.loading_pos == "Obj8_Lo1_from_obj10")
            //{
            //    play_as_per_choice("sequential", 6, afc.stry_data.Introduction.list.Count);
            //}
            //// for obj 11
            //else if (UtilityArtifacts.loading_pos == "Obj11_Lo1_from_obj12")
            //{
            //    play_as_per_choice("context_based", 8, 15);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj11_Lo4_from_obj12")
            //{
            //    play_as_per_choice("sequential", 29, afc.stry_data.Introduction.list.Count);
            //}
            //// for obj 12

            //else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj13")
            //{
            //    play_as_per_choice("context_based", 3, 12);
            //}
            //// for obj 13

            //else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj14")
            //{
            //    play_as_per_choice("context_based", 3, 12);
            //}
            //else if (UtilityArtifacts.loading_pos == "Obj12_Lo2_from_obj14")
            //{
            //    play_as_per_choice("context_based", 12, 23);
            //}

            //// for obj 15
            //else if (UtilityArtifacts.loading_pos == "Obj15_Lo1")
            //{
            //    play_as_per_choice("context_based", 7, 17);
            //}
            else if (UtilityArtifacts.comingbackafterTraversal)
            {
                play_as_per_choice("sequential", UtilityArtifacts.loadStartingpointforcomingback, afc.stry_data.Introduction.list.Count);
                Debug.LogError("obj16");
                
            }
            else
            {
                playSequentially();
            }
                
            }
       



        //  play_as_per_choice("sequential", 0, afc.stry_data.Introduction.list.Count);

    }


    public void playSequentially()
    {
        play_as_per_choice("sequential", 0, afc.stry_data.Introduction.list.Count);
    }

    public void playChoice()
    {
        play_as_per_choice("context_based", 2, 4);
    }
    //    public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {

    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {

    //    }
    //}

    public void play_as_per_choice(string sequnce_scenario, int start, int end)
    {
        Debug.LogError("play_as_per_choice: " + sequnce_scenario + " start: " + start + " end: " + end);
        switch (sequnce_scenario)
        {
            case "sequential":
                count = start;
                end_index = end;
                load_next();
                break;
            case "context_based":
                count = start;
                end_index = end;
                load_next();
                break;
            case "Test_First_Audio ":
                count = start;
                end_index = end;
                load_next();
                break;
        }
    }

    public void playAudioOnRelearn(string audiofile)
    {
        Debug.Log("playing Audio fron Scene: " + audiofile);
        StartCoroutine(playAudio(audiofile));
    }

    IEnumerator playAudio(string audiofile)
    {
        AudioClip temp_clip = null;
        //lapa.PlayAudioFile("", audiofile, temp_clip, "avatar");
        lapa.load_sound_rePlay("", audiofile, temp_clip);
        yield return new WaitForSeconds(0.5f);
        length_of_audio = lapa.length_of_audio;
    }
    public float getCurrentAudioLength()
    {
        return lipsync_player.GetComponent<AudioSource>().clip.length;
    }
   
    public void pauseAudio()
        {
            lipsync_player.GetComponent<AudioSource>().Pause();
        }
    public void stopAudio()
    {
        lipsync_player.GetComponent<AudioSource>().Stop();
    }
        public void resumeAudio()
        {
            lipsync_player.GetComponent<AudioSource>().UnPause();
        }

        public void pause_app()
        {

            if (lipsync_player.GetComponent<AudioSource>().isPlaying)
            {
               
                director.Pause();
                is_pause = true;
                lipsync_player.GetComponent<AudioSource>().Pause();
            }
           
            Time.timeScale = 0.0f;

        }

        public void resume_app()
        {
            
            Time.timeScale = 1.0f;
          
            director.Resume();
            is_pause = false;
            lipsync_player.GetComponent<AudioSource>().UnPause();
           

        }

    public void SkipApp()
    {
        Time.timeScale = 1.0f;
        is_pause = false;
        director.Stop();
        count = end_index;
        lipsync_player.GetComponent<AudioSource>().Stop();
    }
        // new pause

        public void pause_new()
        {
            lipsync_player.GetComponent<AudioSource>().Pause();
           
            Time.timeScale = 0.0f;
        }

        public void pause_play()
        {
            Time.timeScale = 1.0f;
            lipsync_player.GetComponent<AudioSource>().UnPause();
           

        }

        public void createandplay()
        {
          //  Debug.Log(afc.stry_data.Explanation.flag);
           // Debug.Log(afc.stry_data.Activity.flag);

            if (afc.stry_data.Introduction.flag.Equals("true"))
            {
                intr_flag = true;
                expl_flag = false;
                activ_flag = false;
            }
            else if (afc.stry_data.Explanation.flag.Equals("true"))
            {
                intr_flag = false;
                expl_flag = true;
                activ_flag = false;
            }
            else if (afc.stry_data.Activity.flag.Equals("true"))
            {
                intr_flag = false;
                expl_flag = false;
                activ_flag = true;
            }
            else
            {
                
                Debug.Log("Loading Canvas.................. createandplay");
    
            }
            count = 0;

        }
    
        public void setflag()
        {
           
            createandplay();
        }
    
        public void load_next()
        {
            if (end_index == count)
            {
                Debug.Log("Done playing");
            }
            else
            {
                StartCoroutine(Loaddata());
            }
        }
          
        public IEnumerator Loaddata()
        {

           
            if (gameObject.GetComponent<PlayableDirector>() == null)
            {
                director = gameObject.AddComponent<PlayableDirector>();
            }
            if (director == null)
            {
                director = gameObject.GetComponent<PlayableDirector>();
                director.playOnAwake = false;
                director.timeUpdateMode = DirectorUpdateMode.GameTime;
                graph = PlayableGraph.Create();

            }

            director.Stop();
            director.time = 0;
            lipsync_player.GetComponent<AudioSource>().Stop();
            lipsync_player.GetComponent<AudioSource>().time = 0;
      
            yield return StartCoroutine(create_timelines(introduction_timelines, director, gameObject));

            var timelineAsset = director.playableAsset as TimelineAsset;
            director.time = 0;
            director.Play();
            lipsync_player.GetComponent<AudioSource>().time = 0;
            lipsync_player.GetComponent<AudioSource>().Play();
            count = count + 1;
            yield return null;
        }

        int getClipIndex(string clipName)
        {
            //Debug.Log("getClipIndex: " + clipName);
            int i = 0;
           
            foreach (AudioClip ac in a_AudioList)
            {
            if (string.Equals(ac.name, clipName))
                {
                    return i;
                }

                i++;
            }
            return -5;
        }
    
        public IEnumerator create_timelines(TimelineAsset[] timeline_data, PlayableDirector plybldirector, GameObject gameObject)
        {
        lapa.length_of_audio = 0;
        AudioClip testclip = null;

            if (afc.stry_data != null)
            {

                if (intr_flag == true)
                {
                if (count < end_index)
                {
                    //Debug.Log("introduction timeline start");
                    TimelineAsset timeline = new TimelineAsset();
                    timeline.name = "sss" + count;
                    AnimationTrack ani = timeline.CreateTrack<AnimationTrack>(temp, "animator");

                    AnimationTrack loadnext_anim = timeline.CreateTrack<AnimationTrack>(temp, "animator");
                    AudioClip temp_clip = null; ;
                    //do it diffrebt for editor get get audio from array and set length. assign it to lipsync. assign length to start assaging avar or  judge according to story board 
                    // 

                    if (Application.isEditor)
                    {
                        //loading audio in editor
                        int index = getClipIndex(afc.stry_data.Introduction.list[count].audio.Substring(0, afc.stry_data.Introduction.list[count].audio.Length - 4));
                        if (index < 0)
                        {
                            Debug.Log("file not found");
                        }
                        else
                        {
                            testclip = a_AudioList[index];
                            Debug.Log("file found"+testclip.name);
                            currentAvater = true;
                            lipsync_player.GetComponent<AudioSource>().clip = testclip;
                        }

                    }
                    else
                    { 
                        //loading audio in device
                        lapa.PlayAudioFile("", afc.stry_data.Introduction.list[count].audio, temp_clip, afc.stry_data.Introduction.list[count].speaker);
                    }

                    yield return new WaitForSeconds(0.3f);
                    if (afc.stry_data.Introduction.list[count].events.Equals("load_next.anim"))
                    {
                        var f = loadnext_anim.CreateClip(loadnext);
                        if (Application.isEditor)
                            f.start = testclip.length;
                        else
                            f.start = lapa.length_of_audio; 
                    }
                    else
                    {
                        var f = loadnext_anim.CreateClip(activity);
                    }

                  
                    plybldirector.SetGenericBinding(ani, gameObject);
                    plybldirector.SetGenericBinding(loadnext_anim, gameObject);
                    plybldirector.playableAsset = timeline;
                    director.playableAsset = timeline;
                    plybldirector.extrapolationMode = DirectorWrapMode.Hold;

                    plybldirector.Evaluate();
                    callto_function(afc.stry_data.Introduction.list[count].artifacts);
                    
                }
                else
                {
                    Debug.Log("either ended the playing timelin or your timeline is empty");
                }
                    
                }
                
            }
            yield return new WaitForSeconds(0.8f);

        }
     
      
       
    
        public void callto_function(string function_name)
        {
            //Debug.LogError("callto_function: " + function_name);
            if (OnEventCalled != null)
            {
                OnEventCalled(function_name);
            }
        }
    
    }



