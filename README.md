# SkyTakeout_WinForm

基于 WinForms 的“苍穹外卖”实训项目（.NET Framework 4.8）。

## 运行环境

- Windows
- Visual Studio（建议 2019/2022）
- SQL Server Express：`Data Source=.\SQLEXPRESS;`（Windows 身份验证）

## 快速开始

1. 使用 Visual Studio 打开 [SkyTakeout_WinForm.sln](file:///d:/temp/SkyTakeout_WinForm/SkyTakeout_WinForm.sln)
2. 确保本机存在 SQL Server 实例 `.\SQLEXPRESS`，并且数据库 `SkyTakeout` 已创建/导入
3. 检查连接字符串：
   - 默认从 [App.config](file:///d:/temp/SkyTakeout_WinForm/SkyTakeout_WinForm/App.config) 的 `ConnectionStrings["SkyTakeout"]` 读取
   - 若未配置，会使用：`Data Source=.\SQLEXPRESS;Initial Catalog=SkyTakeout;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;`
4. 直接运行项目即可

## 功能概览

- 工作台：控制台快捷入口跳转到菜品/套餐管理及新增页面
- 菜品管理 / 套餐管理 / 分类管理 / 员工管理
- 数据统计：基于现有表的简版统计（菜品、套餐、分类、员工）
- 订单管理：实训占位页

## 说明

- `项目实训/` 目录不纳入版本管理（已在 `.gitignore` 忽略）

