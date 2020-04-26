
/**
 * 
 * 
 * @returns {Promise<string[]>}
 * 
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
 * @returns {Promise<object>} 
 */
function getCountryData(Code) {
    return fetch(`/country/get/${Code}`).then(res => res.json());
}

function setCountryData(Code, Data) {
    return fetch(`/country/set/${Code}`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Data)
    });
}