var mysql = require('mysql');

var pool = mysql.createPool({
  connectionLimit: 10,
  host: 'localhost',
  user: 'root',
  password: '',
  database: 'MOSAD_MIDTERM_DB'
}); // Establish Pooling Connections

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