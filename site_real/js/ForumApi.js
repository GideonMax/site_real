import Post from './Post.js';
/**
 * @typedef {object} Comment
 * @property {Number} ID
 * @property {Number} Country
 * @property {Number} UserID
 * @property {string} UserName
 * @property {string} Body
 */
/**
 * 
 * @param {Number} Country
<<<<<<< Updated upstream
<<<<<<< Updated upstream
 * @returns {Promise<Comment[]>} 
=======
 * @returns {Comment[]} 
>>>>>>> Stashed changes
=======
 * @returns {Comment[]} 
>>>>>>> Stashed changes
 */
function GetCountryComments(Country){
    return fetch(`forum/${Country}`).then(res=>res.json());
}
/**
 * 
 * @param {Comment} comment 
 */
function AddNewComment(comment){
    Post(`forum/${comment.Country}`,comment);
}
/**
 * 
 * @param {Number} Country 
 * @param {Number} UserID 
 * @param {string} Body 
 */
function BuildAndAddComment(Country,UserID,Body){
    let comment={Country:Country,UserID:UserID,Body:Body};
    AddNewComment(comment);
<<<<<<< Updated upstream
<<<<<<< Updated upstream
}
export{GetCountryComments,AddNewComment,BuildAndAddComment};
=======
}
>>>>>>> Stashed changes
=======
}
>>>>>>> Stashed changes
