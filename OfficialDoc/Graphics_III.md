## 粒子系统 ##
### 使用 ###
GameObject > Particle System 或者添加组件(Component > Effects > Particle System)

![](http://docs.unity3d.com/uploads/Main/PartSysEffectPanel.png)


### 简单爆炸 ###
![](http://docs.unity3d.com/uploads/Main/PartSysExpScreenshot.png)

#### 步骤 ####
Starting with the default particle system object (menu: GameObject > Create General > Particle System), go to the Shape module and set the emitter shape to a small Sphere, say about 0.5 units in radius. The particles in the standard assets include a material called Fire Add which is very suitable for explosions (menu: Assets > Import Package > Particles). You can set this material for the system using the Renderer module. With the Renderer open, you should also disable Cast Shadows and Receive Shadows since the explosion flames are supposed to give out light rather than receive it.

At this stage, the system looks like lots of little fireballs being thrown out from a central point. The explosion should, of course, create a burst with lots of particles all at once. In the Emission module, you can set the Rate value to zero and add a single Burst of particles at time zero. The number of particles in the burst will depend on the size and intensity you want your explosion to have but a good starting point is about fifty particles. With the burst set up, the system is now starting to look much more like and explosion but it is rather slow and the flames seem to hang around for a long time. In the Particle System module (which will have the same name as the GameObject, eg, “Explosion”), set both the Duration of the system and the Start Lifetime of the particles to two seconds.

You can also use the Size Over Lifetime module to create the effect of the flames using up their fuel. Set the size curve using the “ramp down” preset (ie, the size starts off at 100% and reduces to zero. To make the flames darken and fade, enable the Color Over Lifetime module and set the gradient to start with white at the left and finish with black at the right. Since the Fire Add material uses and additive shader for rendering, the darkness of the color property also controls the transparency of the particle; the flame’s will become fully transparent as the color fades to black. Also, the additive material allows the brightness of particles to “add” together as they are drawn on top of each other. This helps to further enhance the impression of a bright flash at the start of the explosion when the particles are all close together.

As it stands, the explosion is taking shape but it looks as though it is happening out in space. The particles get thrown out and travel a long distance at constant speed before fading. If your game is set in space then this might be the exact effect you want. However, an explosion that happens in the atmosphere will be slowed and dampened by the surrounding air. Enable the Limit Velocity Over Lifetime module and set the Speed to about 3.0 and the Dampen fraction to about 0.4 and you should see the explosion lose a little strength as it progresses.

A final thing to note is that as the particles move away from the centre of the explosion, their individual shapes become more recognisable. In particular, seeing the particles all at the same size and with the same rotation makes it obvious that the same graphic is being reused for each particle. A simple way to avoid this is to add a bit of random variation to the size and rotation of the particles as they are generated. In the Particle System module at the top of the inspector, click the small arrow to the right of the Start Size and Start Rotation properties and set them both to Random Between Two Constants. For the rotation, set the two values to 0 and 360 (ie, completely random rotation). For the size, set the values to 0.5 and 1.5 to give some variation without the risk of having too many huge or tiny particles. You should now see that the repetition of particle graphics is now much less noticeable.

#### 代码 ####
    void Explode() {
        var exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.duration);
    }

todo::打树花，手机上用手触摸后会更旺


### 汽车尾气 ###
![](http://docs.unity3d.com/uploads/Main/PartSysExhaustScreenshot.png)

#### 步骤 ####
In the Shape module, select the Cone shape and set its Angle property to zero; the “cone” in this case will actually be a cylindrical pipe. The Radius of the pipe naturally depends on the size of the vehicle but you can usually set it by matching the radius Gizmo in the scene view to the vehicle model (eg, a car model will usually feature an exhaust pipe or a hole at the back whose size you can match). The radius actually determines quite a few things about the property settings you choose, such as the particle size and emission rate. For the purposes of this example, we will assume the vehicle is a car which follows Unity’s standard size convention of one world unit to one metre; the radius is thus set to about 0.05 or 5cm.

A suitable graphic for the smoke particle is provided by the Smoke4 material provided in the standard assets. If you don’t already have these installed then select Assets > Import Package > Particles from the menu. Then, go to the Renderer module of the particle system and set the Material property to Smoke4.

The default lifetime of five seconds is generally too long for car exhaust fumes, so you should open the Particle System module (which has the same name as the GameObject, eg, “Exhaust”) and set the Start Lifetime to about 2.5 seconds. Also in this module, set the Simulation Space to World and the Gravity Modifier to a small negative value, say about –0.1. Using a world simulation space allows the smoke to hang where it is produced even when the vehicle moves. The negative gravity effect causes the smoke particles to rise as if they are composed of hot gas. A nice extra touch is to use the small menu arrow next to Start Rotation to select the Random Between Two Constants option. Set the two values to 0 and 360, respectively, and the smoke particles will be randomly rotated as they are emitted. Having many particles that are identically aligned is very noticeable and detracts from the effect of a random, shapeless smoke trail.

At this stage, the smoke particles are starting to look realistic and the default emission rate creates a nice “chugging” effect of an engine. However, the smoke doesn’t billow outwards and dissipate as yet. Open the Color Over Lifetime module and click the top gradient stop on the right hand end of the gradient (this controls the transparency of “alpha” value of the color). Set the alpha value to zero and you should see the smoke particles fading to nothing in the scene. Depending on how clean your engine is, you may also want to reduce the alpha value of the gradient at the start; thick, dark smoke tends to suggest dirty, inefficient combustion.

As well as fading, the smoke should also increase in size as it escapes and you can easily create this effect with the Size Over Lifetime module. Open the module, select the curve and slide the curve handle at the left hand end to make the particles start off at a fraction of their full size. The exact size you choose depends on the size of the exhaust pipe but a value slightly larger than the pipe gives a good impression of escaping gas. (Starting the particles at the same size as the pipe suggests that the gas is being held to its shape by the pipe but of course, gas doesn’t have a defined shape.) Use the simulation of the particle system in the scene view to get a good visual impression of how the smoke looks. You may also want to increase the Start Size in the particle system module at this point if the smoke doesn’t disperse far enough to create the effect you want.

Finally, the smoke should also slow down as it disperses. An easy way to make this happen is with the Force Over Lifetime module. Open the module and set the Space option to Local and the Z component of the force to a negative value to indicate that the particles are pushed back by the force (the system emits the particles along the positive Z direction in the object’s local space). A value of about –0.75 works quite well for the system if the other parameters are set up as suggested above.

#### 代码 ####
    public class ExhaustController : MonoBehaviour {

        private ParticleSystem particle;
        
        void Start () {
            particle = GetComponent<ParticleSystem>();
        }
        
        void Update () {
            if (Input.GetKeyDown(KeyCode.LeftControl)) {
                ParticleSystem.EmissionModule em = particle.emission;
                em.rate = new ParticleSystem.MinMaxCurve(em.rate.constantMax*2);
            }
        }
    }
    
todo::做一个换车的功能，不同的车出不同的尾气
    


## Procedural Materials ##
### 基本概念 ###
![](http://docs.unity3d.com/uploads/Main/Inspector-ProceduralMaterial1.png)

可以在运行时生成贴图，导入Substance Archive file (sbsar)文件，使用方法和传统Material一样

使用Substance Designer制作

使用Substance Player分析性能


### 运行中更新材质 ###
	using UnityEngine;
	using System.Collections;
	
	public class PBSController : MonoBehaviour {
	
		private ProceduralMaterial pbs;
	
	
		void Start () {
			pbs = GetComponent<MeshRenderer> ().material as ProceduralMaterial;
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown (KeyCode.Space)) {
				ProceduralPropertyDescription[] ppds = pbs.GetProceduralPropertyDescriptions ();
				for (int i = 0; i < ppds.Length; i++) {
					Debug.Log ("property:" + ppds [i].name);
				}
	
				pbs.SetProceduralFloat ("Grass_Amount", Random.value*100);
				pbs.RebuildTextures ();
			}
		}
	}






## Mesh组件 ##
### Colliders ###
两种类型：Mesh Colliders和Primitive Colliders，可以在导入模型中选中Generate Colliders生成Mesh Colliders

> Mesh Colliders一般用在静止物体(如地形)上，因为它消耗资源多；运动物体使用Primitive Colliders

### Blendshapes ###
Unity has support for BlendShapes (also called morph-targets or vertex level animation).

todo::需要进一步学习

### Mesh Renderer ###
todo::Anchor Override,Light Probe Proxy Volume

### Skinned Mesh Renderer ###
![](http://docs.unity3d.com/uploads/Main/Inspector-SkinnedMeshRenderer.png)

一些情况下边框不好确定，解决方法有两种：
- Modify the Bounds to match the potential bounding volume of your Mesh
- Enable Update When Offscreen to skin and render the skinned Mesh all the time

todo::做个非人形动画

### Text Mesh ###
这个不受光照影响，而且可以穿透遮挡它的物体，不好用

![](http://docs.unity3d.com/uploads/Main/Inspector-TextMesh.png)

> 字号太小会不清楚，可以将字号设大些，然后把scale调小

#### Text Asset ####
![](http://docs.unity3d.com/uploads/Main/TextAssetInspector.png)

    TextAsset ta = Resources.Load<TextAsset>("freetodo");
    TextMesh tm = GetComponent<TextMesh>();
    tm.text = ta.text;

> Text Assets不是为了运行时生成文本文件使用的

#### 字体 ####
支持TrueType Fonts (.ttf files)和OpenType Fonts (.otf files).

![](http://docs.unity3d.com/uploads/Main/font_importer.png)




## Procedural Mesh Geometry ##
### Mesh剖析 ###
#### Lighting and Normals ####
A normal is a vector that points outward, perpendicular to the mesh surface at the position of the vertex it is associated with.

By calling Mesh.RecalculateNormals, you can get Unity to work out the normals’ directions for you by making some assumptions about the “meaning” of the mesh geometry; it assumes that vertices shared between triangles indicate a smooth surface while doubled-up vertices indicate a crisp edge. While this is not a bad approximation in most cases, RecalculateNormals will be tripped up by some texturing situations where vertices must be doubled even though the surface is smooth.

#### Texturing ####
Like normals, texture coordinates are unique to each vertex and so there are situations where you need to double up vertices purely to get different UV values across an edge. An obvious example is where two adjacent triangles use discontinuous parts of the texture image (eyes on a face texture, say). Also, most objects that are fully enclosed volumes will need a “seam” where an area of texture wraps around and joins together. The UV values at one side of the seam will be different from those at the other side.

### 使用Mesh类 ###
    using UnityEngine;
    using System.Collections;

    public class ExampleClass : MonoBehaviour {
        public Vector3[] newVertices;
        public Vector2[] newUV;
        public int[] newTriangles;
        void Start() {
            Mesh mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            mesh.vertices = newVertices;
            mesh.uv = newUV;
            mesh.triangles = newTriangles;
        }
    }

> 顶点必须顺时针才能看见，表示正面

### 公告板 ###
![](http://docs.unity3d.com/uploads/Main/MeshConstruction.png)

使用下面的代码，并给对象添加Mesh Filter和Mesh Renderer组件

    using UnityEngine;
    using System.Collections;

    public class BillboardCreator : MonoBehaviour {

        public float width;
        public float height;

        // Use this for initialization
        void Start () {
            MeshFilter mf = GetComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            mf.mesh = mesh;

            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(0, 0, 0);
            vertices[1] = new Vector3(width, 0, 0);
            vertices[2] = new Vector3(0, height, 0);
            vertices[3] = new Vector3(width, height, 0);
            mesh.vertices = vertices;


            int[] tri = new int[6];
            tri[0] = 0;
            tri[1] = 2;
            tri[2] = 1;
            tri[3] = 2;
            tri[4] = 3;
            tri[5] = 1;
            mesh.triangles = tri;


            Vector3[] normals = new Vector3[4];
            normals[0] = -Vector3.forward;
            normals[1] = -Vector3.forward;
            normals[2] = -Vector3.forward;
            normals[3] = -Vector3.forward;
            mesh.normals = normals;


            Vector2[] uv = new Vector2[4];
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(1, 0);
            uv[2] = new Vector2(0, 1);
            uv[3] = new Vector2(1, 1);
            mesh.uv = uv;
        }
        
        // Update is called once per frame
        void Update () {
        
        }
    }



## 纹理 ##
### 2D纹理 ###
![](http://docs.unity3d.com/uploads/Main/TexImporterFull40.png)

> 最好是2的次方大小的正方形纹理，除非是GUI使用

### Render Texture ###
![](http://docs.unity3d.com/uploads/Main/Inspector-RenderTexture.png)

摄像机指定Target Texture，Mesh使用Target Texture，可以将摄像机看到的内容生成到纹理上。

> 做汽车观后镜效果。将摄像机的渲染路径设为Deferred可以产生出无尽的效果

todo::制作万花筒

![](http://docs.unity3d.com/uploads/Main/RenderTextureLiveCam.png)

### Movie Texture ###
todo::冒似不支持安卓，不看了

### 3D Textures ###
通常用于体绘制，这里不做研究了

