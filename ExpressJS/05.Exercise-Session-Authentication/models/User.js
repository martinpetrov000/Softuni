const mongoose = require('mongoose');
const encryption = require('../util/encryption');

const userSchema = new mongoose.Schema({
    username: { type: mongoose.Schema.Types.String, required: true, unique: true },
    hashedPass: { type: mongoose.Schema.Types.String, required: true },
    firstName: { type: mongoose.Schema.Types.String },
    lastName: { type: mongoose.Schema.Types.String },
    salt: { type: mongoose.Schema.Types.String, required: true },
    roles: [{ type: mongoose.Schema.Types.String, required: true }],
    rents: [{type:mongoose.Schema.Types.ObjectId, ref: 'Rent'}]
});

userSchema.method({
    authenticate: function (password) {
        return encryption.generateHashedPassword(this.salt, password) === this.hashedPass;
    }
});

const User = mongoose.model('User', userSchema); 

User.seedAdmin = () => {
    try{
        const users = User.find({});
        
        if(users.length > 0){
            return;
        }

        const adminPassword = 'admin123';
        const salt = encryption.generateSalt();
        const hashedPass = encryption.generateHashedPassword(salt, adminPassword);

        return User.create({
            username: 'Admin',
            hashedPass,
            firstName: 'Peter',
            lastName: 'Georgiev',
            salt,
            roles: ['Admin']
        });
    }catch(err){
        console.log(err);
    }
}

module.exports = User;