var db = require('./db');

module.exports = Item;

function Item (item) {
  this.username = item.username;
  this.itemname = item.itemname;
  this.price = item.price;
  this.description = item.description;
  this.leasetimes = item.leasetimes;
  this.icon = item.icon;
  this.itemid = item.itemid;
};

Item.prototype.save = function (callback) {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('insert into Item (username, itemname, price, description, leasetimes, icon, itemid)' +
      'values (?, ?, ?, ?, ?, ?, ?)',
      [this.username, this.itemname, this.price, this.description, this.leasetimes, this.icon, this.itemid],
      (err, results, fields) => {
        connection.release();
        if (err) {
          console.error(err);
          return callback(null, {
            error: true,
            errorMsg: '未知错误'
          });
        }
        callback(null, {
          error: false
        });
      });
  });
};

Item.prototype.update = function (callback) {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }
    
    console.log(this);

    if (typeof(this.icon) === 'undefined' || this.icon === null) {
      connection.query('update Item set username=?, itemname=?, price=?, description=?, leasetimes=? where itemid=?',
        [this.username, this.itemname, this.price, this.description, this.leasetimes, this.itemid],
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
    }
    else {
      connection.query('update Item set username=?, itemname=?, price=?, description=?, icon=?, leasetimes=? where itemid=?',
        [this.username, this.itemname, this.price, this.description, this.icon, this.leasetimes, this.itemid],
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
    }
  });
};

/**
 * Get item with itemid
 * @param {int} itemid 
 * @param {function} callback 
 */
Item.getOne = (itemid, callback) => {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('select * from Item where itemid = ?', [itemid], (err, results, fields) => {
      
      connection.release();
      if (err) {
        return callback(err);
      }

      callback(null, results);
    });
  })
};

/**
 * Get the list of items
 * @param {int} pageNumber 
 * @param {function} callback 
 */
Item.getList = (pageNumber, callback) => {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('select * from Item limit ?, ?', [(pageNumber - 1) * 20, (pageNumber - 1) * 20 + 20], (err, results, fields) => {
      
      connection.release();
      if (err) {
        return callback(err);
      }

      callback(null, results);
    });
  })
}

/**
 * Get items that belong to the user
 * @param {string} username 
 * @param {int} pageNumber 
 * @param {function} callback 
 */
Item.getOnesList = (username, pageNumber, callback) => {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('select * from Item where username = ? limit ?, ?', [username, (pageNumber - 1) * 20, (pageNumber - 1) * 20 + 20], (err, results, fields) => {
      
      connection.release();
      if (err) {
        return callback(err);
      }

      callback(null, results);
    });
  })
}
/**
 * 
 * @param {string} itemname 
 * @param {int} pageNumber 
 * @param {function} callback 
 */
Item.findList = (itemname, pageNumber, callback) => {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('select * from Item where itemname like ? limit ?, ?', ['%' + itemname + '%', (pageNumber - 1) * 20, (pageNumber - 1) * 20 + 20], (err, results, fields) => {
      
      connection.release();
      if (err) {
        return callback(err);
      }

      callback(null, results);
    });
  })
}