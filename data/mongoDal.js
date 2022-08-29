const { MongoClient, ObjectId} = require('mongodb');

const uri = "mongodb://localhost:27017";
const dbName = "Test";
const collectionName = "PRO100";

////////////////////////////////////////////////////////////////////////////////

const updateUser = async (findKey, updateValues) => {
    const client = await MongoClient.connect(uri);
        
    try{
        const db = client.db(dbName);
        const collection = db.collection(collectionName);

        const filter = {_id: new ObjectId(findKey)};

        var results = await collection.updateOne(filter, {$set: updateValues});

        console.log("updateUser: results");
        console.log(results);

        return results;
    }catch(err){
        console.log("updateUser: Some error happened");
        console.log(err);
    }finally{
        client.close();
    }

}

/////////////////////////////////////////////////////////////////////////////////

const findUser = async (key, value) => {
    const client = await MongoClient.connect(uri);
        
    try{
        const db = client.db(dbName);
        const collection = db.collection(collectionName);


        var query = {};
        query[key] = value;
        console.log(query)

        var results = await collection.findOne(query);

        console.log("findUser: results");
        console.log(results);

        return results;
    }catch(err){
        console.log("findUser: Some error happened");
        console.log(err);
    }finally{
        client.close();
    }
}

//////////////////////////////////////////////////////////////////////////

const addUser = async (email, password) => {
    const client = await MongoClient.connect(uri);

    try{
        const db = client.db(dbName);
        const collection = db.collection(collectionName);

        
        var newUser = {
            Email: email,
            Password: password
        }
    const result = await collection.insertOne(newUser);

    console.log("addNewUser: results");
    console.log(result);
    } catch(err){
        console.log("addNewUser: some error happened");
        console.log(err);
    }finally{
        client.close();
    }
}

///////////////////////////////////////////////////////////////////////////

const findQuestions = async () => {
    const client = await MongoClient.connect(uri);

    try{
        const db = client.db(dbName);
        const collection = db.collection(collectionName);
        
        var query = {questions: {$exists: true}};
        var results = await collection.find(query).toArray();

        return results;
    }catch(err){
        console.log("findQuestions: Some error happened");
        console.log(err);
    }finally{
        client.close();
    }
}

///////////////////////////////////////////////////////////////////////////

// find and delete a user
const deleteUser = async (findKey) => {
    const client = await MongoClient.connect(uri);

    try{
        const db = client.db(dbName);
        const collection = db.collection(collectionName);
        
        var query = {Username: findKey};
        var results = await collection.deleteOne(query);

        return results;
    }catch(err){
        console.log("deleteUser: Some error happened");
        console.log(err);
    }finally{
        client.close();
    }
}

///////////////////////////////////////////////////////////////////////////

exports.findUser = findUser;
exports.addUser = addUser;
exports.updateUser = updateUser;
exports.findQuestions = findQuestions;
exports.deleteUser = deleteUser;
