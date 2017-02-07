## 摄像机 ##
### 基本 ###
#### 透视与正交 ####
![](http://docs.unity3d.com/uploads/Main/CameraPerspectiveAndOrtho.jpg)

透视(左) VS 正交(右)

#### 背景 ####
通常天空盒用来表示天空，但是实际上天空盒是完全围绕摄像机的，包括地表以下。意味着可以用天空盒来表示部分场景内容，例如远处起伏的地表、太空、水下。



### 视锥 ###
#### 原理 ####
![](http://docs.unity3d.com/uploads/Main/ViewFrustum.png)

#### 计算 ####
- var frustumHeight = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
- var distance = frustumHeight * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
- var camera.fieldOfView = 2.0f * Mathf.Atan(frustumHeight * 0.5f / distance) * Mathf.Rad2Deg;
- var frustumWidth = frustumHeight * camera.aspect;
- var frustumHeight = frustumWidth / camera.aspect;

#### 强调效果 ####
        public class SuperEffect : MonoBehaviour {

                public Transform target;

                private Camera camera;
                private float initHeightAtDist;
                
                void Start () {
                        camera = GetComponent<Camera>();

                        float distance = Vector3.Distance(transform.position, target.position);
                        initHeightAtDist = 2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
                }
                
                void Update () {
                        float currentDistance = Vector3.Distance(transform.position, target.position);
                        camera.fieldOfView = 2.0f * Mathf.Atan(initHeightAtDist * 0.5f / currentDistance) * Mathf.Rad2Deg;

                        transform.Translate(Input.GetAxis("Vertical") * 4 * Vector3.forward * Time.deltaTime);
                }
        }
        
#### 看大看小 ####
- 为了使物体看起来小，摄像机离物体远点并用窄的FOV
- 为了使物体看起来大，摄像机离物体近点并用宽的FOV

> 默认FOV为60。当从制高点看小物体时，以上效果会更明显。

#### 斜视锥 ####
![](http://docs.unity3d.com/uploads/Main/ObliqueFrustum.png)

赛车游戏中调整接近地平线，显示出速度感。一般值在[-1,1]之间，很少但是也能超出这个范围。

        public class ObliquenessEffect : MonoBehaviour {

                public float horizObl = 0.0f;
                public float verObl = 0.0f;

                void SetObliqueness() {
                        Matrix4x4 mat = Camera.main.projectionMatrix;
                        mat[0, 2] = horizObl;
                        mat[1, 2] = verObl;
                        Camera.main.projectionMatrix = mat;
                }

                void Update() {
                        SetObliqueness();
                }
        }
        




### 使用多个摄像机 ###
#### 切换 ####
        //使用enable属性切换
        public class ExampleScript : MonoBehaviour {
                public Camera firstPersonCamera;
                public Camera overheadCamera;

                public void ShowOverheadView() {
                        firstPersonCamera.enabled = false;
                        overheadCamera.enabled = true;
                }
                
                public void ShowFirstPersonView() {
                        firstPersonCamera.enabled = true;
                        overheadCamera.enabled = false;
                }
        }

#### 大中有小(小地图) ####
Viewport Rect属性，设置小地图占屏幕的比例和在屏幕中的位置.

Depth属性，设置小地图的大点，越大越会被显示


### 屏幕射线 ###
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
                hit.transform.localScale = hit.transform.localScale * 2;
        }
                
### 遮挡剔除 ###
#### 概念 ####
- View Cells (Static Objects)，蓝色线框
- Target Cells (Moving Objects)，白色线框 


可以在Scene窗口中使用Overdraw视图模式，在Game窗口中查看Stats统计信息

> 通常cell相对游戏对象不要太小，不要让游戏对象跨越太多个cell



#### 操作 ####
![](http://docs.unity3d.com/uploads/Main/OcclusionStaticDropdown.png)

- Occludee Static 用于透明的或者太小的，可以被其他物体遮住，而不能遮住其他物体的静态对象
- Occluders 其他的静态对象

#### Occlusion Culling Window ####
Window - Occlusion Culling

##### Object Tab #####
![](http://docs.unity3d.com/uploads/Main/OcclusionCullingInspectorObject.png)

Mesh Renderer

![](http://docs.unity3d.com/uploads/Main/OcclusionCullingInspectorOcclusionArea.png)

Occlusion Area

> 默认情况下，如果不创建任何Occlusion Area，Unity会用整个场景区域当作Occlusion Area

> 只有摄像机在Occlusion Area以内时，遮挡剔除才会生效


##### Bake Tab #####
![](http://docs.unity3d.com/uploads/Main/OcclusionCullingInspectorBake.png)

> 一般用默认的就好了

##### Visualization Tab #####
![](http://docs.unity3d.com/uploads/Main/OcclusionCullingInspectorVisualization.png)

选择一个摄像机，在Scene窗口的预览



#### Occlusion Area ####
在一个空对象上添加组件(Component - Rendering - Occlusion Area)

![](http://docs.unity3d.com/uploads/Main/OcclusionCullingOcclusionArea.png)

Occlusion Culling预览面板

![](http://docs.unity3d.com/uploads/Main/OcclusionCullingViewVolumes.png)


#### 测试 ####
将Occlusion Culling预览面板改为Visualize模式，在Scene窗口中移动摄像机，查看是否有在屏幕中突然出现的物体。

![](http://docs.unity3d.com/uploads/Main/OcclusionCullingVisualize.png)





## 材料, Shader & 纹理 ##
### 基本 ###
创建材料：Assets-Create-Material

![](http://docs.unity3d.com/uploads/Main/StandardShaderNewEmptyMaterial.png)

Shader技术细节

![](http://docs.unity3d.com/uploads/Main/material_diagram.png)

- 通常，角色、场景、固体和透明物体、软硬表面，Standard Shader是不错的选择。
- 其他情况，例如液体、叶子、粒子效果、卡通、艺术效果、夜视、辐射图，使用内建的其他shader或自定义shader。



### Standard Shader ###
#### 基本 ####
Standard Shader用于硬表面物体，用于大多数真实世界的材料，例如石头、玻璃、陶瓷、黄铜、银、橡胶，甚至是软材料如皮肤、头发、衣服。

> 物体从不反射比他们接收到的更多的光
 
#### Content and Context ####
##### The Context #####
周围直接照射的光、间接光等环境因素

##### The Content #####
物体本身的渲染特性

![](http://docs.unity3d.com/uploads/Main/StandardShaderMaterialInspector.png)

> 在纹理缩略图上Ctrl+点击，可以预览大点的图

#### Metallic vs Specular ####
todo::需要进一步看

- Standard: The shader exposes a “metallic” value that states whether the material is metallic or not. In the case of a metallic material, the Albedo color will control the color of your specular reflection and most light will be reflected as specular reflections. Non metallic materials will have specular reflections that are the same color as the incoming light and will barely reflect when looking at the surface face-on.

- Standard (Specular setup): Choose this shader for the classic approach. A Specular color is used to control the color and strength of specular reflections in the material. This makes it possible to have a specular reflection of a different color than the diffuse reflection for instance.

![](http://docs.unity3d.com/uploads/Main/StandardShaderRubberAsMetallicOrSpecular.png)

#### Material parameters ####
##### Rendering Mode #####
![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterRenderMode.png)

- Opaque - 不透明固体.
- Cutout - 全透明和全不透明结合，有明显的界限。高于alpha值的可见，低于alpha值的不可见，且不可见部分不会像Transparent出现反光效果。例如有孔的叶子、有洞的衣服，下图草丛

![](http://docs.unity3d.com/uploads/Main/StandardShaderCutoutGrassExample.png)

- Transparent - 透明，通过alpha值确定透明度。例如塑料袋、玻璃，下图太空帽、窗户框架和玻璃

![](http://docs.unity3d.com/uploads/Main/StandardShaderTransparencySkyBoxReflection.png)

![](http://docs.unity3d.com/uploads/Main/StandardShaderTransparentWindow.png)

- Fade - 透明淡出，而且没有高光和反射，通过调节albedo属性的alpha值实现。例如下图全息投影。

![](http://docs.unity3d.com/uploads/Main/StandardShaderFadeHologram.png)

##### Albedo Color and Transparency #####
![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterAlbedoColor.png)

> 纹理贴图不要包含光照信息

![](http://docs.unity3d.com/uploads/Main/StandardShaderAlbedoTextureExamples.png)

Alpha通道被映射为透明度，白色表示完全不透明，黑色表示完全透明，可以通过点击RGB/A按钮切换预览通道

![](http://docs.unity3d.com/uploads/Main/StandardShaderTransparencyMapRGBAlphaToggle.png)

##### Metallic mode: Metallic Parameter #####
![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterMetallic.png)

使用纹理贴图时，Metalic由R控制，Smoothness由Alpla控制，GB没用

> Metallic模式不仅仅是给金属用的

##### Specular Mode: Specular Parameter #####
![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterSpecularSmoothness.png)

使用纹理贴图时，Specular由RGB控制，Smoothness由Alpha控制 

##### Smoothness #####
![](http://docs.unity3d.com/uploads/Main/StandardShaderMicrosurfaceDiagram.svg)

光滑度从左到右由低到高，由漫反射向镜面反射转变

![](http://docs.unity3d.com/uploads/Main/StandardShaderEnergyConservation.png)

光滑度从上到下由低到高，高光越来越明显

> 使用纹理贴图时，Smoothness由贴图的Alpha值决定。越光滑越像镜子，高光越小越明显；越粗糙越不像镜子高光越大越不明显。


##### 法线贴图 (凹凸贴图) #####
###### 效果 ######
![](http://docs.unity3d.com/uploads/Main/BumpMapTexturePreview.png)

使用了上图中法线贴图的效果

![](http://docs.unity3d.com/uploads/Main/BumpMapLitExample.png)

> 用于制作凸块、凹槽、折痕，衣服上的钮扣、拉链、接缝

###### 原理 ######
![](http://docs.unity3d.com/uploads/Main/BumpMapFlatShadingDiagram.svg)

Flat shading

![](http://docs.unity3d.com/uploads/Main/BumpMapSmoothShadingDiagram.svg)

Smooth shading(根据已知的红色箭头，通过插值计算出黄色箭头)

![](http://docs.unity3d.com/uploads/Main/BumpMapBumpShadingDiagram.svg)

Normal mapping(贴图的RGB对应XYZ向量, 在Smooth shading的基础上修改方向)

> 为什么法线贴图是蓝色调的？RGB对应XYZ向量，Z表示向上。对应关系为除以2加0.5，例如XYZ(0,0,1)对应RGB(0.5, 0.5, 1)，即正上方不对默认法线方向做修改。

###### 导入方法 ######
Normal Map和Height Map都是凹凸贴图

![](http://docs.unity3d.com/uploads/Main/BumpMapHeightMapNormalMapComparison.png)

- 左边是height map，通过灰度表示凸起的高度，越白越高
- 右边是normal map，通过RGB表示法线方向

导入normal map：“Texture Type”设为“Normal Map”

![](http://docs.unity3d.com/uploads/Main/BumpMapImportInspectorWindow.png)

导入height map，和导入normal map一样，除了需要勾选“Create from Greyscale”

![](http://docs.unity3d.com/uploads/Main/BumpMapImportInspectorGreyscale.png)

将法线贴图设置到Standard Shader中

![](http://docs.unity3d.com/uploads/Main/BumpMapPutIntoShader.png)


> 通过3D建模软件，建模高精度模型，然后生成法线贴图，用于低精度模型。

###### Secondary Normal Maps ######
精度为第一个的5到10倍，更精细用于近看

##### 高度贴图(视差贴图) #####
todo::用的G通道？

![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterHeightmap.png)

高度贴图用灰度图表示，白色代表凸出，黑色代表凹陷

![](http://docs.unity3d.com/uploads/Main/StandardShaderParallaxMap.jpg)

从左到右：
1. 纹理贴图
2. 纹理贴图+法线贴图
3. 纹理贴图+法线贴图+高度贴图

> 有遮挡效果，只是改变的表面的显示，并没有改变真正的几何体

##### Occlusion Map #####
todo::用的G通道？

![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterOcclusion.png)

occlusion map遮挡环境光，用灰度图表示，白色代表接受，黑色代表不接受。

![](http://docs.unity3d.com/uploads/Main/StandardShaderOcclusionMap.png)

简单情况，如平面的石头墙，基本和高度贴图相同；复杂情况，如衣服遮挡，需要用3D软件生成。

##### Emission #####
![](http://docs.unity3d.com/uploads/Main/StandardShaderParameterEmission.png)

用于自发光物体，例如屏幕、怪兽发光的眼睛。

##### Secondary Maps (Detail Maps) & Detail Mask #####
###### Secondary Maps ######
用于添加细节，如皮肤毛孔、砖墙上的青苔、金属容器上的划痕

###### Detail Mask ######
Secondary Maps的Mask，通过Alpha通道控制，只让一部分区域使用Secondary Maps。例如不让皮肤毛孔作用于嘴唇和睫毛处。


#### 参考图表 ####
![](http://docs.unity3d.com/uploads/Main/StandardShaderCalibrationChartMetallic.png)

![](http://docs.unity3d.com/uploads/Main/StandardShaderCalibrationChartSpecular.png)


### 运行时修改Shader ###
        using UnityEngine;
        using System.Collections;

        public class ShaderController : MonoBehaviour {

                private Renderer rend;

                        void Start () {
                                rend = GetComponent<Renderer>();
                                //Shader.EnableKeyword("_EMISSION");
                        }
                        
                        void Update () {
                                float metallic = Mathf.PingPong(Time.time, 1);
                                rend.material.SetFloat("_Metallic", metallic);
                        }
        }
> Standard Shader(or Specular Setup)需要注意：

> 1. 使用EnableKeyword开启对应的shader variant
> 2. build时确保Unity包含了对应的shader variants