/**
 * @typedef {Object} Country
 * @property {Number} ID
 * @property {String} code
 * @property {String} CountryName 
 * @property {String} OfficialArticle 
 * @property {String} UserArticle 
 * @property {Boolean} exists
 */
//#region MapsImports
import * as MapsCore from './Maps/Charts/core.js';
import * as Maps from './Maps/Charts/maps.js';
import theme from './Maps/Charts/themes/animated.js';
import { default as GeoData } from './Maps/geodata/worldHigh.js';
//#endregion
//#region MyOwnImports
import GeoCode from './geocoding.js';
import { getCountryData } from './countryData.js';
import * as CommentApi from './ForumApi.js';
//#endregion
//const Maps=H;
//let apiKey = "PMFoDa_dg5LnVM4X9dVHmZuzLg_haag5crYUoYoregg";
class MapElement extends HTMLElement {
    constructor() {
        super();
        this.articleDiv=document.createElement("div");
        this.articleDiv.className="popup";
        this.attachShadow({ mode: 'open' });
        let style = document.createElement("link");
        style.rel = "stylesheet";
        style.href = "/css/Map.css";
        this.shadowRoot.appendChild(style);
        this.mapDiv = document.createElement("div");
        this.mapDiv.style.width = "100%";
        this.mapDiv.style.height = "460px";
        this.shadowRoot.appendChild(this.mapDiv);
        this.SetUpMap();
        this.exitButton=document.createElement("button");
        this.exitButton.innerText="X";
        this.exitButton.setAttribute("onclick","closeArticle()");
        /*this.exitButton.addEventListener("load",()=>{
            console.log("loaded");
            this.exitButton.addEventListener("click",(event) => {
                console.log("yes");
                this.articleDiv.childNodes.forEach(val=>val.remove());
                this.articleDiv.remove();
            });
        })*/
    }
    SetUpMap() {
        MapsCore.useTheme(theme);
        this.map = MapsCore.create(this.mapDiv, Maps.MapChart);
        this.map.geodata = GeoData;
        this.map.projection = new Maps.projections.Miller();
        this.series = this.map.series.push(new Maps.MapPolygonSeries());
        this.series.useGeodata = true;
        this.polygonTemplate = this.series.mapPolygons.template;
        this.polygonTemplate.events.on("hit", (event) => {
            this.articleDiv.childNodes.forEach(val=>val.remove());
            this.articleDiv.remove();
            this.map.zoomToMapObject(event.target);
            let coords = this.map.svgPointToGeo(event.svgPoint);
            GeoCode(coords.latitude, coords.longitude).then(getCountryData).then((country) => this.showCountry(country));
        });
        this.series.exclude = ["AQ"];
    }
    /**
     * 
     * @param {Country} country
     */
    showCountry(country) {
        /*let exitButton = document.createElement("button");
        exitButton.innerText="X";
        exitButton.addEventListener("click",(event) => {
            console.log("yes");
            this.articleDiv.childNodes.forEach(val=>val.remove());
            this.articleDiv.remove();
        });*/
        this.articleDiv.appendChild(this.exitButton);
        let editButton = document.createElement("button");
        editButton.innerText="ערוך";
        editButton.onclick = this.PopUpEditWindow(country);
        this.articleDiv.appendChild(editButton);
        if (!country.exists) {
            let text = document.createElement("div");
            text.innerHTML = "<h1>על המדינה הזאת אין לנו מידע</h1><br>אולי תוסיף?";
            this.articleDiv.appendChild(text);
        }
        else {
            this.articleDiv.innerHTML+=`<h1>${country.CountryName}</h1>`;
            if (country.OfficialArticle && country.OfficialArticle != null) {
                this.articleDiv.innerHTML+="<h2>ערך רשמי</h2>";
                this.articleDiv.innerHTML+=country.OfficialArticle;
            }
            if (country.UserArticle && country.UserArticle != null) {
                this.articleDiv.innerHTML+="<h2>ערך של משתמשי האתר</h2>";
                this.articleDiv.innerHTML+=country.UserArticle;
            } CommentApi.GetCountryComments(country.ID).then(comments => {
                
                if (comments.length == 0) {
                    this.articleDiv.innerHTML+="<h3>אין תגובות</h3>";
                    return;
                }
                let list = document.createElement("ol");
                for (var comment of comments) {
                    let item = document.createElement("li");
                    item.innerHTML = `${comment.UserName}:<br>${comment.Body}<br>`;
                    list.appendChild(item);
                }
                this.articleDiv.appendChild(list);
            })
        }
        this.shadowRoot.appendChild(this.articleDiv);


    }
    connectedCallback(){
    }
    /**
     * 
     * @param {Country} country 
     */
    PopUpEditWindow(country) {

    }
    /**
     * 
     * @param {Country} country
     * @returns {String} 
     */
    countryToText(country) {
        return (country.exists ? JSON.stringify(country) : `${country.code} has no data in the database`);
    }

}
function closeArticle(){
    let map=document.getElementById("map");
    map.articleDiv.innerHTML="";
    map.articleDiv.remove();
}
window.closeArticle=closeArticle;
window.customElements.define("g-map", MapElement);