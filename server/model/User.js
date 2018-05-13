var db = require('./db');

db.getConnection((err, connection) => {
  if (err) {
    return console.error(err);
  }
  connection.query('create table if not exists User' +
    '(username VARCHAR(30) not null unique,' +
    'password VARCHAR(20) not null,' +
    'email VARCHAR(50),' +
    'tel VARCHAR(11),' +
    'wechat VARCHAR(30),' +
    'qq VARCHAR(20),' +
    'icon TEXT,' +
    'primary key (username))', (err, results, fields) => {
      connection.release();
      if (err) {
        console.error('Error creating tables:\n' + err.stack);
      }
    });
});

/**
 * User 的构造函数
 * @param {struct} user
 * @return {null} null
 */
function User(user) {
  this.username = user.username;
  this.password = user.password;
  this.email = user.email;
  this.tel = user.tel;
  this.wechat = user.wechat;
  this.qq = user.qq;
  this.icon = user.icon;
  this.userid = user.userid;
}

module.exports = User;

/**
 * Add a user to the table
 * @param {function} callback (err, errMsg{bool, string})
 */
User.prototype.save = function (callback) {

  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('insert into User (username, password, email, tel, qq, wechat, icon)' +
      'values (?, ?, ?, ?, ?, ?, ?)',
      [username, password, email, tel, qq, wechat, icon],
      (err, results, fields) => {
        connection.release();
        if (err) {
          console.error(err);
          return callback(null, {
            error: true,
            errorMsg: '用户名重复'
          });
        }
        callback(null, {
          error: false
        });
      });
  });

};

/**
 * Get a user through according to the username
 * @param {string} username 
 * @param {function} callback (err, results)
 */
User.get = function (username, callback) {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('select * from User where username=?', [username],
      (err, results, fields) => {
        connection.release();
        if (err) {
          return callback(err);
        }
        callback(null, results);
      });
  });
};

/**
 * Update the user
 * @param {function} callback (err, errMsg{bool, string})
 */
User.prototype.update = function (callback) {

  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('update User set username=?, password=?, email=?, tel=?, qq=?, wechat=?, icon=? where username=?',
      [username, password, email, tel, qq, wechat, icon, username],
      (err, results, fields) => {
        connection.release();
        if (err) {
          return callback(null, {
            error: true,
            errorMsg: err
          });
        }
        callback(null, {
          error: false
        });
      });
  });

};