var mysql = require('mysql');

var pool = mysql.createPool({
  connectionLimit : 10,
  host      : 'localhost',
  user      : 'host',
  password  : '',
  database  : 'MOSAD_MIDTERM_DB'
}); // Establish Pooling Connections

pool.getConnection((err, connection) => {
  connection.query('create table if not exists User' +
                    '(userid INTEGER AUTO_INCREMENT,' +
                    'username VARCHAR(30) not null unique,' + 
                    'password VARCHAR(20) not null,' +
                    'email VARCHAR(50),' + 
                    'tel VARCHAR(11),' +
                    'communication VARCHAR(30),' +
                    'icon MEDIUMBLOB,' +
                    'primary key (userid))' ,(err, results, fields) => {
    connection.release();
    if (err) {
      console.error('Error creating tables: ' + err.stack);
    }
  });
});

/* Query example
pool.getConnection(function(err, connection) {
  // Use the connection
  connection.query('SELECT something FROM sometable', function (error, results, fields) {
    // And done with the connection.
    connection.release();
 
    // Handle error after the release.
    if (error) throw error;
 
    // Don't use the connection here, it has been returned to the pool.
  });
}); // With manual connection management

pool.query('SELECT 1 + 1 AS solution', function (error, results, fields) {
  if (error)  {
    console.error('Error querying: ' + error.stack);
    return;
  }
  console.log('The solution is: ', results[0].solution);
});
*/

module.exports = pool;