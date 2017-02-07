### Navigation System in Unity ###
![](https://docs.unity3d.com/uploads/Main/NavMeshOverview.svg)

#### Walkable Areas ####
![](https://docs.unity3d.com/uploads/Main/NavMeshUnderstandingPath.svg)

#### Following the Path ####
![](https://docs.unity3d.com/uploads/Main/NavMeshUnderstandingCorridor.svg)

#### Global and Local ####
![](https://docs.unity3d.com/uploads/Main/NavMeshUnderstandingLoop.svg)

#### Two Cases for Obstacles ####
![](https://docs.unity3d.com/uploads/Main/NavMeshUnderstandingCarve.svg)

#### Describing Off-mesh Links ####
![](https://docs.unity3d.com/uploads/Main/NavMeshUnderstandingOffmesh.svg)


### Building a NavMesh ###
#### 基本步骤 ####
![](https://docs.unity3d.com/uploads/Main/NavMeshSetupObject.svg)

![](https://docs.unity3d.com/uploads/Main/NavMeshSetupBake.svg)


#### Min Region Area ####
![](https://docs.unity3d.com/uploads/Main/NavMeshMinRegion.svg)

#### Voxel Size ####
The default accuracy is set so that there are 3 voxels per agent radius, that is, the whole agent width is 6 voxels. This is a good trade off between accuracy and bake speed. Halving the voxel size will increase the memory usage by 4x and it will take 4x longer to build the scene.

![](https://docs.unity3d.com/uploads/Main/NavMeshVoxelSize.svg)

Generally you should not need to adjust the voxel size, there are two scenarios where this might be necessary: building a smaller agent radius, or more accurate NavMesh.

#### Building Height Mesh for Accurate Character Placement ####
![](https://docs.unity3d.com/uploads/Main/NavMeshHeightMesh.svg)

For example, stairs may appear as a slope in the NavMesh. If your game requires accurate placement of the agent, you should enable Height Mesh building when you bake the NavMesh.

#### Navigation Areas and Costs ####
![](https://docs.unity3d.com/uploads/Main/NavMeshAreaType.svg)

- Water area is made more costly to walk by assigning it a higher cost, to deal with a scenario where walking on shallow water is slower.注意仅仅是权重，不影响速度
- Door area is made accessible by specific characters, to create a scenario where humans can walk through doors, but zombies cannot.

##### Area Types #####
![](https://docs.unity3d.com/uploads/Main/NavMeshAreaTypeList.png)

The cost to move between two nodes depends on the distance to travel and the cost associated with the area type of the polygon under the link, that is, distance * cost.



### Creating a NavMesh Agent ###
#### 基本 ####
![](https://docs.unity3d.com/uploads/Main/NavMeshAgentSetup.svg)

    //移动到指定地点
    using UnityEngine;
    using System.Collections;
    
    public class MoveTo : MonoBehaviour {
       
       public Transform goal;
       
       void Start () {
          NavMeshAgent agent = GetComponent<NavMeshAgent>();
          agent.destination = goal.position; 
       }
    }
    
    
    //移动到鼠标点击点
    using UnityEngine;
    using System.Collections;

    public class MoveToClickPoint : MonoBehaviour {
        
        NavMeshAgent agent;

        // Use this for initialization
        void Start () {
            agent = GetComponent<NavMeshAgent>();
        }
        
        // Update is called once per frame
        void Update () {
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,100)){
                    agent.destination = hit.point;
                }
            }
        }
    }
    
    //巡逻
    using UnityEngine;
    using System.Collections;

    public class Patrol : MonoBehaviour {
        
        public Transform[] points;
        private int destPoint = 0;
        private NavMeshAgent agent;

        // Use this for initialization
        void Start () {
            agent = GetComponent<NavMeshAgent>();
            agent.autoBraking = false;
            
            GotoNextPoint();
        }
        
        void GotoNextPoint(){
            if(points.Length == 0){
                return;
            }
            
            agent.destination = points[destPoint].position;
            destPoint = (destPoint + 1)%points.Length;
        }
        
        // Update is called once per frame
        void Update () {
            if(agent.remainingDistance < 0.5f){
                GotoNextPoint();
            }
        }
    }



#### 细节 ####
- NavMesh bake settings describe how all the NavMesh Agents are colliding or avoiding the static world geometry. In order to keep memory on budget and CPU load in check, only one size can be specified in the bake settings.
- NavMesh Agent properties values describe how the agent collides with moving obstacles and other agents.

Most often you set the size of the agent the same in both places. But, for example, a heavy soldier may have larger radius, so that other agents will leave more space around him, but otherwise he’ll avoid the environment just the same.

> Auto Braking: 巡逻时不勾选

#### Area Mask ####
![](https://docs.unity3d.com/uploads/Main/NavMeshAreaMask.svg)

For example, in a zombie evasion game, you could mark the area under each door with a Door area type, and uncheck the Door area from the zombie character’s Area Mask.


### Creating a NavMesh Obstacle ###
#### 基本 ####
![](https://docs.unity3d.com/uploads/Main/NavMeshObstacleSetup.svg)

主要用于可移动的障碍物 

#### 细节 ####
![](https://docs.unity3d.com/uploads/Main/NavMeshObstacleCarving.svg)

- Obstructing: This mode is best used in cases where the obstacle is constantly moving, for example a vehicle, or even player character.
- Carving: You should turn on carving for obstacle like crates and barrels which generally block navigation, but can be moved by the player or other game events like explosions.

![](https://docs.unity3d.com/uploads/Main/NavMeshObstacleTrap.svg)

- Carve when stationary: It is good match when the game object is controlled by physics (for example crates and barrels).
- Carve when moved: This mode is well suited for large slowly moving obstacles, for example a tank that is being avoided by infantry.


### Creating an Off-mesh Link ###
#### 手动 ####
![](https://docs.unity3d.com/uploads/Main/OffMeshLinkSetup.svg)

#### 自动 ####
![](https://docs.unity3d.com/uploads/Main/AutoOffMeshLinksSetup.svg)

![](https://docs.unity3d.com/uploads/Main/AutoOffMeshLinksParams.svg)

![](https://docs.unity3d.com/uploads/Main/OffMeshLinkDebug.svg)

> 连接好后会有圆圈.

#### 跨场景 ####
todo::没有测试过

![](https://docs.unity3d.com/uploads/Main/NavMeshLoadAdditiveDiagram.svg)

The NavMeshes in different Scenes are not connect by default. When you load another level using Application.LoadLevelAdditive() you will need to connect the NavMeshes in different Scenes using an Off-Mesh link.

![](https://docs.unity3d.com/uploads/Main/NavMeshLoadAdditiveTrouble.svg)



### Using NavMesh Agent with Other Components ###
很复杂，看todo::https://docs.unity3d.com/Manual/nav-MixingComponents.html

#### Coupling Animation and Navigation ####
也很复杂，看todo::https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
