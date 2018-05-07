# iSharing

<!-- TOC -->

- [iSharing](#isharing)
  - [V1.0](#v10)
  - [DDL](#ddl)
  - [API](#api)

<!-- /TOC -->

MOSAD MID

租赁服务平台

xaml->localhost->response->xaml

## V1.0

1. 页面(navigation view)
    - 登陆/注册页面(Xwy)
        - Username
        - Password(encode)
        - E-mail
        - Tel
        - 扣扣/微信
        - 头像
    - 浏览页面(Xya)
        - 物品图片
        - 物品名称
        - 租赁价格
    - 物品详情(Xya)
        - 物品名称 & 价格
        - 物品图片
        - 物品描述
        - 房东信息<!--
        已租次数
        评论列表
        评论框-->
    - 发布页面(Xya)
        - 物品名称
        - 物品价格
        - 物品图片
        - 物品描述
    - 个人信息(Xwy)
        - 头像(V2.0)
        - Username
        - Password(encode)
        - E-mail
        - Tel
        - 扣扣/微信<!--
        租赁记录-->
    - 物品管理(Xya)
        - 物品图片
        - 物品名称
        - 租赁价格
1. Database(Xyq)
    - 用户表单
        - userId(int primary key)
        - 头像(blob)
        - Username(string unique)
        - Password(string)
        - E-mail(string)
        - Tel(string)
        - 扣扣/微信(string)
    - 物品表单
        - userId(int, foreignkey)
        - ItemId(int, primarykey)
        - 物品名称(string)
        - 物品价格(float)
        - 物品描述(string)
        - 物品图片(blob)
        - 已租次数(int)<!--
        租赁关系
        userId1(int, foreignkey)
        userId2(int, foreignkey)
        ItemId(int, foreignkey)
        rentId(int, primarykey)
    评论表单
        commentId(int, primarykey)
        userId(int, foreignkey)
        ItemId(int, foreignkey)
        time(string)-->
1. Server(Xyq)

## DDL

1. UI 5.13
1. Server 5.13(hope)

## API

1. 用户所有信息

1. 物品信息