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
 * @returns {Promise<Comment[]>} 
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
}
export{GetCountryComments,AddNewComment,BuildAndAddComment};