# Animation Sequencer

<p align="center">
    <a href="https://github.com/brunomikoski/Animation-Sequencer/blob/master/LICENSE.md">
		<img alt="GitHub license" src ="https://img.shields.io/github/license/Thundernerd/Unity3D-PackageManagerModules" />
	</a>

</p> 
<p align="center">
	<a href="https://www.codacy.com/gh/brunomikoski/Animation-Sequencer/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=brunomikoski/Animation-Sequencer&amp;utm_campaign=Badge_Grade"><img src="https://app.codacy.com/project/badge/Grade/ab4c5923ca0545c9b8c85d87adbd689a"/></a>
    <a href="https://openupm.com/packages/com.brunomikoski.animationsequencer/">
        <img src="https://img.shields.io/npm/v/com.brunomikoski.animationsequencer?label=openupm&amp;registry_uri=https://package.openupm.com" />
    </a>

  <a href="https://github.com/brunomikoski/Animation-Sequencer/issues">
     <img alt="GitHub issues" src ="https://img.shields.io/github/issues/brunomikoski/Animation-Sequencer" />
  </a>

  <a href="https://github.com/brunomikoski/Animation-Sequencer/pulls">
   <img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr/brunomikoski/Animation-Sequencer" />
  </a>
  
  <img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/brunomikoski/Animation-Sequencer" />
</p>

<p align="center">
    	<a href="https://github.com/brunomikoski">
        	<img alt="GitHub followers" src="https://img.shields.io/github/followers/brunomikoski?style=social">
	</a>	
	<a href="https://twitter.com/brunomikoski">
		<img alt="Twitter Follow" src="https://img.shields.io/twitter/follow/brunomikoski?style=social">
	</a>
</p>

<p align="center">
  <img alt="Example" src="https://user-images.githubusercontent.com/600419/109826506-c299cb00-7c32-11eb-8b0d-8c0e97c4b5b7.gif">
</p>

I LOVE Tween, I love DOTween even more! But having to wait for a recompilation every time you tweak a single value on some animation it's frustrating! Even more complicated is properly have to visualize the entire animation on your head and having to wait until you reach your animation to see what you have done! That's why I created the Animation Sequencer, it is (~~cloned~~) **HEAVILY INSPIRED** from [Space Ape](https://spaceapegames.com/) amazing [Creative Engineering: Balancing & Juicing with Animations](https://youtu.be/4JoBw212Kyg) presentation.

*This is still in heavy development, please use it carefully*

## Features
- Allow you to create a complex sequence of Tweens/Actions and play on Editor Mode!
- User Friendly interface with a lot of customization
- Easy to extend with project specific actions
- Chain sequences and control entire animated windows with a single interface
- Searchable actions allowing fast interactions and updates
- Can be used for any type of Objects, UI or anything you want! 

## Built in Steps
 - Tween Target 
    - DOAnchoredPosition
    - DOMove
    - DOScale
    - DORotate
    - DOFade (Canvas Group)
    - DOFade (Graphic)
    - DOPath
    - DOShake (Position/Rotation/Scale)
    - DOPunch (Position/Rotation/Scale)
    - DOText (TextMeshPro Support)
    - DOFill  
 - Play Particle System
 - Play Animation Sequencer

## How to use?
- Animation Sequencer rely on DOTween for now, so it a requirement that you have `DOTween` on your project with properly created `asmdef` for it (Created by the `DOTween` setup panel)
- Add the Animation Sequencer to any GameObject and start your animation! 
- Using the <kbd>+</kbd> button under the `Animation Steps` you can add a new step
- Select <kbd>Tween Target</kbd>
- Use the <kbd>Add Actions</kbd> to add specific tweens to your target
- Press play on the Preview bar to view it on Editor Time.
- To play it by code, just call use `animationSequencer.Play();`

## FAQ

<details>
    
<summary>How can I create my custom DOTween actions?</summary> 
Lets say you want to create a new action to play a specific sound from your sound manager on the game, you just need to extend the `AnimationStepBase`

```c#
[Serializable]
public class PlayAudioClipAnimationStep : AnimationStepBase
{
    [SerializeField]
    private AudioClip audioClip;

    //Duration of this step, in this case will return the length of the clip.
    public override float Duration => audioClip.length;
    //This is the name that will be displayed on the + button on the Animation Sequencer
    public override string DisplayName => "Play Audio Clip";

    //Here is actually the action of this step
    public override void Play()
    {
        base.Play();
        AudioManager.Play(audioClip);
    }
}
```

</details>

<details>

<summary>I have my own DOTween extensions, can I use that? </summary>

Absolutely! The same as the step, you can add any new DOTween action by extending `DOTweenActionBase`. In order to avoid any performance issues all the tweens are created on the PrepareToPlay method on Awake, and are paused.

```c#
[Serializable]
public sealed class ChangeMaterialStrengthDOTweenAction : DOTweenActionBase
{
    public override string DisplayName => "Change Material Strength";
        
    public override Type TargetComponentType => typeof(Renderer);

    [SerializeField, Range(0,1)]
    private float materialStrength = 1;

     public override bool CreateTween(GameObject target, float duration, int loops, LoopType loopType)
     {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer == null)
            return false;

        TweenerCore<float, float, FloatOptions> materialTween = renderer.sharedMaterial.DOFloat(materialStrength, "Strength", duration);
        
        SetTween(materialTween, loops, loopType);
        return true;
    }
}
```

![custom-tween-action](https://user-images.githubusercontent.com/600419/109774425-3965a280-7bf8-11eb-9bfe-90b0be8b8617.gif)

</details>

<details>
    <summary>Using custom animation curve as easing </summary>
    
You can use the Custom ease to define an *AnimationCurve* for the Tween.
    
![custom-ease](https://user-images.githubusercontent.com/600419/109780020-7af94c00-7bfe-11eb-8f0f-52480dd97ea3.gif)

</details>

<details>
   <summary>What are the differences between the initialization settings</summary>
	
- <kbd>None</kbd> *Don't do anything on the AnimationSequencer Awake method*	
- <kbd>PrepareToPlayOnAwake</kbd> *This will make sure the Tweens that are from are prepared to play at the intial value on Awake.*
- <kbd>PlayOnAwake</kbd> Will play the tween on Awake.*
   
</details>

## System Requirements
Unity 2018.4.0 or later versions


## How to install

<details>
<summary>Add from OpenUPM <em>| via scoped registry, recommended</em></summary>

This package is available on OpenUPM: https://openupm.com/packages/com.brunomikoski.animationsequencer

To add it the package to your project:

- open `Edit/Project Settings/Package Manager`
- add a new Scoped Registry:
  ```
  Name: OpenUPM
  URL:  https://package.openupm.com/
  Scope(s): com.brunomikoski
  ```
- click <kbd>Save</kbd>
- open Package Manager
- click <kbd>+</kbd>
- select <kbd>Add from Git URL</kbd>
- paste `com.brunomikoski.animationsequencer`
- click <kbd>Add</kbd>
</details>

<details>
<summary>Add from GitHub | <em>not recommended, no updates :( </em></summary>

You can also add it directly from GitHub on Unity 2019.4+. Note that you won't be able to receive updates through Package Manager this way, you'll have to update manually.

- open Package Manager
- click <kbd>+</kbd>
- select <kbd>Add from Git URL</kbd>
- paste `https://github.com/brunomikoski/Animation-Sequencer.git`
- click <kbd>Add</kbd>
</details>


