# WarringStatesDev

- 由于Avalonia-OpenGL的性能存在问题，因此前端移至Godot开发，后续逐步整合后端

## WarringStates.Map

- 特性：
  - 六边形地图
  - 程序化生成地形
  - C#脚本
  - 3D地图存在高度
  - 俯视 - 正交相机：视野可缩放、可旋转，俯视视角与正交视角可转换
  - 平坦地图：方形延申地图，非球面地图
  - 有限地图：总地块单位数量存在可设置的上限，地图总资源量存在上限
  - 循环地图：地图上下边界、左右边界互相衔接
- Godot 4.4 & .Net8（Godot 4.5-mono 在安卓导出上存在问题，安装包更大，且安装包在模拟器上闪退）
- 参考文档
  - [Unity Hex Map Tutorials](https://catlikecoding.com/unity/tutorials/hex-map/) （Unity项目，功能参考，模型网格生成参考）
- 参考项目
  - [ForlornU/HexagonalMapGodot: Hexagonal Map Generation in Godot](https://github.com/ForlornU/HexagonalMapGodot) （Godot 4.4 & GDScript，地形编辑器架构参考，相机参考）
  - [ZeromaXHe/ZeromaX-s-Playground](https://github.com/ZeromaXHe/ZeromaX-s-Playground) （Godot 4.4 & C# & F#，非平坦地形，节点组织参考，开发经验参考）
  - [davepruitt/hex_map_godot: Hexagonal-based maps in Godot](https://github.com/davepruitt/hex_map_godot) （Godot 4.4 & GDScript，模型网格生成参考，功能实现参考，相机参考）
  - [jmb462/godot_infinite_terrain_generation](https://github.com/jmb462/godot_infinite_terrain_generation) （Godot 3.X & GDScript，无限区块生成参考）
  - [SoloCodeNet/hexagon_simple: simple moise for single chunk](https://github.com/SoloCodeNet/hexagon_simple) （Godot 3.X & GDScript，美术参考）
  - [ForlornU/VoxelsBeta](https://github.com/ForlornU/VoxelsBeta) （Godot 4.5 & GDScript，寻路参考，区块生成参考）
