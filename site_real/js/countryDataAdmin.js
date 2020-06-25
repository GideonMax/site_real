/**
 * 
 * @param {string} Code 
 * @param {Country} Data 
 */
import Post from './Post.js';
export default function setCountryDataAdmin(Code, Data) {
    console.log(Data);
    return Post(`/country/setadmin/${Code}`,Data);
}