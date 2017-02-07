## Scripting ##
A script 继承自MonoBehaviour

### Accessing Other Objects ###
#### Linking Objects with Variables ####
非运行时，必须在Unity editor中操作

	public class Enemy : MonoBehaviour {
	    public GameObject player;
	}

![](http://docs.unity3d.com/uploads/Main/GameObjectPublicVar.png)

#### Finding Child Objects ####
	using UnityEngine;
	
	public class WaypointManager : MonoBehaviour {
	    public Transform[] waypoints;
	    
	    void Start() {
	        waypoints = new Transform[transform.childCount];
	        int i = 0;
	        
	        foreach (Transform t in transform) {
	            waypoints[i++] = t;
	        }
	    }
	}
You can also locate a specific child object by name using the Transform.Find function:

	transform.Find("Gun");

#### Finding Objects by Name or Tag ####
	GameObject player;
	GameObject[] enemies;
	
	void Start() {
		//player = GameObject.Find("MainHeroCharacter");
	    player = GameObject.FindWithTag("Player");
	    enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

### Event Functions ###
#### Regular Update Events ####
##### Update #####
	void Update() {		//执行间隔取决于渲染帧的时间
	    float distance = speed * Time.deltaTime * Input.GetAxis("Horizontal");
	    transform.Translate(Vector3.right * distance);
	}
##### FixedUpdate #####
	void FixedUpdate() {
	    Vector3 force = transform.forward * driveForce * Input.GetAxis("Vertical");
	    rigidbody.AddForce(force);
	}
##### LateUpdate #####
	void LateUpdate() {
	    Camera.main.transform.LookAt(target.transform);
	}

#### Initialization Events ####
Awake is always called before any Start functions. 

The Start function is called before the first frame or physics update on an object. 

The Awake function is called for each object in the scene at the time when the scene loads.

如果游戏对象没有开启，Awake和Start都不会执行

如果游戏对象开启而组件没有开启，Awake仍然会执行，而Start不会

一般awake可以看成是构造函数的代替物，只负责自己的初始化工作，不干涉别人；到start里去访问别人会更安全些。

#### GUI events ####
	void OnGUI() {
	    GUI.Label(labelRect, "Game Over");
	} 

A set of OnMouseXXX event functions (eg, OnMouseOver, OnMouseDown) is available to allow a script to react to user actions with the mouse. 

#### Physics events ####
The OnCollisionEnter, OnCollisionStay and OnCollisionExit functions will be called as contact is made, held and broken. 

The corresponding OnTriggerEnter, OnTriggerStay and OnTriggerExit functions will be called when the object’s collider is configured as a Trigger.


### Time and Framerate Management ###
#### Main Frame Update ####
    public float distancePerSecond;
    
    void Update() {
        transform.Translate(0, 0, distancePerSecond * Time.deltaTime);
    }

#### Fixed Timestep ####
##### 设置 #####
执行间隔不变，Edit > Project Settings > Time - Fixed Timestep

![](http://docs.unity3d.com/uploads/Main/TimeSet.png)

> 一般用默认的就行，不建议改
##### 获取 #####
	void Start () {
        Debug.Log("Time.fixedDeltaTime = " + Time.fixedDeltaTime);
	}

#### Maximum Allowed Timestep ####
Edit > Project Settings > Time - Maximum Allowed Timestep

todo::需要好好看看，或者不看

#### Time Scale ####
> 可以用来做子弹时间效果

不影响GUI

#### Capture Framerate ####
屏幕连续截图当作录像时使用，可以去除截图对性能的影响

	string folder = "ScreenshotFolder";
    int frameRate = 25;

	void Start () {
        Time.captureFramerate = frameRate;
        System.IO.Directory.CreateDirectory(folder);
	}
	
	void Update () {
        string name = string.Format("{0}/{1:D04}.png", folder, Time.frameCount);
        Application.CaptureScreenshot(name);
	}


### Coroutines ###
#### yield return ####
yield return会使函数执行在当前帧暂停，并在下一帧时继续执行

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine("Fade");
        }
	}

    IEnumerator Fade() {
        for (int i = 1; i < 50; i++) {
            transform.position += new Vector3(0, 0, 1);
            yield return null;
        }
    }

#### WaitForSeconds ####
默认情况下，协同是每帧执行一次，但是也可以每间隔一段指定时间执行一次

	IEnumerator Fade() {
        for (int i = 1; i < 50; i++) {
            transform.position += new Vector3(0, 0, 1);
            yield return new WaitForSeconds(1);
        }
    }

> 一个常见的用法，检测敌人是否接近，不必要每帧检测，可能1秒执行一次就够了

#### 异步加载场景 ####
> 在被加载的场景中开始时不能使用Instantiate

	using UnityEngine;
	using UnityEngine.UI;
	using System.Collections;
	
	public class LoadingController : MonoBehaviour {
	
	    public Slider progressbar;
	
	    private AsyncOperation async;
	    private float currentProgress;
	
	
	    void Start()
	    {        
	        StartCoroutine(loadScene());
	    }
	
	    IEnumerator loadScene() {        
	        async = Application.LoadLevelAsync("Level1");        
	        yield return async;
	    }
	
	    
	    void Update () {
	        currentProgress = async.progress;
	        progressbar.value = currentProgress + 0.1f;
		}
	}


### 命名空间 ###
	namespace ScriptWorld {
	    public class Fader : MonoBehaviour {	
	        [HideInInspector]
	        public int life;
	    }
	}

### 执行顺序 ###
todo::

![](http://docs.unity3d.com/uploads/Main/monobehaviour_flowchart.svg)


### 自动内存管理 ###
#### 值类型和引用类型 ####
- 值类型，直接存储并且作为参数时被复制。例如integers, floats, booleans and Unity’s struct types (eg, Color and Vector3). 
- 引用类型，分配在堆中并且通过引用访问。例如objects, strings and arrays.

#### 代码优化 ####
##### 频繁更新String #####
	using UnityEngine;
	using System.Collections;
	
	public class ExampleScript : MonoBehaviour {
	    public GUIText scoreBoard;
	    public string scoreText;
	    public int score;
	    public int oldScore;
	    
	    void Update() {
	        if (score != oldScore) {
	            scoreText = "Score: " + score.ToString(); //使用System.Text中的StringBuilder更佳
	            scoreBoard.text = scoreText;
	            oldScore = score;
	        }
	    }
	}

##### 生成随机数数组 #####
	using UnityEngine;
	using System.Collections;
	
	public class ExampleScript : MonoBehaviour {
	    void RandomList(float[] arrayToFill) {
	        for (int i = 0; i < arrayToFill.Length; i++) {
	            arrayToFill[i] = Random.value;
	        }
	    }
	}

#### 调用内存管理方法 ####
谨慎使用，并且查看profiler statistics以确定确实对游戏性能有提升

##### Small heap with fast and frequent garbage collection #####
	if (Time.frameCount % 30 == 0)
	{
	   System.GC.Collect();
	}

##### Large heap with slow but infrequent garbage collection #####
	using UnityEngine;
	using System.Collections;
	
	public class ExampleScript : MonoBehaviour {
	    void Start() {
	        var tmp = new System.Object[1024];
	        
	        // make allocations in smaller blocks to avoid them to be treated in a special way, which is designed for large blocks
	        for (int i = 0; i < 1024; i++)
	            tmp[i] = new byte[1024];
	        
	        // release reference
	        tmp = null;
	    }
	}

#### 对象池 ####
像炮弹这样的，可以使用对象池

### 不同平台 ###
#### 预编译 ####
	void Start () {
		Debug.Log("Current platform " + Application.platform);
	
		#if UNITY_EDITOR
		  Debug.Log("Unity Editor");
		#endif	
			
		#if UNITY_STANDALONE_WIN
		  Debug.Log("Stand Alone Windows");
		#endif
	}

#### 自定义 ####
[Edit]-[Project Settings]-[Player]-[Other Settings]

![](http://docs.unity3d.com/uploads/Main/ScriptDefines.png)

### 序列化 ###
#### 规则 ####
> 序列化后可以在Inspector中显示

#### 规则 ####
- 自定义类前加[System.Serializable]
- public字段默认被序列化，除了constants、static、readonly或者添加了[System.NonSerialized];
- private字段默认不被序列化，除非添加了[SerializeField]
- 如果想序列化，但是不想在Inspector面板中显示，使用[HideInInspector]

#### 示例 ####
	using UnityEngine;
	using System.Collections;
	
	[System.Serializable]
	public class Foobar{	
	    public int foo;
	    public int bar;
	}

使用System.Serializable添加到自定义类上

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	
	public class SerializeDemo : MonoBehaviour {
	
	    //会序列化
	    public int maxExp;
	    public string roleName;
	    public Foobar foobar;
    
	    [SerializeField]
	    private int currentExp;

		[HideInInspector]
    	public float pi = 3.14f;

	    public List<string> friendNames;
	    
	
	    //不会序列化
	    [System.NonSerialized]
	    public int maxHp;

	    public static int enemyCount;
	    public Dictionary<int, string> rankInfo;
	
	    //todo::可能有问题
	    public List<Foobar> foobars;
	}

#### ScriptableObject ####
1. 在play时做的更改退出后依然生效
2. 有OnEnable、OnDisable、OnDestroy方法

> 可以用作配置文件，不能用作游戏存档，因为打包后这个文件就无法修改了

##### 购物车 #####
ShopConfig.cs

	using UnityEngine;
	using System.Collections.Generic;
	
	[CreateAssetMenu]	//会在Assets/Create中出现
	public class ShopConfig : ScriptableObject {
	
	    public enum ShopTag {
	        hot,
	        item,
	        weapon
	    }
	
	    public List<ShopListInfo> ShopList;
	}

ShopListInfo.cs

	using UnityEngine;
	using System.Collections.Generic;
	
	[System.Serializable]
	public class ShopListInfo{
	
	    public ShopConfig.ShopTag tag;
	    public List<ShopItemInfo> list;
	}

ShopItemInfo.cs

	using UnityEngine;
	using System.Collections;
	
	[System.Serializable]
	public class ShopItemInfo{
	
	    public string name;
	    public int price;    
	}

ShopTest.cs

	using UnityEngine;
	using System.Collections.Generic;
	
	public class ShopTest : MonoBehaviour {
	
	    public ShopConfig shopConfig;
	
		void Start () {
	        Debug.Log("count " + shopConfig.ShopList.Count);
	
	        ShopListInfo shopListInfo = new ShopListInfo();
	        shopListInfo.tag = ShopConfig.ShopTag.hot;
	
	        ShopItemInfo shopItemInfo = new ShopItemInfo();
	        shopItemInfo.name = "newItem";
	        shopItemInfo.price = 600;
	        shopListInfo.list = new List<ShopItemInfo>();
	        shopListInfo.list.Add(shopItemInfo);
	
	        shopConfig.ShopList.Add(shopListInfo);
		}
	}


#### JSON ####
##### 简单使用 #####
	[Serializable]
	public class MyClass
	{
		public int level;
		public float timeElapsed;
		public string playerName;
	}

	MyClass myObject = new MyClass();
	myObject.level = 1;
	myObject.timeElapsed = 47.5f;
	myObject.playerName = "Dr Charles Francis";

	string json = JsonUtility.ToJson(myObject);	//get {"level":1,"timeElapsed":47.5,"playerName":"Dr Charles Francis"}
	myObject = JsonUtility.FromJson<MyClass>(json);

##### 覆盖对象 #####
将json数据加载到已存在的对象中，如果一个字段没有，对象中对应的字段会保持不变

	JsonUtility.FromJsonOverwrite(json, myObject);
	
> 将反序列化到MonoBehaviour或ScriptableObject时, 必须使用FromJsonOverwrite，不能使用FromJson

todo::EditorJsonUtility




### 事件 ###
#### 不带参数 ####
	using UnityEngine;
	using System.Collections;
	using UnityEngine.Events;

	public class EventDemo : MonoBehaviour {
	
	    public UnityEvent mycallback;
	
		void Update () {
	        if (Input.anyKeyDown) {
	            mycallback.Invoke();
	        }
	    }
	}

#### 带参数 ####
	using UnityEngine;
	using System.Collections;
	using UnityEngine.Events;
	
	[System.Serializable]
	public class FloatEvent : UnityEvent<float> {	
		
	}

自定义一个类，参数的值可以在Inspector中设置

	using UnityEngine;
	using System.Collections;
	using UnityEngine.Events;
	
	public class EventDemo : MonoBehaviour {
	
	    public FloatEvent mycallback;
	
		void Update () {
	        if (Input.anyKeyDown) {
	            mycallback.Invoke(0.5f);
	        }
	    }
	}

### 事件系统 ###
总的来说，EventSystem负责管理，BaseInputModule负责输入，BaseRaycaster负责确定目标对象，目标对象负责接收事件并处理，然后一个完整的事件系统就有了。

#### 消息系统 ####
ICustomMessageTarget.cs

	using UnityEngine;
	using System.Collections;
	using UnityEngine.EventSystems;
	
	public interface ICustomMessageTarget : IEventSystemHandler {
	
	    void Message1();
	    void Message2();
	}

CustomMessageTarget.cs
	
	using UnityEngine;
	using System.Collections;
	using System;
	
	public class CustomMessageTarget : MonoBehaviour,ICustomMessageTarget {
	
	    public void Message1()
	    {
	        Debug.Log("Message 1 received");
	    }
	
	    public void Message2()
	    {
	        Debug.Log("Message 2 received");
	    }    
	}

MessageDemo.cs

	using UnityEngine;
	using System.Collections;
	using UnityEngine.EventSystems;
	
	public class MessageDemo : MonoBehaviour {
	
	    public GameObject target;	
		
		void Update () {
	        if (Input.GetKeyDown(KeyCode.Space)) {
	            ExecuteEvents.Execute<ICustomMessageTarget>(target, null, (x, y) => x.Message1());
	        }
		}
	}

将MessageDemo.cs和CustomMessageTarget.cs分别附给obj1和obj2，设置MessageDemo的target为obj2

#### 使用方法 ####
1. 摄像机添加Physics Raycaster组件
2. 添加空的Game Object，并且添加Event System和Standalone Input Module组件
3. 在要在点击的Game Object上，添加实现了IPointerClickHandler接口的Script，或者添加Event Trigger

> 添加Select事件时，需要在此Game Object上添加Selectable组件


#### EventTrigger动态添加 ####
	using UnityEngine;
	using System.Collections;
	using UnityEngine.Events;
	using UnityEngine.EventSystems;
	
	public class ScriptControl : MonoBehaviour {
	    
		void Start () {
	        var trigger = gameObject.GetComponent<EventTrigger>();
	        if (trigger == null) {
	            trigger = gameObject.AddComponent<EventTrigger>();
	        }
	
	        EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
	        triggerEvent.AddListener(new UnityAction<BaseEventData>(handleClick));
	
	        EventTrigger.Entry entry = new EventTrigger.Entry();
	        entry.eventID = EventTriggerType.PointerClick;
	        entry.callback = triggerEvent;
	
	        trigger.triggers.Add(entry);
		}
	
	    void handleClick(BaseEventData data) {
	        Debug.Log("ScriptControl handleClick");
	    }
	}

> 针对Click事件还存在一种特殊方式：添加uGUI系统中的Button组件，设置On Click事件
#### Button动态添加 ####
	using UnityEngine;
	using UnityEngine.UI;
	
	public class BtnControl : MonoBehaviour {
	
		void Start () {
	        var button = gameObject.GetComponent<Button>();
	        if (button == null) {
	            button = gameObject.AddComponent<Button>();
	        }
	        
	        button.onClick.RemoveAllListeners();
	        button.onClick.AddListener(TestClick);
		}
	
	    public void TestClick() {
	        Debug.Log("BtnControl TestClick");
	    }
	}
	
	
	
### 空引用 ###
	public class Star : MonoBehaviour
	{
		[SerializeField]
		private Text scoreText;

		void OnEnable() {
			Assert.IsNotNull(scoreText, "ScoreText is not set!");
		}
	}
	

> RequireComponent不能作为防止空引用的方法，因为它会在为空时自动创建一个对象

	[RequireComponent(typeof(Renderer))] 
	public class Player : MonoBehaviour
	{
		void Start()
		{
			var renderer = GetComponent<Renderer>();
			renderer.enabled = false;
		}
	}


### 向量 ###
#### 理解向量算法 ####
##### 加 #####
![](http://docs.unity3d.com/uploads/Main/VectorAdd.png)

##### 减 #####
向量相减通常用来计算两个物体间的距离和方向

![](http://docs.unity3d.com/uploads/Main/VectorSubtract.png)

##### 乘除标量 #####
方向不变，大小进行乘除

##### 点乘(Dot Product) #####
运算结果是标量

![](http://docs.unity3d.com/uploads/Main/DotProduct.png)

![](https://upload.wikimedia.org/math/1/8/9/189ee9305ab1c31263968d03d1b3db24.png)

![](https://upload.wikimedia.org/math/e/4/5/e45c9d4961034918dd0ae0b3790a4373.png)

![](https://upload.wikimedia.org/math/0/9/2/09243eaabd696902d133dd2d12eaab76.png)

##### 叉乘(Cross Product) #####
运算结果是向量，与原来的两个向量都垂直。

![](http://docs.unity3d.com/uploads/Main/LeftHandRuleDiagram.png)

![](https://upload.wikimedia.org/math/a/e/8/ae83a053163df8dbb7b460713102a3f0.png)

##### 代码示例 #####
	Vector2 a = new Vector2(3, 5);
	Vector2 b = new Vector2(2, -1);
	Vector2 plus = a + b;	//加
	Vector2 minus = a - b;	//减
	Vector2 scalarMulti = a * 2;	//乘标量
	float dotProduct = Vector2.Dot(a, b);	//点乘
	
	Vector3 m = new Vector3(1, 2, 3);
	Vector3 n = new Vector3(4, 5, 6);
	Vector3 crossProduct = Vector3.Cross(m, n);	//叉乘

#### 两点间的距离和方向 ####
	Vector2 a = new Vector2(0, 0);
	Vector2 b = new Vector2(3, 4);
	Vector2 c = b - a;
	
	Vector2 direction = c.normalized;	//方向向量
	float mag = c.magnitude;	//距离
	float sqrMag = c.sqrMagnitude;	//距离的平方
	
	float maxDistance = 10.0f;
	if (sqrMag < maxDistance * maxDistance) {   //比较超出范围时用平方效率更高
	    Debug.Log("in range");
	}

#### 计算法线向量 ####
	Vector3 a = new Vector3(0, 0, 0);
	Vector3 b = new Vector3(3, 0, 0);
	Vector3 c = new Vector3(3, 0, 5);
	
	Vector3 normal = Vector3.Cross(a - c, a - b);

#### 一个向量在另一个向量方向上的分量 ####
	float forwardSpeed = Vector3.Dot(rigid.velocity, transform.forward);	//汽车的速度


### 系统工具 ###
#### Console Window ####
##### 错误暂停 #####
![](http://docs.unity3d.com/uploads/Main/Console.png)

	Debug.LogError("Console Demo error");   //当Error Pause选中时，执行到此句会暂停

#### 日志堆栈级别 ####
![](http://docs.unity3d.com/uploads/Main/ConsoleStackTrace.png)

#### IL2CPP ####
IL的全称是 Intermediate Language，翻译过来就是中间语言

AOT（Ahead Of Time）编译而非JIT（Just In Time）编译

> 设置方法：Edit - Project Settings - Player - Configuration - Scripting Backend

##### 运行方式 #####
###### Mono ######
![](http://inpla.net/data/attachment/forum/201502/15/184604ciig9bxigy3kkxk7.png)

###### IL2CPP ######
![](http://inpla.net/data/attachment/forum/201502/15/184606ju90jpursw180zrw.png)

##### Compiler Options #####
- Null checks 默认开启
- Array bounds checks 默认开启
- Divide by zero checks 默认关闭

##### 扩展学习
[https://www.youtube.com/watch?v=s7Ple1G83jc](https://www.youtube.com/watch?v=s7Ple1G83jc)

[http://blogs.unity3d.com/2015/05/06/an-introduction-to-ilcpp-internals/](http://blogs.unity3d.com/2015/05/06/an-introduction-to-ilcpp-internals/)

[http://inpla.net/forum-517-1.html](http://inpla.net/forum-517-1.html)(上面那个的翻译版)


#### Editor Test Runner ####
(Assets - Create - Editor Test)

![](https://docs.unity3d.com/uploads/EditorTestsRunner/editor-tests-overview.png)



#### Visual Studio Code配置 ####
##### 1. 安装扩展 #####
F1 Extensions:Install Extension

- C#
- Debugger for Unity

##### 2.配置VS Code #####
File - Preferences - Workspace Settings中添加

	"files.exclude": {
		"**/.git": true,
		"**/.DS_Store": true,
		"**/*.meta": true,
		"**/*.*.meta": true,
		"**/*.unity": true,
		"**/*.unityproj": true,
		"**/*.mat": true,
		"**/*.fbx": true,
		"**/*.FBX": true,
		"**/*.tga": true,
		"**/*.cubemap": true,
		"**/**.prefab": true,
		"**/Library": true,
		"**/ProjectSettings": true,
		"**/Temp": true
	}

##### 3.开启Debug #####
![](https://github.com/Unity-Technologies/vscode-unity-debug/raw/master/Screenshots/vscode-debugger-list.png)