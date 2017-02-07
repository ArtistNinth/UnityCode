## 导入和创建 ##
### 小贴示 ###
- Cube边长是1，导入的模型可以和它对比大小
- Cylinder的Collider是Capsule，如果想要准确的物理检测，必须在建模软件中另外创作
- 外部可以打开或编辑资源，但是千万不能移动或重命名，只能在Unity中操作，
- Textures应该是2的幂次方(如32x32, 64x64, 128x128, 256x256, etc.)

### Asset Store ###
#### 下载路径 ####
- Windows:C:\Users\accountName\AppData\Roaming\Unity\Asset Store
- Mac:~/Library/Unity/Asset Store

#### Mass Labeler ####
todo::上传用到

#### Asset Store Publisher Administration ####
todo::上传用到

#### Asset Store Publishing Guide ####
todo::上传用到

#### DeprecateAssetGuide ####
todo::上传用到

#### Asset Store Manual ####
todo::上传用到

### Asset Server (Team License) ###
todo::Team License

### Cache Server (Team License) ###
todo::Team License

### 项目文件夹 ###
- Assets 资源，每个都有对应的.meta隐藏文件
- Library 内部关联，根据Assets和ProjectSettings生成，可以在Unity关闭时删除，不应包含在版本控制中
- ProjectSettings 基本对应Edit - Project Settings菜单中的内容

> 备份项目时，需要备份Assets和ProjectSettings文件夹，可以忽略Library和Temp文件夹


## AssetDatabase ##
> 仅在编辑时使用，编译后不可用。放在Assets/Editor文件夹中

    using UnityEngine;
    using UnityEditor;

    public class ImportAsset
    {

        [MenuItem("AssetDatabase/ImportExample")]
        static void ImportExample() //导入
        {
            AssetDatabase.ImportAsset("Assets/Texture/wedding.psd", ImportAssetOptions.Default);
        }

        [MenuItem("AssetDatabase/LoadAssetExample")]
        static void LoadAssetExample()  //加载以进行操作
        {
            Texture2D t = AssetDatabase.LoadAssetAtPath("Assets/Texture/wedding.psd", typeof(Texture2D)) as Texture2D;
        }

        [MenuItem("AssetDatabase/IOExample")]
        static void IOExample()
        {
            string ret;

            //创建
            Material material = new Material(Shader.Find("Standard"));
            AssetDatabase.CreateAsset(material, "Assets/Material/MyMaterial.mat");
            if (AssetDatabase.Contains(material))
            {
                Debug.Log("Material asset created");
            }

            //重命名
            ret = AssetDatabase.RenameAsset("Assets/Material/MyMaterial.mat", "NewMaterial.mat");
            if (ret == "")
            {
                Debug.Log("Material renamed to NewMaterial.mat");
            }
            else
            {
                Debug.Log(ret);
            }

            //创建文件夹
            ret = AssetDatabase.CreateFolder("Assets", "NewFolder");
            if (AssetDatabase.GUIDToAssetPath(ret) != "")
            {
                Debug.Log("Folder asset created");
            }
            else
            {
                Debug.Log("Count't find the GUID for the path");
            }

            //移动
            ret = AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(material), "Assets/NewFolder/MyMaterialNew.mat");
            if (ret == "")
            {
                Debug.Log("Material asset moved to NewFolder/MyMaterialNew.mat");
            }
            else
            {
                Debug.Log(ret);
            }

            //复制
            if (AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(material), "Assets/Material/MyMaterialCopy.mat"))
            {
                Debug.Log("Material asset copied as MyMaterialCopy.mat");
            }
            else
            {
                Debug.Log("Count't copy the material");
            }

            //刷新
            AssetDatabase.Refresh();

            //删除到回收站
            Material materialCopy = AssetDatabase.LoadAssetAtPath("Assets/Material/MyMaterialCopy.mat", typeof(Material)) as Material;
            if (AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(materialCopy)))
            {
                Debug.Log("MaterialCopy asset moved to trash");
            }

            //删除
            if (AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(materialCopy)))
            {
                Debug.Log("Material asset deleted");
            }
            if (AssetDatabase.DeleteAsset("Assets/NewFolder"))
            {
                Debug.Log("NewFolder deleted");
            }

            AssetDatabase.Refresh();
        }
    }


## AssetBundles ##
### 基本概念 ###
- AssetBundles作为整体下载
- AssetBundles下载好后，可以加载其中的一部分Assets
- Assets可以依赖其他Assets
- Assets可以共享Assets
- AssetBundle会占用空间和资源
- AssetBundles应该为每个目标平台各编译一次

