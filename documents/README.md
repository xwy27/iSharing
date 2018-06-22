# 如何启动服务端？

## MySQL 安装、配置和启动

### 从下载 MySQL 开始

首先点击 [MySQL 官方下载页](https://dev.mysql.com/downloads/mysql/)，拉到最下方，您可以看到数个下载链接，请选择 ZIP 压缩包版本进行下载。

### 安装 MySQL

下载完成后，将 MySQL 解压到任意位置。使用命令行进入其解压目录下的 `bin` 文件夹，运行：

```bat
mysqld --initialize
mysqld --install
```

### 启动 MySQL 服务器

完成少数操作后，运行如下命令即可启动 MySQL 服务器

```bat
net start mysql
```

### 找到你的初始密码并登陆服务器

进入 `data` 文件夹，您可以看到一个 `XXX.err` 文件，打开文件，您就可以看到您的初始密码了。

之后，在命令行中输入：

```bat
mysql -u root -p
```

并且输入初始密码即可进入服务器

### 修改你的初始密码，并创建数据库

在修改密码前，mysql 将不会允许你进行大多数操作，因而试用以下命令修改密码：

```bat
mysqladmin -u root -p [初始密码] password [新密码];
```

之后，创建数据库：

```bat
create database MOSAD_MIDTERM_DB;
```

### 将你的密码写入服务端中

在 server/model/db.js 中，将 password 修改为你的新密码：

```js
var pool = mysql.createPool({
  connectionLimit: 10,
  host: 'localhost',
  user: 'root',
  password: 'YOUR PASSWORD HERE',
  database: 'MOSAD_MIDTERM_DB'
}); // Establish Pooling Connections
```

至此，MySQL 已经准备就绪。

## Node.js 安装和配置

进入 [nodejs官网](https://nodejs.org/zh-cn/)，点击最新版本下载，之后按照提示完成安装即可。安装完成后请重启您的命令行。

## NPM 依赖安装

进入 `server` 目录，输入以下命令：

```bat
npm install
```

等待处理完成即可

## 启动服务端

完成上述所有步骤后，在 `server` 目录下，键入以下命令：

```bat
node server.js
```

您就可以看到服务端已经成功运行起来了。