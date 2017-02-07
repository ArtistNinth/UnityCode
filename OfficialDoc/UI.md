## UI ##

### Canvas Related###

#### Canvas ####
> 嵌套的Canvas被强制使用和它父对象同样的Render Mode


##### 设置UI上下层关系 #####
Transform component: SetAsFirstSibling, SetAsLastSibling, and SetSiblingIndex

##### Render Modes #####
###### Screen Space - Overlay ######
![](http://docs.unity3d.com/uploads/Main/GUI_Canvas_Screenspace_Overlay.png)

![](http://docs.unity3d.com/uploads/Main/CanvasOverlay.png)

平面，以Canvas为变化

###### Screen Space - Camera ######
![](http://docs.unity3d.com/uploads/Main/GUI_Canvas_Screenspace_Camera.png)

![](http://docs.unity3d.com/uploads/Main/CanvasCamera.png)
Canvas在一个给定摄像机的正前面.

斜面，以Canvas为变化

###### World Space ######
![](http://docs.unity3d.com/uploads/Main/GUI_Canvas_Worldspace.png)

![](http://docs.unity3d.com/uploads/Main/CanvasWorldSpace.png)

斜面，以摄像头变化

适合做游戏中的屏幕

> 例如，设置Canvas的Rect Transform

> 位置设置为0，0，0

> Width和Height为800*600

> Scale X和Y为0.005


#### Canvas Scaler ####

![](http://docs.unity3d.com/uploads/Main/UI_CanvasScalerInspector.png)

- UI Scale Mode 一般设置为Scale With Screen Size
- Match 一般设置为0.5

##### Canvas set to ‘Screen Space - Overlay’ or ‘Screen Space - Camera’ #####

- Constant Pixel Size 固定值，不随屏幕分辨率变化

- Scale With Screen Size 随屏幕分辨率自动拉伸

- Constant Physical Size 没研究

##### Canvas set to ‘World Space’ #####
- World Space

#### Canvas Group ####
![](http://docs.unity3d.com/uploads/Main/UI_CanvasGroupInspector.png)

加到一组UI对象的父对象上，统一设置透明度、是否可用等

****

### Basic Layout ###
#### 中枢点 ####
![](http://docs.unity3d.com/uploads/Main/GUI_Pivot_Local_Buttons.png)

When working with UI it’s usually a good idea to keep those set to Pivot and Local.

> 注意：中枢点不是锚点

#### 锚点 ####
![](http://docs.unity3d.com/uploads/Main/UI_Anchored1.gif)
![](http://docs.unity3d.com/uploads/Main/UI_Anchored2.gif)
![](http://docs.unity3d.com/uploads/Main/UI_Anchored3.gif)
![](http://docs.unity3d.com/uploads/Main/UI_Anchored4.gif)

拖动锚点时按住Shift,对应对象大小会跟着变

Raw Mode

****
### Visual Components ###
#### Text ####
![](http://docs.unity3d.com/uploads/Main/UI_TextInspector.png)

- Horizontal Overflow
- Vertical Overflow
- Best Fit	按照字数多少自适应字号


> 中文字体
>
> 国际化

#### Image ####
![](http://docs.unity3d.com/uploads/Main/UI_ImageInspector.png)

Image Type:

Simple - 拉伸

Sliced - 九宫格

Tiled - 重复

Filled - 除了拉伸，还可以显示一部分，比如3/4

![](http://docs.unity3d.com/uploads/Main/UI_SpriteEditor.png)

导入图片时设置九宫格

#### Raw Image ####

除了极少数情况使用Raw Image，一般使用Image.


#### Mask ####

此对象的孩子将只显示父对象的部分。只是显示上，响应区域没有变化。

战神升级技能效果

#### Effects ####

添加阴影、描边效果

****

### Interaction Components ###
#### Selectable Base Class ####
所有interaction components的基类

设置是否可选择，状态(常态、高亮、选中、不可用)转换，不用组件间的键盘导航


#### Toggle ####
![](http://docs.unity3d.com/uploads/Main/UI_ToggleExample.png)

设置是否显示字幕

#### Toggle Group ####
![](http://docs.unity3d.com/uploads/Main/UI_ToggleGroupExample.png)

只能同时选中一个或者都不选

选择职业，游戏速度

#### Slider ####
![](http://docs.unity3d.com/uploads/Main/UI_SliderExample.png)

设置音量

#### Scrollbar ####
![](http://docs.unity3d.com/uploads/Main/UI_ScrollbarExample.png)

一般和ScrollRect、Mask一起组成ScrollView用

游戏内大段文本介绍

#### Dropdown ####
![](http://docs.unity3d.com/uploads/Main/UI_DropdownExample.png)

下拉列表

#### Input Field ####
![](http://docs.unity3d.com/uploads/Main/UI_InputFieldExample.png)

输入姓名、数字、密码等

****

### Animation Integration ###

![](http://docs.unity3d.com/uploads/Main/GUI_ButtonInspectorAnimation.png)

****

### Auto Layout ###

#### Understanding Layout Elements ####
设置最小大小、有足够空间时的大小、权重比例

##### Layout Element Component #####

![](http://docs.unity3d.com/uploads/Main/UI_LayoutElementInspector.png)

网格布局时，此设置无效。


#### Understanding Layout Controllers ####
##### Content Size Fitter #####
![](http://docs.unity3d.com/uploads/Main/UI_ContentSizeFitterInspector.png)

与Layout Element配合，设置大小为Min或Preferred中的一项固定值

也可以添加到一个Text上，设置为Preferred，则大小会根据文本大小数量自动调整，与Text的Best Fit功能正好相反，因为Text实现了ILayoutElement接口。


##### Aspect Ratio Fitter #####
![](http://docs.unity3d.com/uploads/Main/UI_AspectRatioFitterInspector.png)

添加到Rect Transform上，设置宽高比例，可以让宽控制高或相反，或者按比例自动适应父对象

##### Layout Groups #####

有水平、竖直、网格布局

Button有Text子对象，为了让Button自适应Text的大小，可以为它添加Horizontal Layout Group和Content Size Fitter，然后设置Horizontal Fit和the Vertical Fit为Preferred Size。因为Horizontal Layout Group实现了 ILayoutElement接口


#### Technical Details ####
##### Layout Interfaces #####


- 实现ILayoutElement接口来定义Layout Element.

- 实现ILayoutGroup接口来定义可以控制子对象大小的组

- 实现ILayoutSelfController接口来控制自身的大小

##### Layout Calculations #####
1. CalculateLayoutInputHorizontal on ILayoutElement 先子后父

2. SetLayoutHorizontal on ILayoutController 先父后子

先计算宽再计算高，因此，高可以依赖宽，但宽肯定不会依赖高。

##### Triggering Layout Rebuild #####
LayoutRebuilder.MarkLayoutForRebuild (transform as RectTransform);

The rebuild will not happen immediately, but at the end of the current frame, just before rendering happens.

Guidelines for when a rebuild should be triggered:

In setters for properties that can change the layout.

****
### Rich Text ###
Text组件上打上勾，可以显示富文本

	We are <b>not</b> amused
We are <b>not</b> amused

	We are <b><i>definitely not</i></b> amused
We are <b><i>definitely not</i></b> amused

	We are <b>absolutely <i>definitely</i> not</b> amused
We are <b>absolutely <i>definitely</i> not</b> amused

    We are <color=green>green</color> with envy

