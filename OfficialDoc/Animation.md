## Animation Clips ##
### Animation from External Sources ###
#### 导入 ####
##### 格式 #####
- 导出格式：FBX 、OBJ
- 原生格式：Max、Blend

> 一般使用FBX格式，用FBX Review软件可以查看

##### Model Tab #####
![](http://docs.unity3d.com/uploads/Main/MecanimImporterModelTab.png)

> Note that Unity’s Physics Engine is scaled as 1 unit equals 1 metre. It is important that if you want to have correct physical behaviour you should have the model correctly scaled in the original modeling application.

> Flat Shading效果：Normals设置为Calculate，Smoothing Angle设置为0

##### Rig Tab #####
![](http://docs.unity3d.com/uploads/Main/MecanimImporterRigTab.png)

##### Animations Tab #####
![](http://docs.unity3d.com/uploads/Main/MecanimImporterAnimationsTab.png)

选择其中一个Clip后

![](http://docs.unity3d.com/uploads/Main/MecanimAnimationClipInspector.png)

> Bake Into Pose和Apply Root Motion配合使用


#### 人形动画 ####
##### 创建Avatar #####
大多数情况会自动完成，可以看到有个小对勾

![](http://docs.unity3d.com/uploads/Main/MecanimImporterRigTab.png)

成功之后，Avatar子资源会出现

![](http://docs.unity3d.com/uploads/Main/MecanimFBXNoAvatar.png)

##### 配置Avatar #####
![](http://docs.unity3d.com/uploads/Main/MecanimAvatarMappingValid.png)

自动映射失败后可以用下面功能，也可以手动拖动映射

![](http://docs.unity3d.com/uploads/Main/MecanimMappingMenus.png)

![](http://docs.unity3d.com/uploads/Main/MecanimPoseMenus.png)

##### Avatar Mask #####
可以单独建立Avatar Mask资源，或者在导入动画时设置一次性的Mask

![](http://docs.unity3d.com/uploads/Main/AvatarMaskInspectorHumanoid.png)

todo::Transform在人形上冒似不太好用，一般用于无法使用Humanoid的骨骼动画，bug?

![](http://docs.unity3d.com/uploads/Main/AvatarMaskInspectorTransform.png)

> 也可以在运行时设置，但是一般在导入时就设置好，这样可以节省资源

##### Muscle setup #####
![](http://docs.unity3d.com/uploads/Main/MecanimAvatarMuscles.png)




#### Asset Preparation and Import ####
![](http://docs.unity3d.com/uploads/Main/Char200.png)

步骤：modeling, rigging, skinning

##### Modelling #####
- Observe a sensible topology. 
- Be mindful of the scale of your mesh. Do a test import
- Arrange the mesh so that the character’s feet are standing on the local origin or “anchor point” of the model.
- Model in a T-pose if you can. 
- Clean up your model.

![](http://docs.unity3d.com/uploads/Main/SkinMesh256.png)

##### Rigging #####
臀部应该是骨骼的根元素，至少需要15块骨头

    * HIPS - spine - chest - shoulders - arm - forearm - hand
    * HIPS - spine - chest - neck - head
    * HIPS - UpLeg - Leg - foot - toe - toe_end

![](http://docs.unity3d.com/uploads/Main/Skeleton256.png)

##### Skinning #####
- Using an automated process initially to set up some of the skinning
- Creating a simple animation for your rig or importing some animation data to act as a test for the skinning.
- Incrementally editing and refining your skinning solution.
- Sticking to a maximum of four influences when using a soft bind, since this is the maximum number that Unity will handle.

![](http://docs.unity3d.com/uploads/Main/Skinning256.png)



#### 通用动画 ####
![](http://docs.unity3d.com/uploads/Main/MecanimImportRigGeneric.png)

设置Root node

#### 分拆动画 ####
##### 提前分拆好的动画 #####
![](http://docs.unity3d.com/uploads/Main/MecanimImportPreSplitAnimation.png)

##### 没有提前分拆好的动画 #####
使用+号添加，并指定Start和End帧

![](http://docs.unity3d.com/uploads/Main/MecanimImportAnimationSplitting.png)

##### 从多个文件导入 #####
按照这种格式：modelName@animationName.fbx

![](http://docs.unity3d.com/uploads/Main/animation_at_naming.png)

#### 循环动画 ####
![](http://docs.unity3d.com/uploads/Main/MecanimAnimClipLoopingGreen.png)

#### 动画曲线 ####
![](http://docs.unity3d.com/uploads/Main/MecanimCurves.png)

可以建立自己的曲线库

![](http://docs.unity3d.com/uploads/Main/CurveEditorPopupDescr.png)

> 如果曲线的名字和Animator Controller的parameter名字相同，那么parameter的值就会用曲线上的值

#### 动画事件 ####
可以确定在何时播放行走动画时的脚步声

![](http://docs.unity3d.com/uploads/Main/AnimationInspectorEventsSection.png)

使用此动画的对象的附加script中，需要有个和事件名同名的方法，参数从下图中的类型中最多选择一个，或者没有 

![](http://docs.unity3d.com/uploads/Main/AnimationInspectorEventsSectionDialog.png)

    void Footstep(int a) {
        Debug.Log("Footstep " + a);
    }

#### 选择根运动节点 ####
![](http://docs.unity3d.com/uploads/Main/AnimationInspectorRootNodeSelectionMenu.png)

#### Euler Curve Import ####
todo::旋转quaternion file:///C:/Program%20Files/Unity/Editor/Data/Documentation/en/Manual/QuaternionAndEulerRotationsInUnity.html

勾选上，如果原动画是euler旋转，就会被转换为quaternion旋转，但是可能会失真严重

> Any rotation curve on a joint that has pre- or post-rotations will be automatically resampled by the FBX SDK, and will automatically be imported as quaternion curves.

![](http://docs.unity3d.com/uploads/Main/AnimationImportResampleRotations.png)

euler的旋转XYZ顺序可能会不同

![](http://docs.unity3d.com/uploads/Main/AnimationEulerAlternateRotationOrderInInspector.png)




### Animation View Guide ###
#### 动画视图 ####
##### 原理 #####
![](http://docs.unity3d.com/uploads/Main/AnimationNewClipAutoSetup.png)

##### 创建动画 #####
![](http://docs.unity3d.com/uploads/Main/AnimationEditorNewClip.png)

添加曲线

![](http://docs.unity3d.com/uploads/Main/AnimationEditorAddCurves.png)

##### 时间线 #####

1:30代表1秒加30帧

![](http://docs.unity3d.com/uploads/Main/AnimationEditorTimeLine.png)

- ,：上一帧
- .：下一帧
- Alt + ,：上一个关键帧
- Alt + .：下一个关键帧

#### 动画曲线 ####
##### 属性类型 #####
- Float
- Color
- Vector2
- Vector3
- Vector4
- Quaternion
- Boolean

> 不支持Arrays、structs、objects

##### 差值旋转类型 #####
![](http://docs.unity3d.com/uploads/Main/AnimationEditorQuaternionInterpolationMenu.png)

##### 编辑切线 #####
![](http://docs.unity3d.com/uploads/Main/AnimationCurveTangentTypes.png)

> 按F可以放大关键点
> 
> 按鼠标中键或Alt+鼠标左键可以滑动视角

#### 动画事件 ####
![](http://docs.unity3d.com/uploads/Main/AnimationEditorEventLine.png)

参数可以没有，或者是一个float, string, int, object或AnimationEvent。

AnimationEvent可以包含float, string, integer,object

	void SayHello(AnimationEvent myevent){
		Debug.Log ("Hello " + myevent.stringParameter + " age " + myevent.intParameter);
	}

![](http://docs.unity3d.com/uploads/Main/AnimationEditorEventPopup.png)







## Animator Controllers ##
### Animator窗口 ###
点击眼睛图标可以隐藏或显示左边栏

![](http://docs.unity3d.com/uploads/Main/AnimatorWindowEyeIcon.png)

面包屑导航

![](http://docs.unity3d.com/uploads/Main/AnimatorWindowBreadcrumbLocation.png)

锁定当前显示

![](http://docs.unity3d.com/uploads/Main/AnimatorWindowLockIcon.png)

### Animation State Machines ###
#### Animation Parameters ####
![](http://docs.unity3d.com/uploads/Main/AnimationEditorParametersSection.png)

    using UnityEngine;
    using System.Collections;

    public class SimplePlayer : MonoBehaviour {
        
        Animator animator;
        
        // Use this for initialization
        void Start () {
            animator = GetComponent<Animator>();
        }
        
        // Update is called once per frame
        void Update () {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool fire = Input.GetButtonDown("Fire1");

            animator.SetFloat("Forward",v);
            animator.SetFloat("Strafe",h);
            animator.SetBool("Fire", fire);
        }

        void OnCollisionEnter(Collision col) {
            if (col.gameObject.CompareTag("Enemy"))
            {
                animator.SetTrigger("Die");
            }
        }
    }


#### State Machine Behaviours ####
![](http://docs.unity3d.com/uploads/Main/StateMachineBehaviourAttached.png)

    using UnityEngine;

    [SharedBetweenAnimators]    //可以将AttackBehaviour单例化，注意改变成员变量的值也会影响其他使用此脚本的物体 todo::试验下
    public class AttackBehaviour : StateMachineBehaviour
    {
        public GameObject particle;
        public float radius;
        public float power;
        
        protected GameObject clone;
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            clone = Instantiate(particle, animator.rootPosition, Quaternion.identity) as GameObject;
            var rb = clone.GetComponent<Rigidbody>();
            rb.AddExplosionForce(power, animator.rootPosition, radius, 3.0f);
        }
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Destroy(clone);
        }
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("On Attack Update ");
        }
        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("On Attack Move ");
        }
        override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("On Attack IK ");
        }
    }


#### Sub-State Machines ####
![](http://docs.unity3d.com/uploads/Main/MecanimSelectSubState.png)

子状态机可以看成一个文件夹，状态与状态、状态与状态机、状态机与状态机之间可以互相转换


#### Animation Layers ####
##### 基本 #####
Blending type: Override means information from other layers will be ignored, while Additive means that the animation will be added on top of previous layers.

![](http://docs.unity3d.com/uploads/Main/MecanimAnimationLayers2.png)

> ‘M’标志这层用了mask

![](http://docs.unity3d.com/uploads/Main/AnimatorMaskOnLayer.png)

##### Animation Layer syncing #####
状态机的结构相同, 但是动画片段不同

![](http://docs.unity3d.com/uploads/Main/AnimatorSyncedLayer.png)

> ‘S’标志这层是同步层

todo::做一个人荡秋千的功能


#### Solo and Mute functionality ####
Soloed transitions will be shown in green, while muted transitions in red, like this:

![](http://docs.unity3d.com/uploads/Main/MecanimSoloMuteGraph.png)

#### Target Matching ####
    using UnityEngine;
    using System;

    [RequireComponent(typeof(Animator))] 
    public class TargetCtrl : MonoBehaviour {

        protected Animator animator;    
        
        //the platform object in the scene
        public Transform jumpTarget = null; 
        void Start () {
            animator = GetComponent<Animator>();
        }
        
        void Update () {
            if(animator) {
                if(Input.GetButton("Fire1"))         
                    animator.MatchTarget(jumpTarget.position, jumpTarget.rotation, AvatarTarget.LeftFoot, 
                                                        new MatchTargetWeightMask(Vector3.one, 1f), 0.141f, 0.78f);
            }       
        }
    }

#### 反向动力学 ####
效果是右手拿着圆柱

![](http://docs.unity3d.com/uploads/Main/MecanimIKGrabbing.png)

选中IK Pass

![](http://docs.unity3d.com/uploads/Main/AnimatorControllerToolSettingsIKPass.png)

将下面脚本附到animator上

    using UnityEngine;
    using System;
    using System.Collections;

    [RequireComponent(typeof(Animator))] 

    public class IKControl : MonoBehaviour {
        
        protected Animator animator;
        
        public bool ikActive = false;
        public Transform rightHandObj = null;
        public Transform lookObj = null;

        void Start () 
        {
            animator = GetComponent<Animator>();
        }
        
        //a callback for calculating IK
        void OnAnimatorIK()
        {
            if(animator) {
                
                //if the IK is active, set the position and rotation directly to the goal. 
                if(ikActive) {

                    // Set the look target position, if one has been assigned
                    if(lookObj != null) {
                        animator.SetLookAtWeight(1);
                        animator.SetLookAtPosition(lookObj.position);
                    }    

                    // Set the right hand target position and rotation, if one has been assigned
                    if(rightHandObj != null) {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
                        animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                    }        
                    
                }
                
                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else {          
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
                    animator.SetLookAtWeight(0);
                }
            }
        }    
    }


![](http://docs.unity3d.com/uploads/Main/MecanimIKGrabHandle.png)

手拿的和眼看的不是同一个地方

![](http://docs.unity3d.com/uploads/Main/MecanimIKSetupInspector.png)

todo::试下SetIKHintPosition


#### Root Motion ####
##### Root Transform #####
- Body Transform是物体的质量中心
- Root Transform是Body Transform在Y平面上的投影，是在运行时计算的

角色下方的圆代表root transform

![](http://docs.unity3d.com/uploads/Main/MecanimRootMotionPreview.png)

Animation Clip Inspector

![](http://docs.unity3d.com/uploads/Main/MecanimRootMotion.png)

##### Root Transform Rotation #####
Bake into Pose: Rotation属性不会随动画变化，一般开始和结束方向相同时启用，即loop match为绿色

Based Upon: Using Body Orientation works well for most Motion Capture (Mocap) data like walks, runs, and jumps, but it will fail with motion like strafing where the motion is perpendicular to the body’s forward vector. In those cases you can manually adjust the orientation using the Offset setting.

##### Root Transform Position (Y) #####
多数情况下启用，只有改变物体高度的不启用，例如跳跃

Note: the Animator.gravityWeight is driven by Bake Into Pose position Y. When enabled, gravityWeight = 1, when disabled = 0. gravityWeight is blended for clips when transitioning between states.

##### Root Transform Position (XZ) #####
“Idles”通常会启用，使Position.XZ固定

##### 就地动画，代码让你动 #####
在动画上创建一个名为Runspeed的曲线，添加同名参数，适用于人形动画和非人形动画

    using UnityEngine;
    using System.Collections;

    [RequireComponent(typeof(Animator))]
    public class RootMotionScript : MonoBehaviour {            
        void OnAnimatorMove()
        {
            Animator animator = GetComponent<Animator>();						  
            if (animator)
            {
                Vector3 newPosition = transform.position;
                newPosition.z += animator.GetFloat("Runspeed") * Time.deltaTime; 
                transform.position = newPosition;
            }
        }
    }

> 重写OnAnimatorMove后，Apply Root Motion会变为Handled By Script



### Blend Trees ###
#### 1D Blending ####
![](http://docs.unity3d.com/uploads/Main/MecanimBlendTree1D.png)

The Compute Thresholds drop-down will set the thresholds from data of your choice obtained from the root motions in the Animation Clips.

权重

![](http://docs.unity3d.com/uploads/Main/MecanimBlendTreeParam.png)

#### 2D Blending ####
![](https://docs.unity3d.com/uploads/Main/MecanimBlendTree2D.png)

- 2D Simple Directional: Best used when your motions represent different directions, such as “walk forward”, “walk backward”, “walk left”, and “walk right”, or “aim up”, “aim down”, “aim left”, and “aim right”. Optionally a single motion at position (0, 0) can be included, such as “idle” or “aim straight”. In the Simple Directional type there should not be multiple motions in the same direction, such as “walk forward” and “run forward”.

- 2D Freeform Directional: This blend type is also used when your motions represent different directions, however you can have multiple motions in the same direction, for example “walk forward” and “run forward”. In the Freeform Directional type the set of motions should always include a single motion at position (0, 0), such as “idle”.

- 2D Freeform Cartesian: Best used when your motions do not represent different directions. With Freeform Cartesian your X parameter and Y parameter can represent different concepts, such as angular speed and linear speed. An example would be motions such as “walk forward no turn”, “run forward no turn”, “walk forward turn right”, “run forward turn right” etc.

- Direct: This type of blend tree lets user control the weight of each node directly. Useful for facial shapes or random idle blending.

![](https://docs.unity3d.com/uploads/Main/MecanimBlendTree2DDiagram.png)

#### Direct Blending ####
todo::实践下

![](https://docs.unity3d.com/uploads/Main/AnimatorDirectBlendTree.png)

This can be particularly useful when mixing blendshape animations for facial expressions, or when blending together additive animations.

![](https://docs.unity3d.com/uploads/Main/AnimatorDirectBlendTreeFacialExpressions.png)



### Animation Blend Shapes ###
用于网格动画混合，一般是面部表情

    using UnityEngine;
    using System.Collections;
    
    public class BlendShapeExample : MonoBehaviour
    {
    
        int blendShapeCount;
        SkinnedMeshRenderer skinnedMeshRenderer;
        Mesh skinnedMesh;
        float blendOne = 0f;
        float blendTwo = 0f;
        float blendSpeed = 1f;
        bool blendOneFinished = false;
    
        void Awake ()
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
            skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
        }
    
        void Start ()
        {
            blendShapeCount = skinnedMesh.blendShapeCount; 
        }
    
        void Update ()
        {
            if (blendShapeCount > 2) {
    
                    if (blendOne < 100f) {
                        skinnedMeshRenderer.SetBlendShapeWeight (0, blendOne);
                        blendOne += blendSpeed;
                    } else {
                        blendOneFinished = true;
                    }
    
                    if (blendOneFinished == true && blendTwo < 100f) {
                        skinnedMeshRenderer.SetBlendShapeWeight (1, blendTwo);
                        blendTwo += blendSpeed;
                    }
    
            }
        }
    }



### Animator Override Controllers ###
扩展存在的Animator Controller, 替换动画但保留结构逻辑和参数

![](https://docs.unity3d.com/uploads/Main/AnimatorOverrideControllerInspector.png)





## Retargeting of Humanoid animations ##
todo::实践下

1. Create a GameObject in the Hierarchy that contains Character-related components
![](http://docs.unity3d.com/uploads/Main/MecanimRetargetingTopLevel.png)

2. Put the model as a child of the GameObject, together with the Animator component
![](http://docs.unity3d.com/uploads/Main/MecanimRetargetingModel.png)

3. Make sure scripts referencing the Animator are looking for the animator in the children instead of the root; use GetComponentInChildren<Animator>() instead of GetComponent<Animator>().

4. Disable the original model and Drop in the desired model as another child of GameObject
![](http://docs.unity3d.com/uploads/Main/MecanimRetargetingOtherModel.png)

5. Make sure the Animator Controller property for the new model is referencing the same controller asset
![](http://docs.unity3d.com/uploads/Main/MecanimRetargetingOtherModelCorrectController.png)

6. Tweak the character controller, the transform, and other properties on the top-level GameObject, to make sure that the animations work smoothly with the new model.



## Playable API ##
todo::还没看
