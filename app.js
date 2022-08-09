const express = require('express');
const bodyParser = require('body-parser');
const { response } = require('express');

const app = express();
const port = 3000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}))

app.use(express.static("public"))

app.set("views", "./views");
app.set("view engine", "pug");

app.get("/", (req,res) =>
{
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

})

app.listen(port, () =>
{
    console.log(`Listening on port ${port}`)
})

