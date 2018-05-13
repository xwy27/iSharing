var User = require('./model/User'),
  Item = require('./model/Item'),
  Comment = require('./model/Comment'),
  Lease = require('./model/Lease');

module.exports = function(app) {
  app.post('/user_add', (req, res) => {
    
    var body = req.body;
    
    var username = body.username,
      password = body.password,
      email = body.email,
      tel = body.tel,
      communication = body.communication,
      icon = body.icon;

    var newUser = new User({
      username: username,
      password: password,
      email: email,
      tel: tel,
      communication: communication,
      icon: icon,
      userid: null
    });

    newUser.save((err, userid) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      console.log(userid);
      req.status(200).json({
        user: {
          userid: userid
        }
      }).end();
      return;
    });
  });

  app.post('/user_get', (req, res) => {
    var body = req.body;

    var userid = body.userid;

    User.get(userid, (err, user) => {
      if (err) {
        res.status(500).end();
        console.error(err);
        return;
      }

      console.log(user.userid);
      req.status(200).json({
        user: {

        }
      }).end();
      return;
    });
  });
}