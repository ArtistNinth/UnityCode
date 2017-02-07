## 灯光 ##
### 类型 ###
#### 点光 ####
![](http://docs.unity3d.com/uploads/Main/PointLightDiagram.svg)

模拟蜡烛、烟花、爆炸

> 目前点光和聚光灯不支持间接光阴影，所以间接光会穿过物体导致漏光，因此摆放时注意物体的位置。使用Baked GI没有这个问题。

#### 聚光灯 ####
![](http://docs.unity3d.com/uploads/Main/SpotLightDiagram.svg)

模拟手电筒、汽车大灯、探照灯、路灯

#### 平行光 ####
![](http://docs.unity3d.com/uploads/Main/DirectionalLightDiagram.svg)

模拟太阳、月光

#### 区域光 ####
![](http://docs.unity3d.com/uploads/Main/AreaLightDiagram.svg)

柔和，运行时不可用只能被渲染成光照贴图，矩形的单面发出各个方向的光，模拟天花板灯条、真实的路灯



### Cookies ###
#### 使用Cookie ####
1.使用灰度图，Unity会将灰度转换为透明度

![](http://docs.unity3d.com/uploads/Main/Cookie.png)

2.选择光源类型，选中Alpha From Grayscale

![](http://docs.unity3d.com/uploads/Main/CookieImportSettings.png)

> 平行光可以使用平行光或聚光灯类型的Cookie：当使用平行光的Cookies时，会将形状重复许多次；当使用聚光灯的Cookie时，只会出现一次。

todo::冒似点光也可以使用cookie，需要做进一步测试

3.将Cookie应用到灯光

![](http://docs.unity3d.com/uploads/Main/CookieLightInspector.png)

#### 自己画Cookie ####
1.在Photoshop中新建灰度图

2.黑色代表无光，白色代表有光，边缘部分一般是黑色

3.导入Unity时，选中Alpha From Grayscale，会把灰度转换为透明度



### 渲染模式 ###
#### 两种模式 ####

- Vertex lighting 性能高，不支持一些特殊效果，比如cookie
- Pixel lighting 慢但是效果好

> 聚光灯和点光在Pixel lighting模式中效果好很多

#### 设置Render Mode ####
![](http://docs.unity3d.com/uploads/Main/LightInspectorV3.png)

- Important 强制使用Pixel lighting，仅仅在非常重要的灯光上使用(例如汽车的大灯)
- Not Important 强制使用Vertex lighting

> 自我感觉设置成Auto就好

### 阴影 ###
![](http://docs.unity3d.com/uploads/Main/InspectorMeshRend.png)

Mesh Renderer设置是否生成自身阴影或接受其他物体的阴影

> todo::Normal Bias

#### 性能影响 ####
- 实时光照
- Soft shadow对GPU影响大
- Shadow Distance
- 高分辨率的shadow map


### 平行光阴影 ###
#### 问题及解决方案 ####
![](http://docs.unity3d.com/uploads/Main/ShadMapFrustumDiagram.svg)

离摄像机近处的阴影会有马赛克，解决方案：

![](http://docs.unity3d.com/uploads/Main/ShadMapCascadeDiagram.svg)

> 只有平行光有这个问题

#### 操作 ####
Edit - Project Settings - Quality - Shadows - Shadow Cascades

![](http://docs.unity3d.com/uploads/Main/QualitySettingsBottom.png)

配置Shadow Cascades和Shadow Distance

> 在手机平台上，配置光照距离特别重要，因为手机平台不支持Shadow Cascades

Scene视图的draw mode设置为Shadow Cascades，来配合上面的Quality设置

![](http://docs.unity3d.com/uploads/Main/ShadCascade4Visualization.png)


### 全局光照 ###
#### 基础概念 ####
GI =直接光照+间接光照+环境光+反射光.

GI的重点在于Bounce , 可以将Static物体上的光反弹到其他Static物体上, 无法将非Static物体上的光反弹到Static物体上. 
也无法将Static物体上的光反弹到非Static物体上, 但是Unity5引入了新的概念, 那就是Realtime的LightProbe, 
Unity4中的LightProbe只能静态烘焙, 而Unity5的LightProbe会实时捕捉包括Precomputed Realtime GI的Bounce光在内的
任何光线信息并且将其赋予非Static物体上.

##### Baking模式 #####
- Realtime：对动态物体还是静态物体都使用实时光源.
- Baked：只对静态物体使用烘焙光照. 对动态物体没有作用.
- Mixed：对静态物体使用烘焙光照, 对动态物体使用实时光源.

##### GI分类 #####
- Precomputed Reatime GI：需要预先计算场景中所有的Static物体的信息, 并且允许在运行时
任意修改光源的Bounce Intensity或者移动光源的位置. 所有的变化都是实时的
- Baked GI：不会预先计算但会进行预先烘焙, 无法像Precomputed Realtime GI那样在运行时更改光源


#### Lighting Window ####
通过 Window - Lighting 打开光照窗口

使用下面的Build按钮，下拉中Clear Baked Data可以删除烘焙好的数据

##### Object Tab #####
显示当前选中物体的信息，可以同时选择多个

![](http://docs.unity3d.com/uploads/Main/LtWindowFilterLight.svg)

过滤在hierarchy中显示的物体

每个物体在Unity中最多会使用3个UV通道, UV1是贴图通道, UV2是光照贴图的通道, 而第三个UV是Unity自动生成的要用于Precomputed Realtime GI的UV通道.

![](http://imgsrc.baidu.com/forum/w%3D580/sign=6c30d524f336afc30e0c3f6d8318eb85/f85c7dd98d1001e9567e8cebbc0e7bec55e79720.jpg)
 
预览贴图，最后两个, 使用了光照贴图的UV通道, 而前面五个使用了第三个UV

###### Lights ######
影响GI的选项有Baking和Bounce Intensity.

###### Renderers ######
![](http://docs.unity3d.com/uploads/Main/LtWindowObjRenderProps.png)

重要的选项有Lightmap Static和Scale in Lightmap

###### Terrains ######
![](http://docs.unity3d.com/uploads/Main/LtWIndowObjTerrainProps.png)

重要的选项有Lightmap Static和Scale in Lightmap

##### Scene Tab #####
对整个场景生效

- 电脑或主机：Precomputed Realtime GI和Linear Color Space，渲染路径Deferred适用于全动态阴影
- 手机：Baked GI和Gamma Color Space,渲染路径Forward适用于烘焙阴影与实时阴影相结合

![](http://docs.unity3d.com/uploads/Main/LtWindowSceneTab.png)

##### Lightmaps Tab #####
![](http://docs.unity3d.com/uploads/Main/LtWindowLightmapsTab.png)

点击后会在Project view中显示对应的资源文件


#### Light Probes ####
LightProbe是对LightMapping的一个补充功能，可以让动态物体在烘焙好的场景里面受到光的照射效果，就是在LightMapping的基础上加上了一些探头的点来记录光源的信息

![](http://docs.unity3d.com/uploads/Main/LightProbesTestScene-baked.png)

##### 添加 #####
GameObject - Light - Light Probe Group

![](http://docs.unity3d.com/uploads/Main/InspectorLightProbeGroup.png)

> 设置probes点时，不要全部在同一水平面上，要有一定的高度差

##### 使用 #####
![](http://docs.unity3d.com/uploads/Main/MeshRendInspector.png)

在动态的物体上勾选Use Light Probes


#### Lightmap Parameters ####
-- todo::需要进一步学习

![](http://docs.unity3d.com/uploads/Main/newLightmapParameters.png)


#### Directional Lightmapping ####
三种模式依次变好: Non-directional, Directional and Directional with Specular. 都可用于realtime和baked lightmaps.


#### GI Cache ####
Edit - Preferences - GI Cache 设置缓存大小，清除缓存，或更改缓存位置


### 颜色空间 ###
#### Linear VS Gamma ####
##### Light Falloff #####
![](http://docs.unity3d.com/uploads/Main/lightfalloff.png)

##### Linear Intensity Response #####
![](http://docs.unity3d.com/uploads/Main/lineargammahead.png)

##### Linear and Gamma Blending #####
![](http://docs.unity3d.com/uploads/Main/LinearGammaBlending.png)

#### 设置 ####
Edit - Project Settings - Player - Other Settings - Color Space
a
![](http://docs.unity3d.com/uploads/Main/GammaPlayerSetting.png)

#### 平台支持 ####
如果实际的是Gamma而期望的是Linear，可以提示用户或者强制退出游戏

- QualitySettings.desiredColorSpace 期望的
- QualitySettings.activeColorSpace 实际的





## Reflection probes ##
### 使用 ###
![](http://docs.unity3d.com/uploads/Main/RefProbeInspector.png)

1. 创建一个空对象，然后添加组件(Component - Rendering - Reflection Probe). 
2. 添加一个简单的物体比如球(GameObject - 3D Object - Sphere).
3. 创建材料(Assets - Create - Material) 设置为Standard shader,设置Metallic和Smoothness为1.0,将该材料赋给球.
4. 如果需要别忘记bake

### 放置 ###
多个probe应该尽可能覆盖到反射物体可能经过的所有地方。容易被注意到的大的物体，中心和墙角是不错的地点，小的重要的物体需要放近些。 

![](http://docs.unity3d.com/uploads/Main/RefProbeHandles.svg)

![](http://docs.unity3d.com/uploads/Main/RefProbeOrigin.svg)

通过上面的两个按钮，可以拖动来调节Size和Probe Origin属性


### 相交区域 ###
![](http://docs.unity3d.com/uploads/Main/ProbeZoneOverlap.svg)

Mesh Renderer组件的Reflection Probes属性设置

- Blend Probes 忽略天空盒，适用于室内，例如洞穴、隧道
- Blend Probes and Skybox 适用于室外可以看到天空的地方
- Simple 物体的轮廓在哪个区域内占的多，就用哪个区域的probe

> Blend需要SM3及以上，以上3种都可以通过修改probes的Importance属性来更改权重


### 高级特征 ###
#### Interreflections ####
两面镜子互相反射的次数，配置：Lighting窗口中的Reflection Bounces属性


#### Box projection ####
- 通常，reflection cubemap被假设为无限远，不同方向看也有不同的效果，但是没有近大远小的效果，在室外用没有问题。
- Box Projection属性被假设为有限距离，适合室内用。离物体越近，物体看着越大，反之越小。

> Box Projection需要SM3及以上

### 性能优化 ###
#### 一般技巧 ####
##### Resolution #####
分辨率越高，渲染时间越长。可以把小的或远的物体设置为低分辨率，把容易看到的详细物体设置为高分辨率。

##### Culling Mask #####
可以忽略某些层，比如离的远的。
 
#### Realtime Probe优化 ####
##### Refresh Mode #####
- Every Frame. 每帧更新，特别消耗资源。

- On Awake. 开始时更新一次，适用于运行中途生成然后不变的场景。 

- Via Scripting. 手动更新，场景有较大变化时调用。
        private ReflectionProbe probe;
        probe.RenderProbe();

##### Time Slicing #####
- All Faces at Once. 9帧完成.

- Individual Faces. 14帧完成，最节省资源.

- No Time Slicing. 1帧完成，最费资源.