/**
 * 
 * @param {string} TextName 
 * @returns {Promise<string>}
 */
function GetText(TextName) {
    return fetch(`/text/${TextName}`)
        .then(res => res.text());
}