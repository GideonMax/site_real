
/**
 * @typedef {Object} Country
 * @property {Number} ID
 * @property {String} code
 * @property {String} CountryName 
 * @property {String} OfficialArticle 
 * @property {String} UserArticle 
 * @property {Boolean} exists
 */
/**
 * 
 * @deprecated
 * @summary gets all country names in the database
 * @returns {Promise<string[]>}
 * */
function getAllCountryNames() {
    return fetch("/country/getall").then(res => res.json());
}

/**
 * @returns {Promise<string[]>}
 */
function getAllCountryCodes() {
    return fetch("/country/getcodes").then(res => res.json());
}


/**
 * 
 * @param {string} Code
 * @returns {Promise<Country>} 
 */
function getCountryData(Code) {
    return fetch(`/country/get/${Code}`).then(res => res.json()).then((country=>{
        if(country==null)return {exists:false,code:Code};
        country.exists=true;
        country.code=Code;
        return country;
    }));
}
/**
 * sets the country's data without changing any admin field
 * @param {string} Code the country's code
 * @param {Country} Data the Country object to set the country to
 */
import Post from './Post.js';
function setCountryData(Code, Data) {
    return Post(`/country/set/${Code}`,Data);
}
export {getAllCountryCodes,getAllCountryNames,getCountryData,setCountryData};