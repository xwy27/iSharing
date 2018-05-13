var db = require('./db');

function User(user) {
  this.username = user.username;
  this.password = user.password;
  this.email = user.email;
  this.tel = user.tel;
  this.communication = user.communication;
  this.icon = user.icon;
  this.userid = user.userid;
}

module.exports = User;

// callback format: err, userid
User.prototype.save = function(callback) {

  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('insert into User (username, password, email, tel, userid, communication, icon)' + 
                      'values (?, ?, ?, ?, ?, ?, ?)', 
                      [username, password, email, tel, userid, communication, icon],
                      (err, results, fields) => {
      connection.release();
      if (err) {
        return callback(err);
      }
      callback(null, results.insertId);
    });
  });

};

// callback format: err, user
User.get = function (userid, callback) {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('select * from User where userid=?', [userid], 
                      (err, results, fields) => {
      connection.release();
      if (err) {
        return callback(err);
      }
      callback(null, results);
    });
  });
};

// callback format: err
User.prototype.update = function(callback) {

  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('update User set username=?, password=?, email=?, tel=?, communication=?, icon=? where userid=?', 
                      [username, password, email, tel, communication, icon, userid],
                      (err, results, fields) => {
      connection.release();
      if (err) {
        return callback(err);
      }
      callback(null);
    });
  });

};