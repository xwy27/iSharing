var db = require('./db');

db.getConnection((err, connection) => {
  if (err) {
    return console.error(err);
  }
  connection.query('create table if not exists Item' +
    '(username VARCHAR(30) not null,' +
    'itemid INTEGER not null AUTO_INCREASEMENT,' +
    'itemname VARCHAR(100),' +
    'price FLOAT,' +
    'description TEXT,' +
    'leasetimes INTEGER,' +
    'icon TEXT,' +
    'primary key (itemid),' +
    'FOREIGN KEY (username) REFERENCES User (username) ON DELETE CASCADE)', (err, results, fields) => {
      connection.release();
      if (err) {
        console.error('Error creating tables:\n' + err.stack);
      }
    });
});

module.exports = Item;

function Item (item) {
  username = item.username;
  itemname = item.itemname;
  price = item.price;
  description = item.description;
  leasetimes = item.leasetimes;
  icon = item.icon;
  itemid = null;
};

Item.prototype.save = (callback) => {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('insert into Item (username, itemname, price, description, leasetimes, icon, itemid)' +
      'values (?, ?, ?, ?, ?, ?, ?)',
      [username, itemname, price, description, leasetimes, icon, itemid],
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

Item.prototype.update = (callback) => {
  db.getConnection((err, connection) => {
    if (err) {
      return callback(err);
    }

    connection.query('update Item set username=?, itemname=?, price=?, tel=?, description=?, icon=? , leasetimes=?, where itemid=?',
      [username, itemname, price, tel, description, icon, leasetimes, itemid],
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

    connection.query('select * from Item limit ?, ?', [pageNumber * 20 + 1, pageNumber * 20 + 20], (err, results, fields) => {
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

    connection.query('select * from Item where username = ? limit ?, ?', [username, pageNumber * 20 + 1, pageNumber * 20 + 20], (err, results, fields) => {
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

    connection.query('select * from Item where itemname like ? limit ?, ?', [itemname, pageNumber * 20 + 1, pageNumber * 20 + 20], (err, results, fields) => {
      if (err) {
        return callback(err);
      }

      callback(null, results);
    });
  })
}