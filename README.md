<h1 align="center">DriverExam</h1>
<h3 align="center">驾考刷题：题库、判卷、错题统计</h3>

<p align="center">
  <img src="docs/banner.svg?v=2" alt="DriverExam" width="900" />
</p>

<p align="center">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8%20%7C%209-512BD4" />
  <img alt="Desktop" src="https://img.shields.io/badge/desktop-WinForms-7C4DFF" />
  <img alt="Storage" src="https://img.shields.io/badge/storage-SQLite%20%7C%20MySQL-003B57" />
  <a href="https://github.com/WebbYoung/DriverExam/commits/master"><img alt="Last commit" src="https://img.shields.io/github/last-commit/WebbYoung/DriverExam" /></a>
</p>

---

科目一 505 题、科目四 40 题，支持顺序练习、章节跳转、提交判卷，以及正确 / 错误 / 正确率 / 进度的累计统计。题库是纯文本，直接放在仓库里，换题不用改代码。

仓库里是同一个课题的**两代实现**：先做了一个 WinForms 桌面客户端（直连 MySQL 存账号与进度），后来把它重写成 ASP.NET Core 8 Web API（EF Core + SQLite）。两代共用同一套题库格式和实体语义，所以放在一个仓库里维护 —— `desktop/` 是历史客户端，根目录是重写后的服务端。

