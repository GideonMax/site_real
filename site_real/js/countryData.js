﻿/**
 * @typedef {Object} Country
 * @property {String} CountryName 
 * @property {String} OfficialArticle 
 * @property {String} UserArticle 
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
    return fetch(`/country/get/${Code}`).then(res => res.json());
}
/**
 * sets the country's data without changing any admin field
 * @param {string} Code the country's code
 * @param {Country} Data the Country object to set the country to
 */
function setCountryData(Code, Data) {
    return fetch(`/country/set/${Code}`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Data)
    });
}