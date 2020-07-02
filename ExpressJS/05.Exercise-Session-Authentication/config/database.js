const mongoose = require('mongoose');
const User = require('../models/User');

mongoose.Promise = global.Promise;

module.exports = config => {
    mongoose.connect(config.dbPath, {
        useNewUrlParser: true
    });
    const db = mongoose.connection;
    db.once('open', err => {
        if (err) {
            console.log(err);
            return;
        }

        User.seedAdmin()
        .then(() => {
            console.log('Database is ready');
        })
        .catch(err => {
            console.log('Something failed');
            console.log(err);
        });
    });

    db.on('error', reason => {
        console.log(reason);
    });
};