> AssetBundles不能包含scripts

### 使用步骤 ###
1. Organizing & Setting-up AssetBundles in the editor.
2. Building AssetBundles.
3. Uploading AssetBundles to external storage.
4. Downloading AssetBundles at run-time.
5. Loading objects from AssetBundles.

### AssetBundle Variants ###
左边是AssetBundle Name，右边是Variants Name

![](http://unity3d.com/sites/default/files/assetbundlename.png)

在不同分辨率、语言等情况使用

![](http://unity3d.com/sites/default/files/matching_variant_structure.png)

分别配置两个文件夹，AssetBundle Name相同，而Variants Name不同，注意其中的文件夹结构完全相同

![](http://unity3d.com/sites/default/files/variant_name_hd.png)

![](http://unity3d.com/sites/default/files/variant_name_sd.png)

> AssetBundle Name通过斜杠形成菜单结构: variant/myassets

![](http://unity3d.com/sites/default/files/ab-variant-menu2.png)

### AssetBundle Manager ###
下载地址:https://www.assetstore.unity3d.com/en/#!/content/45836

![](http://unity3d.com/sites/default/files/assetbundle-menu.png)
![](http://unity3d.com/sites/default/files/assetbundle_menu_item.png)


#### Simulation Mode ####
不用编译就能模拟AssetBundles

> Simulation Mode不支持AssetBundle Variants，可以编译后使用Local Asset Server

#### Local Asset Server ####
![](http://unity3d.com/sites/default/files/assetbundles_folder.png)

编译后的本地文件夹

![](http://unity3d.com/sites/default/files/grouped_by_target.png)

> 使用时必须关闭Simulation Mode


## Resources Folder ##
放在Resources文件夹中的资源不管用不用，在编译时都会被打包进去，加载时可能会掉帧。

todo::做一个黑暗之魂加载场景页面



## 通过代码修改资源 ##
### 临时修改 ###
    private var invincibleShader = Shader.Find ("Specular");

    function StartInvincibility {
        renderer.material.shader = invincibleShader;
    }
    
### 永久修改 ###
    private var invincibleShader = Shader.Find ("Specular");

    function StartInvincibility {
        renderer.sharedMaterial.shader = invincibleShader;
    }
    
### 适用对象 ###
- Materials: renderer.material 和 renderer.sharedMaterial
- Meshes: meshFilter.mesh 和 meshFilter.sharedMesh
- Physic Materials: collider.material 和 collider.sharedMaterial

### 默认永久修改的对象 ###
下面的对象修改时要谨慎，因为它们都是永久修改的

- Texture2D
- TerrainData

-- todo::还没有测试过



## Build Player Pipeline ##
### 示例 ###
    using UnityEditor;
    using System.Diagnostics;

    public class ScriptBatch 
    {
        [MenuItem("MyTools/Windows Build With Postprocess")]
        public static void BuildGame ()
        {
            // Get filename.
            string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
            string[] levels = new string[] {"Assets/Scene1.unity", "Assets/Scene2.unity"};

            // Build player.
            BuildPipeline.BuildPlayer(levels, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

            // Copy a file from the project folder to the build folder, alongside the built game.
            FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "/Readme.txt");

            // Run the game (Process class from System.Diagnostics).
            Process proc = new Process();
            proc.StartInfo.FileName = path + "BuiltGame.exe";
            proc.Start();
        }
    }
    
> Mac上把exe改为app即可

### PostProcessBuildAttribute ###
-- todo::还没看



## 减少包的大小 ##
### Editor Log ###
![](http://docs.unity3d.com/uploads/Main/FileSizeOptimization.png)

- 通常textures、music、videos会占用大部分空间
- 而scripts、levels、shaders占用很少

资源大小分析，在Console窗口右上角打开。

- Unity会重新编码导入的资源，所以直接使用带图层的PS格式，不用导出为PNG
- Unity会Build时会移除没有用到的资源，除了scripts(因为它们很小)和Resources文件夹，可以使用AssetBundles加载替换Resources文件夹中的内容来减少大小

### 建议 ###
#### Textures ####
![](http://docs.unity3d.com/uploads/Main/FileSizeOptimizationTexture.png)

> 近看物体贴图是否模糊来确定Max Size

#### Meshes and Animations ####
- mesh compression 仅仅减少了数据文件大小而不会减少运行时占用的内存大小 
- Animation keyframe reduction 都减少了，通常应该打开这个功能

-- todo::试着看看mesh是否像上面说的那样

-- todo::试着导入个动画看看

#### DLLs ####
![](http://docs.unity3d.com/uploads/Main/FileSizeMonoDependency.png)

Unity默认不包括System.dll和System.Xml.dll除非使用了其中的类，作为代替可以使用Mono.Xml.zip解析XML。Stack<>在System.dll中，所以尽量避免使用。

> 个人觉得还是使用System比较方便


#### 减少一些手机上.NET类库大小 ####
Edit - Project Settings - Player - Api Compatibility Level

.NET 2.0 VS NET 2.0 Subset



## Plugins ##
todo::还没看



## Scene的文本格式 ##
### 基础 ###
![](http://docs.unity3d.com/uploads/Main/EditorSettings.png)

以文本方式保存场景：Edit - Project Settings - Editor - Asset Serialization - Mode

YAML Class ID Reference：https://docs.unity3d.com/Manual/ClassIDReference.html

> 自定义的类(MonoBehaviour)ID为114

### 示例 ###
包含一个Camera和Cube的Scene

    %YAML 1.1
	%TAG !u! tag:unity3d.com,2011:
	--- !u!header
	SerializedFile:
	  m_TargetPlatform: 4294967294
	  m_UserInformation: 
	--- !u!29 &1
	Scene:
	  m_ObjectHideFlags: 0
	  m_PVSData: 
	  m_QueryMode: 1
	  m_PVSObjectsArray: []
	  m_PVSPortalsArray: []
	  m_ViewCellSize: 1.000000
	--- !u!104 &2
	RenderSettings:
	  m_Fog: 0
	  m_FogColor: {r: 0.500000, g: 0.500000, b: 0.500000, a: 1.000000}
	  m_FogMode: 3
	  m_FogDensity: 0.010000
	  m_LinearFogStart: 0.000000
	  m_LinearFogEnd: 300.000000
	  m_AmbientLight: {r: 0.200000, g: 0.200000, b: 0.200000, a: 1.000000}
	  m_SkyboxMaterial: {fileID: 0}
	  m_HaloStrength: 0.500000
	  m_FlareStrength: 1.000000
	  m_HaloTexture: {fileID: 0}
	  m_SpotCookie: {fileID: 0}
	  m_ObjectHideFlags: 0
	--- !u!127 &3
	GameManager:
	  m_ObjectHideFlags: 0
	--- !u!157 &4
	LightmapSettings:
	  m_ObjectHideFlags: 0
	  m_LightProbeCloud: {fileID: 0}
	  m_Lightmaps: []
	  m_LightmapsMode: 1
	  m_BakedColorSpace: 0
	  m_UseDualLightmapsInForward: 0
	  m_LightmapEditorSettings:
	    m_Resolution: 50.000000
	    m_LastUsedResolution: 0.000000
	    m_TextureWidth: 1024
	    m_TextureHeight: 1024
	    m_BounceBoost: 1.000000
	    m_BounceIntensity: 1.000000
	    m_SkyLightColor: {r: 0.860000, g: 0.930000, b: 1.000000, a: 1.000000}
	    m_SkyLightIntensity: 0.000000
	    m_Quality: 0
	    m_Bounces: 1
	    m_FinalGatherRays: 1000
	    m_FinalGatherContrastThreshold: 0.050000
	    m_FinalGatherGradientThreshold: 0.000000
	    m_FinalGatherInterpolationPoints: 15
	    m_AOAmount: 0.000000
	    m_AOMaxDistance: 0.100000
	    m_AOContrast: 1.000000
	    m_TextureCompression: 0
	    m_LockAtlas: 0
	--- !u!196 &5
	NavMeshSettings:
	  m_ObjectHideFlags: 0
	  m_BuildSettings:
	    cellSize: 0.200000
	    cellHeight: 0.100000
	    agentSlope: 45.000000
	    agentClimb: 0.900000
	    ledgeDropHeight: 0.000000
	    maxJumpAcrossDistance: 0.000000
	    agentRadius: 0.400000
	    agentHeight: 1.800000
	    maxEdgeLength: 12
	    maxSimplificationError: 1.300000
	    regionMinSize: 8
	    regionMergeSize: 20
	    tileSize: 500
	    detailSampleDistance: 6.000000
	    detailSampleMaxError: 1.000000
	    accuratePlacement: 0
	  m_NavMesh: {fileID: 0}
	--- !u!1 &6
	GameObject:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  importerVersion: 3
	  m_Component:
	  - 4: {fileID: 8}
	  - 33: {fileID: 12}
	  - 65: {fileID: 13}
	  - 23: {fileID: 11}
	  m_Layer: 0
	  m_Name: Cube
	  m_TagString: Untagged
	  m_Icon: {fileID: 0}
	  m_NavMeshLayer: 0
	  m_StaticEditorFlags: 0
	  m_IsActive: 1
	--- !u!1 &7
	GameObject:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  importerVersion: 3
	  m_Component:
	  - 4: {fileID: 9}
	  - 20: {fileID: 10}
	  - 92: {fileID: 15}
	  - 124: {fileID: 16}
	  - 81: {fileID: 14}
	  m_Layer: 0
	  m_Name: Main Camera
	  m_TagString: MainCamera
	  m_Icon: {fileID: 0}
	  m_NavMeshLayer: 0
	  m_StaticEditorFlags: 0
	  m_IsActive: 1
	--- !u!4 &8
	Transform:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 6}
	  m_LocalRotation: {x: 0.000000, y: 0.000000, z: 0.000000, w: 1.000000}
	  m_LocalPosition: {x: -2.618721, y: 1.028581, z: 1.131627}
	  m_LocalScale: {x: 1.000000, y: 1.000000, z: 1.000000}
	  m_Children: []
	  m_Father: {fileID: 0}
	--- !u!4 &9
	Transform:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 7}
	  m_LocalRotation: {x: 0.000000, y: 0.000000, z: 0.000000, w: 1.000000}
	  m_LocalPosition: {x: 0.000000, y: 1.000000, z: -10.000000}
	  m_LocalScale: {x: 1.000000, y: 1.000000, z: 1.000000}
	  m_Children: []
	  m_Father: {fileID: 0}
	--- !u!20 &10
	Camera:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 7}
	  m_Enabled: 1
	  importerVersion: 2
	  m_ClearFlags: 1
	  m_BackGroundColor: {r: 0.192157, g: 0.301961, b: 0.474510, a: 0.019608}
	  m_NormalizedViewPortRect:
	    importerVersion: 2
	    x: 0.000000
	    y: 0.000000
	    width: 1.000000
	    height: 1.000000
	  near clip plane: 0.300000
	  far clip plane: 1000.000000
	  field of view: 60.000000
	  orthographic: 0
	  orthographic size: 100.000000
	  m_Depth: -1.000000
	  m_CullingMask:
	    importerVersion: 2
	    m_Bits: 4294967295
	  m_RenderingPath: -1
	  m_TargetTexture: {fileID: 0}
	  m_HDR: 0
	--- !u!23 &11
	Renderer:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 6}
	  m_Enabled: 1
	  m_CastShadows: 1
	  m_ReceiveShadows: 1
	  m_LightmapIndex: 255
	  m_LightmapTilingOffset: {x: 1.000000, y: 1.000000, z: 0.000000, w: 0.000000}
	  m_Materials:
	  - {fileID: 10302, guid: 0000000000000000e000000000000000, type: 0}
	  m_SubsetIndices: 
	  m_StaticBatchRoot: {fileID: 0}
	  m_LightProbeAnchor: {fileID: 0}
	  m_UseLightProbes: 0
	  m_ScaleInLightmap: 1.000000
	--- !u!33 &12
	MeshFilter:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 6}
	  m_Mesh: {fileID: 10202, guid: 0000000000000000e000000000000000, type: 0}
	--- !u!65 &13
	BoxCollider:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 6}
	  m_Material: {fileID: 0}
	  m_IsTrigger: 0
	  m_Enabled: 1
	  importerVersion: 2
	  m_Size: {x: 1.000000, y: 1.000000, z: 1.000000}
	  m_Center: {x: 0.000000, y: 0.000000, z: 0.000000}
	--- !u!81 &14
	AudioListener:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 7}
	  m_Enabled: 1
	--- !u!92 &15
	Behaviour:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 7}
	  m_Enabled: 1
	--- !u!124 &16
	Behaviour:
	  m_ObjectHideFlags: 0
	  m_PrefabParentObject: {fileID: 0}
	  m_PrefabInternal: {fileID: 0}
	  m_GameObject: {fileID: 7}
	  m_Enabled: 1
	--- !u!1026 &17
	HierarchyState:
	  m_ObjectHideFlags: 0
	  expanded: []
	  selection: []
	  scrollposition_x: 0.000000
	  scrollposition_y: 0.000000
	  
	  
## 目录结构 ##
### 资源 ###
- 3rd-Party
- Animations
- Audio
 - Music
 - SFX
- Materials
- Models
- Plugins
- Prefabs
- Resources
- Textures
- Sandbox
- Scenes
 - Levels
 - Other
- Scripts
 - Editor
- Shaders

注意点：
1. 根目录不要放任何资源
2. 不要移动Model自动生成的Material
3. 3rd-Party放Asset Store中的文件
4. Sandbox放些实验室功能