有一点要说清楚：**两代目前各自独立运行**，桌面端还没有改成调用 API，服务端也没有对应客户端。合并只是让两份代码待在一起，逻辑上尚未打通；要补什么写在[已知限制](#已知限制与后续路线)里。

---

**想直接刷题？**<br />
Windows 上打开 [`desktop/DriverExamMax.sln`](desktop/DriverExamMax.sln) 编译运行。题库随构建复制到输出目录，不用另外准备。

**想跑服务端接口？**<br />
`dotnet run --project Driver.API`，然后打开 `/swagger`；完整步骤见[服务端](#服务端)。

**想换成自己的题库？**<br />
改 [`desktop/DriverExamMax/Resources/`](desktop/DriverExamMax/Resources/) 下的 `Subject1/` 与 `Subject4/`，格式见[题库数据](#题库数据)。

**想找第一代独立仓库？**<br />
[DriverExamGuide](https://github.com/WebbYoung/DriverExamGuide) 已归档，只读留档，代码与这里一致。

---

## 两代实现

| 代 | 位置 | 形态 | 数据 | 前端 |
| --- | --- | --- | --- | --- |
| 第一代 · 2025-09 | [`desktop/`](desktop/) | WinForms 桌面端 | MySQL 存账号与答题进度，题库为内置文件 | 就是客户端本体 |
| 第二代 · 2025-12 | [`Driver.API/`](Driver.API/) 等 | ASP.NET Core 8 Web API | EF Core + SQLite | 仅 Swagger，客户端未重写 |

## 目录结构

```
DriverExam/
├── DriverExam.slnx           # 服务端解决方案（Linux/macOS 可构建）
├── Driver.API/               # Web API 入口：控制器、JWT、验证码、异常/模型校验过滤器
├── Driver.Service/           # 业务层：DbContext、实体、值对象、Token/题目/用户服务、Migrations
├── Driver.IService/          # 服务接口
├── Driver.DTO/               # 传输对象
├── Driver.Common/            # 通用工具
├── Driver.Test/              # 单元测试
└── desktop/
    ├── DriverExamMax.sln     # 桌面端解决方案（需 Windows，net9.0-windows）
    └── DriverExamMax/        # WinForms 客户端
        ├── LoginForm / MainForm / PracticeForm / Reminderterms
        ├── LoginDal / UserDal / QuestionDal      # 直连 MySQL 的数据访问
        ├── QuizSystem.cs                         # 题库解析与出题
        └── Resources/                            # 题库：Subject1、Subject4 + 协议文本
```

服务端与桌面端拆成两个解决方案文件，是为了让根目录的 `dotnet build` 在非 Windows 环境下仍能通过（`net9.0-windows` 的 WinForms 项目只在 Windows 上还原）。

## 技术栈

**服务端**：.NET 8 · ASP.NET Core · EF Core 8.0.22 (SQLite) · JWT Bearer (`System.IdentityModel.Tokens.Jwt` 8.15.0) · Swashbuckle 6.6.2 · CaptchaGen.NetCore 1.1.2

**桌面端**：.NET 9 (`net9.0-windows`) · WinForms · MySqlConnector 2.4.0 · Magick.NET 14.6.0（图像处理）· MD5 2.2.5

## 题库数据

题库以文本 + 图片的形式放在 `desktop/DriverExamMax/Resources/`，共 690 个文件（约 22 MB）：

- `Subject1/` 科目一 **505 题**，`Subject4/` 科目四 **40 题**，每题一个编号目录
- 每题一个 `Question.txt`，约 140 题另配有图片（133 个 png + 10 个 webp）
- `PrivacyPolicy.txt`、`UsageAgreement.txt` 供登录页弹窗使用

`Question.txt` 是自定义的纯文本格式，`F-` 前缀标记正确选项：

```
[驾驶机动车在道路上违反道路交通安全法的行为，属于什么行为？]
[A、违章行为]
[F-B、违法行为]
[C、过失行为]
[D、违规行为]
[驾驶机动车在道路上违反道路交通安全法的行为属于违法行为。……]
```

行序含义：题干 → 选项（A/B/C/D，前缀 `F-` 表示该项为正确答案）→ 最后一行为解析。这套解析逻辑在 `desktop/DriverExamMax/QuizSystem.cs`（`LoadQuestion` / `GetTitle` / `GetOptions` / `GetAnalysis`）。

## 运行

### 服务端

```bash
cd DriverExam
dotnet restore
dotnet ef database update --project Driver.Service --startup-project Driver.API   # 建库
dotnet run --project Driver.API
```

- 默认监听 `https://localhost:7193` / `http://localhost:5108`（见 `Driver.API/Properties/launchSettings.json`），启动后访问 `/swagger` 查看全部接口。
- SQLite 库文件 `Driver.db` 落在程序输出目录（`AppContext.BaseDirectory`）。
- **JWT 签名密钥**默认值只是占位串，正式使用请用环境变量覆盖，不要提交真实密钥：

  ```bash
  export JWT__SecKey="用足够长的随机字符串"
  ```

### 桌面端（Windows）

1. 准备 MySQL，建库建表：

   ```sql
   CREATE DATABASE IF NOT EXISTS user DEFAULT CHARACTER SET utf8mb4;
   CREATE TABLE IF NOT EXISTS UserInfo (
     UserName   VARCHAR(50) PRIMARY KEY,
     UserPwd    VARCHAR(64) NOT NULL,
     CorrectNum INT NOT NULL DEFAULT 0,
     CurrentNum INT NOT NULL DEFAULT 0
   );
   ```

2. 配置连接串（**凭据不写在源码里**）。二选一：
   - 环境变量：`set DRIVEREXAM_DB=Server=localhost;Port=3306;Database=user;Uid=root;Pwd=你的密码;CharSet=utf8mb4;`
   - 把 [`desktop/DriverExamMax/appsettings.Example.json`](desktop/DriverExamMax/appsettings.Example.json) 复制为同目录下的 `appsettings.json` 并填好密码（构建时会复制到输出目录，程序优先读环境变量）。

3. 打开 `desktop/DriverExamMax.sln` 或 `dotnet run --project desktop/DriverExamMax`。题库目录 `Resources/` 会随构建复制到输出目录，程序按 `AppContext.BaseDirectory/Resources` 定位，不再依赖开发机路径。

## API 一览（服务端）

所有接口统一返回 `Driver.Common.ApiResult`：`{ "Status": "ok|error", "Message": "", "Data": ... }`；参数校验失败由 `ValidFilter` 拦截并以同样结构返回错误信息，未捕获异常由 `GlobalExceptionFilter` 兜底。

| 方法 | 路径 | 说明 |
| --- | --- | --- |
| POST | `/api/Auth/login` | 手机号 + 密码 + 图形验证码登录，返回 JWT |
| POST | `/api/Auth/register` | 注册（手机号 / 密码 / 确认密码 / 图形验证码 / 短信验证码） |
| DELETE | `/api/Auth/logout` | 退出登录 |
| POST | `/api/Auth/verify-code` | 申请图形验证码，返回 `clientId` + Base64 图片 |
| GET | `/api/Auth/verify-code/{clientId}` | 按 `clientId`（32 位）取验证码图片 |
| POST | `/api/Auth/sms-code` | 申请短信验证码（示例实现） |
| POST | `/api/Auth/sms-code/{phoneNumber}` | 向指定手机号发送短信验证码（示例实现） |
| GET | `/api/Question/order/info` | 初始信息：总题数 / 已答 / 正确 / 错误 / 正确率 / 进度 |
| GET | `/api/Question/order` | 当前题 |
| GET | `/api/Question/order/prev` | 上一题（携带 `CurrentId` / `CurrentIndex` / `TotalCount`） |
| GET | `/api/Question/order/next` | 下一题 |
| GET | `/api/Question/{id}` | 按 ID 取题 |
| GET | `/api/Question/{id}/image` | 取题目配图 |
| POST | `/api/Question` | 提交答案判卷，返回是否正确、正确答案与累计对错数 |

鉴权：除登录/注册/验证码外，其余接口需要 `Authorization: Bearer <token>`；令牌有效期 30 分钟（`JWT:ExpiredTime`，单位分钟）。图形验证码在 `IMemoryCache` 中缓存 10 分钟，校验一次即失效。

题型枚举 `QuestionType`：`Single = 0`、`Multiple = 1`、`Judgement = 2`。

## 已知限制与后续路线

1. **桌面端未接入 API**。`desktop/` 全部数据访问都直连 MySQL，代码里没有任何 `HttpClient` 调用；服务端也没有对应的桌面客户端。合并只是把两代实现放进同一个仓库，**逻辑上尚未打通**。
2. 打通需要三步：① 服务端补题库导入（把 `Resources/` 的 `Question.txt` 解析进 SQLite，`desktop/DriverExamMax/QuizSystem.cs` 的解析器可直接复用）；② 服务端补科目维度（当前 `Question` 没有 Subject 字段）；③ 桌面端把 `LoginDal` / `UserDal` / `QuestionDal` 换成 HTTP 客户端，或直接写一个新的前端，并删除 MySQL 依赖。
3. 短信验证码是示例实现，没有接真实短信网关。
4. `Driver.Test` 覆盖有限，目前主要是服务层的基础用例。

## 安全提醒

这个仓库早期版本提交过两处真实凭据，已在本仓库中清除，但**它们仍留在 Git 历史里**（公开仓库等于已泄露）：

- `desktop/DriverExamMax/SqlManager.cs` 曾硬编码本机 MySQL 的 root 密码 → 请更换该数据库密码；
- `Driver.API/appsettings.json` 曾硬编码 JWT 签名密钥 → 请不要再复用该字符串。

现在的做法：连接串只从环境变量 `DRIVEREXAM_DB` 或 `appsettings.json` 读取，JWT 密钥用 `JWT__SecKey` 环境变量覆盖。

## 许可

个人学习项目，未附带开源许可文件；如需引用请先联系作者。
