const express = require('express');
const bodyParser = require('body-parser');
const { response } = require('express');
const geoLite = require("geoip-lite");
const ip = require('ip');
const cookieParser = require('cookie-parser');
const session = require('express-session');

const sessionconfig = {
    secret: 'secret',
    cookie: {}
}
const app = express();
const port = 3000;

app.use(cookieParser());
app.use(session(sessionconfig));

var userIP = "69.27.21.153";//ip.address();
var geo = geoLite.lookup(userIP);

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}))

app.use(express.static("public"))

app.set("views", "./views");
app.set("view engine", "pug");

const indexRouter = require('./routes/index');
app.use('', indexRouter);

const usersRouter = require('./routes/users');
app.use('/u', usersRouter);

app.listen(port, () =>
{
    console.log(`Listening on port ${port}`)
})

// const setMcAddress = () =>
// {
//     const address = document.getElementById("address");
//     address.innerText = "TODO: Place Mcdonalds Address Here";
// }

// const setMcOrderNumber = () =>
// {
//     const orderNumber = document.getElementById("orderNumber");
//     orderNumber.innerText = "TODO: Place Order Number Here";
// }