var User = require('./model/User'),
  Item = require('./model/Item'),
  Comment = require('./model/Comment'),
  Lease = require('./model/Lease');

module.exports = function(app) {
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
        user: {
          status: msg.error ? 'error' : 'success',
          errorMsg: msg.errorMsg
        }
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

  });

  app.post('/user_update', (req, res) => {
    
  });
}