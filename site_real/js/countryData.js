
/**
 * 
 * 
 * @returns {Promise<string[]>}
 * 
 * */
function getAllCountryNames() {
    return new Promise((resolve, reject) => {
        $.get("/country/getall", (data, status) => {
            resolve(data);
        });
    });
}
/**
 * 
 * @param {string} Code
 * @returns {Promise<string>} 
 */
function getCountryArticle(Code) {
    return new Promise((resolve, reject) => {
        $.get("/country/get/" + Code, (data, status) => {
            resolve(data);
        });
    });
}

function getAllCountryCodes() {
    return new Promise((resolve, reject) => {
        $.get("/country/getcodes", (data, status) => {
            resolve(data);
        });
    });
}