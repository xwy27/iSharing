var User = require('./model/User'),
  Item = require('./model/Item');
var fs = require('fs');
var multer = require('multer');
var upload = multer({ dest: 'public/temp/' })

module.exports = function (app) {

  /* User */

  app.post('/user_add', (req, res) => {

    var body = req.body;

    console.log(body);

    var username = body.user.username,
      password = body.user.password,
      email = body.user.email,
      tel = body.user.tel,
      qq = null,
      wechat = null,
      icon = null;

    var newUser = new User({
      username: username,
      password: password,
      email: email,
      tel: tel,
      qq: qq,
      wechat: wechat,
      icon: icon
    });

    newUser.save((err, msg) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        status: msg.error ? 'error' : 'success',
        errorMsg: msg.errorMsg
      }).end();
      return;
    });
  });

  app.post('/user_get', (req, res) => {
    var body = req.body;

    var username = body.user.username;

    User.get(username, (err, users) => {
      if (err) {
        
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        user: {
          username: users[0].username,
          password: users[0].password,
          email: users[0].email,
          tel: users[0].tel,
          qq: users[0].qq,
          wechat: users[0].wechat,
          icon: users[0].icon
        }
      }).end();
      return;
    });
  });

  app.post('/user_login', (req, res) => {
    var body = req.body;
    var username = body.user.username,
      password = body.user.password;

    User.get(username, (err, users) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      if (users[0] && users[0].password == password) {
        res.status(200).json({
          status: 'success'
        }).end();
      }
      else {
        res.status(200).json({
          status: 'error',
          errorMsg: '用户名或密码错误'
        }).end();
      }
    });
  });

  app.post('/user_update', (req, res) => {

    var body = req.body;

    var username = body.user.username,
      password = body.user.password,
      email = body.user.email,
      tel = body.user.tel,
      qq = body.user.qq,
      wechat = body.user.wechat,
      icon = body.user.icon;

    var newUser = new User({
      username: username,
      password: password,
      email: email,
      tel: tel,
      qq: qq,
      wechat: wechat,
      icon: icon
    });

    newUser.update((err, msg) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        status: msg.error ? 'error' : 'success',
        errorMsg: msg.errorMsg
      }).end();
      return;
    });
  });

  /* Image */
  app.post('/image_upload', upload.any(), (req, res) => {
    console.log(req.files);
    var originalname = req.files[0].originalname;
    var temp_path = req.files[0].path;
    var newname = req.files[0].filename + originalname.substring(originalname.lastIndexOf('.'));
    var taget_path = './public/Images/'+ newname;

    fs.rename(temp_path, taget_path, (err) => {
      if (err) {
        res.status(500).end();
        return console.error(err);
      }

      res.status(200).json({
        status: 'success',
        url: 'localhost:8000/Images/' + newname
      }).end();
    });
  });

  /* Item */
  app.post('/item_add', (req, res) => {
    var body = req.body;

    var newItem = new Item({
      username: body.item.username,
      itemid: null,
      itemname: body.item.itemname,
      price: body.item.price,
      description: body.item.description,
      leasetimes: body.item.leasetimes,
      icon: body.item.icon
    });

    console.log(newItem);

    newItem.save((err, errorMsg)=> {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        status: errorMsg.error ? 'error' : 'success',
        errorMsg: errorMsg.errorMsg
      }).end();
      return;
    });
  });
  
  app.post('/item_update', (req, res) => {
    var body = req.body;

    var newItem = new Item({
      username: body.item.username,
      itemid: body.item.itemid,
      itemname: body.item.itemname,
      price: body.item.price,
      description: body.item.description,
      leasetimes: body.item.leasetimes,
      icon: body.item.icon
    });

    newItem.update((err, errorMsg)=> {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        status: errorMsg.error ? 'error' : 'success',
        errorMsg: errorMsg.errorMsg
      }).end();
      return;
    });
  });

  app.post('/item_getone', (req, res) => {
    var body = req.body;

    var itemid = body.item.itemid;

    Item.getOne(itemid, (err, items) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        item: {
          username: items[0].username,
          itemid: items[0].itemid,
          itemname: items[0].itemname,
          tel: items[0].tel,
          description: items[0].description,
          leasetimes: items[0].leasetimes,
          icon: items[0].icon
        }
      }).end();
      return;
    });
  });

  app.post('/item_getpage', (req, res) => {
    var body = req.body;

    var pageNumber = body.pageNumber;

    Item.getList(pageNumber, (err, items) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        items: items
      }).end();
      return;
    });
  });

  app.post('/item_getonesitems', (req, res) => {
    var body = req.body;

    var pageNumber = body.pageNumber,
      username = body.user.username;

    Item.getOnesList(username, pageNumber, (err, items) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        items: items
      }).end();
      return;
    });
  });

  app.post('/item_find', (req, res) => {
    var body = req.body;

    var pageNumber = body.pageNumber,
      itemname = body.item.itemname;

    Item.findList(itemname, pageNumber, (err, items) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      res.status(200).json({
        items: items
      }).end();
      return;
    });
  });
};