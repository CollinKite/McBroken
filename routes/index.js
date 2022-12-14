
const fetch = require("node-fetch");
const express = require('express');
const router = express.Router();
const bcrypt = require('bcrypt');
const open = require("open");

const https = require('https');
const httpsAgent = new https.Agent({
    rejectUnauthorized: false,
  });
// const fetch = require("../data/api.js");

const dal = require('../data/mongoDAL');
const { response } = require("express");

////////////////////////////////////////////////////////////////////////////////////

router.get('/', (req, res) => {
    //Make a cookie that will store the last time the user visited the site
    let lastVisit = new Date();
    res.cookie('lastVisit', lastVisit);

    console.log(req.cookies)

    let model = {
        loggedInUser: req.session.user,
        lastVisit: req.cookies.lastVisit
    }

    res.render('home', model);

})

////////////////////////////////////////////////////////////////////////////////////

router.get('/login', (req, res) => {
    let model = {
        loggedInUser: req.session.user
    }
    res.render('login', model);
})

router.post('/login', async (req, res) => {
    let email = req.body.email;
    let password = req.body.password;

    var userLogin = await dal.findUser("Email", email);
    console.log(`This is the users Id on the login.post ${userLogin._id}`)

    if(userLogin && await bcrypt.compare(password, userLogin.Password)){
        console.log(`${email} logged in`);

        let user = {
            email: email,
            userId: userLogin._id, 
            isAdmin: false
        }

        req.session.user = user;
        res.redirect("order");
    }else{
        let model = {
            errorMessage: "Invalid Login!",
            Email: email,
            password: password
        };
        console.log("Invalid login!");
        res.render("login", model);
    }
})

////////////////////////////////////////////////////////////////////////////////////

router.get("/logout", async (req, res) => {
    req.session.user = null;
    res.redirect("/");
} )

////////////////////////////////////////////////////////////////////////////////////

router.get('/register', (req, res) => {
    let model = {
        loggedInUser: req.session.user
    }
    res.render('register', model);
})

router.post('/register', async (req, res) => {
    let email = req.body.email;
    let password = req.body.password;
    let confirmPassword = req.body.confirmPassword;
    let isValidPassword = regexValidate(password, /(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[\W])[a-zA-Z\d\W]{8,}/);

    if (email === '' || password === '' || confirmPassword === '' || password !== confirmPassword || !isValidPassword) {
        let model = {
            errorMessage: 'Please fill out all fields and make sure the passwords match',
            email: email,
            password: password,
            confirmPassword: confirmPassword
        }
        res.render('register', model);
    } else {
        let hashedPassword = await bcrypt.hash(password, 10);
        dal.addUser(email, hashedPassword);
        res.redirect('/login');
    }
})

const regexValidate = (ele, pattern) => {
    if (ele === null || ele === ''){
        return false;
    }
    var eleResult = ele.match(pattern);
    if(!eleResult){
        return false;
    }
    return true;
}

////////////////////////////////////////////////////////////////////////////////////

router.get('/profile', async (req, res) => {
    let userInfo = await dal.findUser('Email', req.session.user.email);
    console.log(`This is the email on the profile.get: ${req.session.user.email}`);

    let model = {
        loggedInUser: req.session.user,
        Email: userInfo.Email,
    }

    res.render('profile', model);
})

router.post('/profile', async (req, res) => {
    let email = req.body.email;
    let password = req.body.password;
    let confirmPassword = req.body.confirmPassword;
    let isValidPassword = regexValidate(password, /(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[\W])[a-zA-Z\d\W]{8,}/);

    if (email === '' && password !== confirmPassword && !isValidPassword) {
        let model = {
            errorMessage: 'Please fill out all fields and make sure the passwords match',
            email: email,
            password: password,
            confirmPassword: confirmPassword
        }
        res.render('profile', model);
    } else if(!email === '' && password == confirmPassword && isValidPassword){
        let hashedPassword = await bcrypt.hash(password, 10);
        let updatedInfo = {
            Email: email,
            Password: hashedPassword
        }
        await dal.updateUser(req.session.user.userId, updatedInfo);
        res.redirect('/profile');
    } else if(!email === '' && password === 'NA' && confirmPassword === 'NA'){
        let updatedInfo = {
            Email: email
        }
        await dal.updateUser(req.session.user.userId, updatedInfo);
        res.redirect('/profile');
    }

})

////////////////////////////////////////////////////////////////////////////////////

router.get('/order', (req, res) => {
    let model = {
        loggedInUser: req.session.user
    }
    res.render('order', model);
})

router.post('/order', async (req, res) => {
    
    
    let token = "";

    try {

    const response = await fetch("https://localhost:7192/authAccount",{
 
        // Adding method type
        method: "GET",
        agent: httpsAgent,
    });
    
    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    token = await response.text();

    } catch (error) {
        console.log(error);
    }
    // open("mcdmobileapp://registrationMagicLink/"+ token);
    let model = {
        loggedInUser: req.session.user,
        token: token
    }
    res.render('verifyMDAcc', model);
    })
   
    
///////////////////////////////////

router.get('/verifyMDAcc', (req, res) => {
    let model = {
        loggedInUser: req.session.user
    }
    res.render('verifyMDAcc', model);
})

router.post('/verifyMDAcc', async (req, res) => {
    
    
    let token = "";

    try {

    const response = await fetch("https://localhost:7192/loginToken",{
 
        // Adding method type
        method: "GET",
        agent: httpsAgent,
    });
    
    if(!response.ok){
        throw new Error(`Error: ${response.status}`);
    }

    token = await response.text();

    } catch (error) {
        console.log(error);
    }
    // open("mcdmobileapp://registrationMagicLink/"+ token);
    let model = {
        loggedInUser: req.session.user,
        token: token
    }
    res.render('loginMDAcc', model);
    })

/////////////////////////////////////////////////

router.get('/loginMDAcc', (req, res) => {
    let model = {
        loggedInUser: req.session.user
    }
    res.render('loginMDAcc', model);
})


        

module.exports = router;

