const express = require('express');
const app = express();

var bodyParser = require('body-parser');
var routes = require('./routes');

var fs = require('fs');

if (!fs.existsSync('public/temp')) {
  fs.mkdirSync('public/temp');
}

if (!fs.existsSync('public/Images')) {
  fs.mkdirSync('public/Images');
}

app.set('port', process.env.PORT || 8000);
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(express.static('public'));

routes(app);

app.listen(app.get('port'), function () {
  console.log('Express server listening on port ' + app.get('port'));
});

module.exports = app;