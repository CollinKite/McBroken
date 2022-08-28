const express = require('express');
const router = express.Router();

const dal = require('../data/mongoDAL');

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

module.exports = router;

////////////////////////////////////////////////////////////////////////////////////

router.get('/register', (req, res) => {
    let model = {
        loggedInUser: req.session.user
    }
    res.render('register', model);
})