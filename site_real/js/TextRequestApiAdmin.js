import Post from './Post.js';
function SetText(TextName,Text){
    return Post(`/text/${TextName}`,Text);
}