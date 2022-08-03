const express = require('express');
const bodyParser = require('body-parser');

const app = express();
const port = 3000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}))

app.use(express.static("public"))

app.set("views", "./views");
app.set("view engine", "pug");

app.get("/", (req,res) =>
{
    
})

app.get("/location", (req,res) =>
{
    
})

app.post("/location", (req,res) =>
{
    
})

app.get("/order", (req,res) =>
{
    
})

app.post("/order", (req,res) =>
{
    
})