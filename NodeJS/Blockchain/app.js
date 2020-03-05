var CryptoBlock = require("../blockchain/entities/cryptoblock.js");
var CryptoBlockChain = require("../blockchain/entities/cryptoblockchain.js");
var body_parser = require("body-parser");
var express = require("express");
var app = express();

var mongoose = require("mongoose");
mongoose.Promise = global.Promise;
mongoose.connect("mongodb://localhost:27017/blockchain-demo");

var blockSchema = new mongoose.Schema({
    id: { type: Number },
    difficulty: { type: Number },
    blockchain: [{
        index: { type: Number },
        timestamp: { type: String },
        data: { type: String },
        precedingHash: { type: String },
        hash: { type: String },
        nonce: { type: Number }
    }]
});

var blockchainModel = mongoose.model("BlockChain", blockSchema);

let chain = new CryptoBlockChain();

var lastBlock = chain.obtainLatestBlock();
var index = lastBlock.index;

app.use(body_parser.json({ extended: true }));

app.get("/blockchain", (req, res, next) => {
    res.json(chain);
});

app.post("/blockchain", (req, res, next) => {
    console.log("Data received:" + JSON.stringify(req.body));
    chain.addNewBlock(new CryptoBlock(
        index, req.body.timestamp, req.body.data
    ));
    index++;
    
    blockchainModel.findOne({ id: 1 }, function (err, doc) {
        if (err) {
            console.log(err);
        }
        else {
            doc =  new blockchainModel(chain);
            doc.save();           
        }
    });
    
    res.json({ status: 200, responseText: "Block data added" });
});


app.listen(3000, () => {
    console.log("Server running");
});