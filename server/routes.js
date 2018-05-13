var User = require('./model/User'),
  Item = require('./model/Item'),
  Comment = require('./model/Comment'),
  Lease = require('./model/Lease');
var fs = require('fs');
var multer = require('multer');
var upload = multer({ dest:'public/temp/' })

module.exports = function(app) {
  
  /* User */

  app.post('/user_add', (req, res) => {
    
    var body = req.body;
    
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

      req.status(200).json({
        status: msg.error ? 'error' : 'success',
        errorMsg: msg.errorMsg
      }).end();
      return;
    });
  });

  app.post('/user_get', (req, res) => {
    var body = req.body;

    var username = body.username;

    User.get(username, (err, users) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      req.status(200).json({
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

    newUser.update((err, msg) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      req.status(200).json({
        status: msg.error ? 'error' : 'success',
        errorMsg: msg.errorMsg
      }).end();
      return;
    });
  });

  /* Image */
  app.post('/image_upload', upload.any(), (req, res) => {
    console.log(req.files);
    var temp_path = req.files.file.path;
    var taget_path = './public/images/' + req.files.file.name;
    
    fs.rename(temp_path, taget_path, (err) => {
      if (err) {
        res.status(500).end();
        return console.error(err);
      }

      fs.unlink(temp_path, (err) => {
        if (err) {
          res.status(500).end();
          return console.log(Err);
        }
        res.status(200).json({
          status: 'success',
          url: 'localhost:8000/public/images/' + req.files.file.name
        }).end();
      });
    });
  });

  
};