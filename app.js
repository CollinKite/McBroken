const express = require('express');
const bodyParser = require('body-parser');
const { response } = require('express');
const geoLite = require("geoip-lite");
const ip = require('ip');

const app = express();
const port = 3000;

var userIP = "69.27.21.153";//ip.address();
var geo = geoLite.lookup(userIP);

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}))

app.use(express.static("public"))

app.set("views", "./views");
app.set("view engine", "pug");

app.get("/", (req,res) =>
{
    console.log(userIP)
    console.log(geo);
    res.render("home")
})

app.get("/location", (req,res) =>
{
    res.render("location")
})

app.get("/order", (req,res) =>
{
    res.render("order")
})

app.post("/order", (req,res) =>
{
    res.render("order")
    // setMcAddress();
    // setMcOrderNumber();
})

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